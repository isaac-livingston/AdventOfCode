namespace Challenge2023.Day20.Models
{
    internal abstract class Pulser
    {
        public static long UnresolvedPulseCount { get; private set; } = 0;

        public static long HighPulseCount { get; private set; } = 0;
        public static long LowPulseCount { get; private set; } = 0;

        public const int HIGH = 1;
        public const int LOW = 0;

        public virtual int EmitHighPulse()
        {
            HighPulseCount++;
            UnresolvedPulseCount++;
            return HIGH;
        }

        public virtual int EmitLowPulse()
        {
            LowPulseCount++;
            UnresolvedPulseCount++;
            return LOW;
        }

        public virtual void ResolvePulse(int pulse)
        {
            UnresolvedPulseCount--;
        }
    }
}
