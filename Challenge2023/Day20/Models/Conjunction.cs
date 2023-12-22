namespace Challenge2023.Day20.Models
{
    internal class Conjunction : BaseComponent
    {
        private Dictionary<string, int> InputMemory { get; } = [];

        public override void ReceivePulse(int pulse, string fromId, Queue<Action> actions)
        {
            RegisterPulse(pulse);

            InputMemory[fromId] = pulse;

            var nextPulse = InputMemory.Values.All(p => p == HIGH_PULSE)
                          ? LOW_PULSE
                          : HIGH_PULSE;

            ScheduleActions(nextPulse, actions);
        }

        public void RegisterInputComponentWithMemory(BaseComponent component)
        {
            InputMemory.Add(component.Id, LOW_PULSE);
        }
    }
}
