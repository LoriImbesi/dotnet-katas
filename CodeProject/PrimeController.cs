using System;

namespace CodeProject
{

    public class PrimeController
    {
        // dependencies
        // These are fields in a class; class is the data and the function;
        // A common C# pattern - are the dependencies and functions (occasionally you'll see extra data)
        private readonly IGoodLogger _goodLogger; 
        private readonly ICacheService _cacheService;
        private readonly IPrimeService _primeService;
        
        // constructor - taking in dependencies / dependency injection
        public PrimeController(IGoodLogger goodLogger, ICacheService cacheService, IPrimeService primeService)
        {
            _goodLogger = goodLogger;  
            _cacheService = cacheService; 
            _primeService = primeService;
        }
        // (dependency injected) controller functions; can also be known as orchestrator
        // one class that uses three other classes to do its work
        public async Task CachePrimes(int number) {
            var isPrime = await _primeService.IsPrime(number);  // "Task" - In javascript this is a "promise"
            // If you want to get the value out of a Task you have to use "await"
            // if true
            if (isPrime) {
                 await _cacheService.Set(number); 
                _goodLogger.LogInfo($"{number} was prime");
            } else {
                _goodLogger.LogError($"{number} wasn't prime");
            }
        }
    }
}