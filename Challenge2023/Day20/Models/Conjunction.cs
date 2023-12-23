namespace Challenge2023.Day20.Models
{
    internal class Conjunction : BaseComponent
    {
        private Dictionary<string, int> InputMemory { get; } = [];

        public override void ReceivePulse(int pulse, string fromId, Queue<Action> actions, long pushCount)
        {
            RegisterPulse(pulse);

            InputMemory[fromId] = pulse;

            var nextPulse = InputMemory.Values.All(p => p == HIGH_PULSE)
                          ? LOW_PULSE
                          : HIGH_PULSE;

            if (nextPulse == HIGH_PULSE)
            {
                NotablePulseObservedAtPushCount ??= pushCount;
            }

            ScheduleActions(nextPulse, actions, pushCount);
        }

        public void RegisterInputComponentWithMemory(BaseComponent component)
        {
            InputMemory.Add(component.Id, LOW_PULSE);
        }
    }
}
