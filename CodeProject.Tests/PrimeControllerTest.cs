using NUnit.Framework;
using CodeProject;
using FakeItEasy;

namespace CodeProject.Tests
{
    [TestFixture]
    public class PrimeControllerTest
    {



        [Test]
        public async Task CachePrimes_IsPrime_PutsInCache()
        {
            // Arrange mocks

            var goodLogger = A.Fake<IGoodLogger>();
            var primeService = A.Fake<IPrimeService>();
            var cacheService = A.Fake<ICacheService>();
            var primeController = new PrimeController(goodLogger, cacheService, primeService);

            A.CallTo(() => primeService.IsPrime(1)).Returns(Task.FromResult(true));


            // call System Under Test (SUT) aka "ACT"
            await primeController.CachePrimes(1);

            // Assert calls happened
            A.CallTo(() => primeService.IsPrime(1)).MustHaveHappenedOnceExactly();
            A.CallTo(() => cacheService.Set(1)).MustHaveHappenedOnceExactly();
            A.CallTo(() => goodLogger.LogInfo("1 was prime")).MustHaveHappenedOnceExactly();
        }

        [Test]
        [Ignore("Not finished yet")]
        public async Task CachePrimes_IsNotPrime_PutsNothignInCacheAndLogsError()
        {
            // Arrange mocks
            var goodLogger = A.Fake<IGoodLogger>();
            var primeService = A.Fake<IPrimeService>();
            var cacheService = A.Fake<ICacheService>();
            var primeController = new PrimeController(goodLogger, cacheService, primeService);


            // call System Under Test (SUT) aka "ACT"
            await primeController.CachePrimes(1);

            // Assert calls happened
        }
    }
}