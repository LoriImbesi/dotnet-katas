using System;

namespace CodeProject
{

    public class PrimeController
    {
        private readonly IGoodLogger _goodLogger; 
        private readonly ICacheService _cacheService;
        private readonly IPrimeService _primeService;
        public PrimeController(IGoodLogger goodLogger, ICacheService cacheService, IPrimeService primeService)
        {
            _goodLogger = goodLogger;  
            _cacheService = cacheService; 
            _primeService = primeService;
        }

        public async Task CachePrimes(int number) {
            var isPrime = await _primeService.IsPrime(number);
            if (isPrime) {
                await _cacheService.Set(number);
                _goodLogger.LogInfo($"{number} was prime");
            } else {
                _goodLogger.LogError($"{number} wasn't prime");
            }
        }
    }
}