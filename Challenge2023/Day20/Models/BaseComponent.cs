namespace Challenge2023.Day20.Models
{
    internal abstract class BaseComponent
    {
        public const int HIGH_PULSE = 1;
        public const int LOW_PULSE = 0;

        public static long LowPulseCount { get; set; } = 0;
        public static long HighPulseCount { get; set; } = 0;

        public long? HighPulseOccuredAt { get; protected set; } = null;

        public static void RegisterPulse(int pulse)
        {
            if (pulse == HIGH_PULSE)
            {
                HighPulseCount++;
            }
            else
            {
                LowPulseCount++;
            }
        }

        public void ScheduleActions(int pulse, Queue<Action> actions)
        {
            for (var c = 0; c < ConnectedComponents.Length; c++)
            {
                var component = ConnectedComponents[c];
                actions.Enqueue(() => component.ReceivePulse(pulse, Id, actions));
            }
        }

        public required string Id { get; set; }

        public BaseComponent[] ConnectedComponents { get; set; } = [];

        public abstract void ReceivePulse(int pulse, string fromId, Queue<Action> actions);
    }
}
