namespace Challenge2023.Day20.Models
{
    internal abstract class BaseComponent
    {
        public const int HIGH_PULSE = 1;

        public const int LOW_PULSE = 0;

        public required string Id { get; set; }

        public BaseComponent[] ConnectedComponents { get; set; } = [];

        public long LowPulseCount { get; private set; } = 0;

        public long HighPulseCount { get; private set; } = 0;

        public long? NotablePulseObservedAtPushCount { get; protected set; } = null;

        public void RegisterPulse(int pulse)
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

        public void ScheduleActions(int pulse, Queue<Action> actions, long pushCount)
        {
            for (var c = 0; c < ConnectedComponents.Length; c++)
            {
                var component = ConnectedComponents[c];
                actions.Enqueue(() => component.ReceivePulse(pulse, Id, actions, pushCount));
            }
        }

        public abstract void ReceivePulse(int pulse, string fromId, Queue<Action> actions, long pushCount);
    }
}
