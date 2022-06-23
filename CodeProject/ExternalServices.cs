using System;

namespace CodeProject
{

    public interface IGoodLogger
    {
        void LogError(string message);
        void LogInfo(string message);
    }

    public interface ICacheService
    {
        Task Set(int number);
    }

    public interface IPrimeService
    {
        Task<bool> IsPrime(int candidate);
    }

}