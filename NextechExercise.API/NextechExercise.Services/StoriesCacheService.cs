using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NextechExercise.Domain;
using NextechExercise.Services.Interfaces;
using System.Configuration.Internal;
using System.Drawing.Printing;
using System.Runtime.Caching;
using System.Transactions;
using System.Threading.Tasks;

namespace NextechExercise.Services
{
    public class StoriesCacheService : IStoriesCacheService
    {
        public IStoriesService StoriesService;
        public CacheStatusEnum Status { get; set; }
        private readonly IConfiguration _configuration;
        private ObjectCache _cache { get; set; }

        public StoriesCacheService(IStoriesService storiesService, IConfiguration configuration)
        {
            Status = CacheStatusEnum.Not_Initialized;
            _cache = MemoryCache.Default;
            StoriesService = storiesService;
            _configuration = configuration;
        }

        public void AddStoryToCache(Story story)
        {
            if (story != null)
            {
                int cacheExpiration = int.Parse(_configuration["ExpirationTimeCache"]);
                
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.SlidingExpiration = new TimeSpan(0, cacheExpiration, 0);
                _cache.Add(story.Id.ToString(), story, policy);
            }
        }

        public async Task<IEnumerable<Story>> GetStoriesById(List<int> ids, int pageSize, int pageNumber)
        {
            ids = ids.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            List<Story> stories = new List<Story>();

            foreach (int id in ids)
            {
                Story cacheItem = (Story)_cache.Get(id.ToString());

                if (cacheItem == null)
                {
                    cacheItem = await StoriesService.GetStoryById(id);
                    if (cacheItem != null)
                    {
                        AddStoryToCache(cacheItem);
                        stories.Add(cacheItem);
                    }
                }
                else
                {
                    stories.Add(cacheItem);
                }
            }

            return stories;
        }

        public async Task<IEnumerable<Story>> GetStoriesByName(string name, List<int> ids)
        {
            List<string> stringIds = ids.Select(x => x.ToString()).ToList();
            List<Story> stories = new List<Story>();

            var values = _cache.GetValues(stringIds).Values.Where(x =>
            {
                Story auxStory = (Story)x;
                if (!string.IsNullOrWhiteSpace(auxStory.Title) && auxStory.Title.ToLower().Contains(name.ToLower()))
                    return true;
                else
                    return false;
            });

            if (values.Any())
            {
                foreach (var item in values)
                {
                    stories.Add((Story)item);
                }
            }

            return stories;
        }

        public async Task InitializeCache(List<int> ids)
        {
            Status = CacheStatusEnum.On_Initialize;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");

            Parallel.ForEach(ids, async id =>
            {
                HttpResponseMessage response = await client.GetAsync($"item/{id}.json?print=pretty");

                if (response.IsSuccessStatusCode)
                {
                    AddStoryToCache(JsonConvert.DeserializeObject<Story>(await response.Content.ReadAsStringAsync()));
                }
            });

            Status = CacheStatusEnum.Initialized;
        }

        public async Task UpdateCache(List<int> ids)
            {
            Parallel.ForEach(ids, async id =>
            {
                Story story = (Story)_cache.Get(id.ToString());
                
                if(story == null)
                {
                    story = await StoriesService.GetStoryById(id);
                    AddStoryToCache(story);
                }
            });
        }
    }
}
