using System;


namespace MajorWork.Logic.Helpers
{
    static class MathRandom
    {
        /// <summary>
        /// Due to Random() producing the same variables a helper class has been implemented. 
        /// All numbers are now generated psuedo-randomly, and the code remains thread-safe.
        /// Relevant XKCD: https://xkcd.com/221/
        /// </summary>
        private static readonly Random Getrandom = new Random();
        private static readonly object SyncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (SyncLock) //Should random generation be called from separate threads "random" integers will still be returned
            {
                return Getrandom.Next(min, max);
            }
        }
    }
}
