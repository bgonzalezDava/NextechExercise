using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextechExercise.Tests
{
    [TestFixture]
    public class StoriesServiceTests
    {
        private IStoriesService _storiesService;

        [SetUp]
        public void SetUp()
        {
            _storiesService = new StoriesService(); ;
        }

        [Test]
        public async Task Test_GetStoryById_Should_Return_Null()
        {
            int id = -1;

            Story story = await _storiesService.GetStoryById(id);

            Assert.IsNull(story);
        }
    }
}
