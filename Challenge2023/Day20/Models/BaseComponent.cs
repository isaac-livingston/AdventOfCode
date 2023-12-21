namespace Challenge2023.Day20.Models
{
    internal abstract class BaseComponent
    {
        public static long LowPulseCount { get; set; } = 0;
        public static long HighPulseCount { get; set; } = 0;

        public const int HIGH_PULSE = 1;
        public const int LOW_PULSE = 0;

        public static bool FirstLowToOutputterFound { get; set; } = false;

        public required string Id { get; set; }

        public List<BaseComponent> ConnectedComponents { get; } = [];

        public abstract void ReceivePulse(int pulse, string fromId, Queue<Action> actions);
    }
}
