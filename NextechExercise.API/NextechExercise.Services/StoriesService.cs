using Newtonsoft.Json;
using NextechExercise.Domain;
using NextechExercise.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextechExercise.Services
{
    public class StoriesService : IStoriesService
    {
        public async Task<Story> GetStoryById(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");

            HttpResponseMessage responseMessage = await client.GetAsync($"item/{id}.json?print=pretty");

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Story>(await responseMessage.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<List<int>> GetNewStoriesIds()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");
            string content = "";

            HttpResponseMessage response = await client.GetAsync("newstories.json?print=pretty");

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<int>>(content);
            }

            return null;
        }
    }
}
