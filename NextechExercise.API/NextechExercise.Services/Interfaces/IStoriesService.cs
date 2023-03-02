using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextechExercise.Domain;

namespace NextechExercise.Services.Interfaces
{
    public interface IStoriesService
    {
        Task<Story> GetStoryById(int id);
        Task<List<int>> GetNewStoriesIds();
    }
}
