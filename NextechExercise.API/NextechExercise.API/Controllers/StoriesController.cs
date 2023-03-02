using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NextechExercise.Domain;
using NextechExercise.Services.Interfaces;
using System.Threading.Tasks;

namespace NextechExercise.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoriesController : Controller
    {
        private IStoriesService _storiesService;
        private static IStoriesCacheService _storiesCacheService;

        private static List<int> _storiesIds;

        public StoriesController(IStoriesCacheService storiesCacheService, IStoriesService storiesService)
        {
            _storiesCacheService = storiesCacheService;
            _storiesService = storiesService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get(int pageSize, int pageNumber)
        {
            if (_storiesCacheService.Status.Equals(CacheStatusEnum.Initialized))
            {
                return Ok(await _storiesCacheService.GetStoriesById(_storiesIds, pageSize, pageNumber));
            }
            else
            {
                return Ok(await GetStoriesFromRequests(pageSize, pageNumber));
            }
        }

        [HttpGet]
        [Route("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (_storiesCacheService.Status == CacheStatusEnum.Initialized)
            {
                _storiesIds = await _storiesService.GetNewStoriesIds();

                await _storiesCacheService.UpdateCache(_storiesIds);

                IEnumerable<Story> filteredStories = await _storiesCacheService.GetStoriesByName(name, _storiesIds);
                return Ok(filteredStories);
            }
            else
            {
                return BadRequest("Cache is still initializing");
            }
        }


        #region Service_Layer

        private async Task<IEnumerable<Story>> GetStoriesFromRequests(int pageSize, int pageNumber)
        {
            List<Story> stories = new List<Story>();

            if (_storiesCacheService.Status.Equals(CacheStatusEnum.Not_Initialized))
            {
                _storiesIds = await _storiesService.GetNewStoriesIds();
                _storiesCacheService.InitializeCache(_storiesIds);
            }

            List<int> trimmedStoriesIds = _storiesIds.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (int id in trimmedStoriesIds)
            {
                stories.Add(await _storiesService.GetStoryById(id));
            }

            return stories;
        }
        #endregion
    }
}
