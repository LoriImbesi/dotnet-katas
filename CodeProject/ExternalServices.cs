using System;

namespace CodeProject
{

    // Class - a container; blueprint - for the data; a class is a mix of functions of data
    // Class is behavior and data; potentially dependencies, state, data, functions
    // Interface - a shape of a class, not the state, not the dependencies - only the shape of the functions
    // Interface - prong/outlet - the interface is the contract - the shape of the functions of the class
    
    // This is the contract for logging - could send to file system, datadog, console (would need a class for each)
    // Each class implements the interface (implementations not in this kata)
    public interface IGoodLogger
    {
        void LogError(string message); // Interface definition; void function returns nothing and it's synchronous
        void LogInfo(string message); // You don't need the await 
    }

    public interface ICacheService
    {
        Task Set(int number); // The return is at the beginning of the function!!! Task is the return!!
    }
    
    // This is the interface and shape of the function, not the actual function itself
    public interface IPrimeService
    {
        Task<bool> IsPrime(int candidate); // returns a Task with/of a boolean;
        
    }

}