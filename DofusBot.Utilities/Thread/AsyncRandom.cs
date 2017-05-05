using System;
using System.Threading;

namespace DofusBot.Utilities.Thread
{
    public sealed class AsyncRandom : System.Random
    {
        private static int m_incrementer;

        public AsyncRandom()
            : base(Environment.TickCount +System.Threading.Thread.CurrentThread.ManagedThreadId + AsyncRandom.m_incrementer)
        {
            Interlocked.Increment(ref AsyncRandom.m_incrementer);
        }

        public AsyncRandom(int seed)
            : base(seed)
        {
        }

        public double NextDouble(double min, double max)
        {
            return this.NextDouble() * (max - min) + min;
        }
    }
}
