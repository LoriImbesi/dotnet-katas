using NUnit.Framework;
using CodeProject;
using FakeItEasy;

// Do this kata from scratch 5 times - delete 1 test first, then delete both


namespace CodeProject.Tests
{
    [TestFixture]
    public class PrimeControllerTest
    {
        private readonly IGoodLogger _goodLogger; 
        private readonly ICacheService _cacheService;
        private readonly IPrimeService _primeService;

        public PrimeControllerTest()
        {
            _goodLogger = A.Fake<IGoodLogger>();
            _cacheService = A.Fake<ICacheService>();
            _primeService = A.Fake<IPrimeService>();
        }
// Mockist testing, not classical - mocking is common for C#

        [Test]
        public async Task CachePrimes_IsPrime_PutsInCache()  // Task without angle brackets is the Task void
        {
            // Arrange mocks
            // mocking the dependencies
            // Fake it Easy makes classes at runtime that implement the Interface
            
            // goodLogger is a class, it is a fake implementation of IGoodLogger; the "I" means Interface b/c that's 
            // the name we called the interface - IT COULD BE CALLED ANYTHING
            // Fake It Easy is a way to generate a class that doesn't exist in the codebase; it's generated
            // in memory ONLY for the test
            var fakeGoodLogger = A.Fake<IGoodLogger>();
            var fakePrimeService = A.Fake<IPrimeService>();
            var fakeCacheService = A.Fake<ICacheService>();
            var primeController = new PrimeController(fakeGoodLogger, fakeCacheService, fakePrimeService);
            
            // Mockist code can do a lot of things; it can hardcode for your use case
            A.CallTo(() => fakePrimeService.IsPrime(13)).Returns(Task.FromResult(true));
            
            // call System Under Test (SUT) aka "ACT"
            await primeController.CachePrimes(13);

            // Assert calls happened
            A.CallTo(() => fakePrimeService.IsPrime(13)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeCacheService.Set(13)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeGoodLogger.LogInfo("13 was prime")).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task CachePrimes_IsNotPrime_PutsNothingInCacheAndLogsError()
        {
            // Arrange
            var fakeGoodLogger = A.Fake<IGoodLogger>();
            var fakeCacheService = A.Fake<ICacheService>();
            var fakePrimeService = A.Fake<IPrimeService>();
            var primeController = new PrimeController(fakeGoodLogger, fakeCacheService, fakePrimeService);

            A.CallTo(() => fakePrimeService.IsPrime(4)).Returns(Task.FromResult<bool>(false));

            // Act

            await primeController.CachePrimes(4);
            
            // Assert
            A.CallTo(() => fakePrimeService.IsPrime(4)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeCacheService.Set(4)).MustNotHaveHappened();
            A.CallTo(() => fakeGoodLogger.LogInfo("4 was prime")).MustNotHaveHappened();
            A.CallTo(() => fakeGoodLogger.LogError("4 wasn't prime")).MustHaveHappenedOnceExactly();

        }
    }
}