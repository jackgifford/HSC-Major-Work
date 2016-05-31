using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorWork.Logic.Helpers
{
    static class MathRandom
    {
        /// <summary>
        /// Due to Random() producing the same variables a helper class has been implemented. 
        /// All numbers are now generated psuedo-randomly, and the code remains thread-safe.
        /// Relevant XKCD: https://xkcd.com/221/
        /// </summary>
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronise
                return getrandom.Next(min, max);
            }
        }
    }
}
