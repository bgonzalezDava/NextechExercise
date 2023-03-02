using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextechExercise.Domain;
namespace NextechExercise.Services.Interfaces
{
    public interface IStoriesCacheService
    {
        public CacheStatusEnum Status { get; set; }

        void AddStoryToCache(Story story);
        Task<IEnumerable<Story>> GetStoriesById(List<int> ids, int pageSize, int pageNumber);
        Task<IEnumerable<Story>> GetStoriesByName(string name, List<int> ids);
        Task InitializeCache(List<int> ids);
        Task UpdateCache(List<int> ids);
    }
}
