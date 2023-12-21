namespace Challenge2023.Day20.Models
{
    internal class Conjunction : BaseComponent
    {
        private Dictionary<string, int> InputMemory { get; } = [];

        public override void ReceivePulse(int pulse, string fromId, Queue<Action> actions)
        {
            if (pulse == HIGH_PULSE)
            {
                HighPulseCount++;
            }
            else
            {
                LowPulseCount++;
            }

            if (!InputMemory.ContainsKey(fromId))
            {
                throw new Exception("Component not registered");
            }

            //Console.WriteLine($"{fromId} -> {(pulse == HIGH_PULSE ? "H" : "L")} -> {Id}");

            InputMemory[fromId] = pulse;

            var nextPulse = 0;
            
            if (InputMemory.Values.All(p => p == HIGH_PULSE))
            {
                nextPulse = LOW_PULSE;
            }
            else
            {
                nextPulse = HIGH_PULSE;
            }

            for (var c = 0; c < ConnectedComponents.Count; c++)
            {
                var component = ConnectedComponents[c]; 
                actions.Enqueue(() => component.ReceivePulse(nextPulse, Id, actions));
            }
        }

        public void RegisterInputComponentWithMemory(BaseComponent component)
        {
            InputMemory.Add(component.Id, LOW_PULSE);
        }
    }
}
