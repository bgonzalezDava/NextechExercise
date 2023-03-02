using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextechExercise.Tests
{
    [TestFixture]
    public class StoriesCacheServiceTests
    {
        private IStoriesCacheService _storiesCacheService;
        private Mock<IStoriesService> _mockStoriesService = new Mock<IStoriesService>();

        [SetUp]
        public void SetUp()
        {
            _storiesCacheService = new StoriesCacheService(_mockStoriesService.Object);
        }

        [Test]
        public async Task Test_GetStoriesById_Should_Be_Empty()
        {
            List<int> storyIds = new List<int>() { -1 };

            var stories = await _storiesCacheService.GetStoriesById(storyIds);

            Assert.That(stories.Count() == 0);
        }
    }
}
