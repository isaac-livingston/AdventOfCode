namespace Challenge2023.Day20.Models
{
    internal class FlipFlop : BaseComponent
    {
        public bool On { get; private set; } = false;

        public override void ReceivePulse(int pulse, string fromId, Queue<Action> actions)
        {
            //Console.WriteLine($"{fromId} -> {(pulse == HIGH_PULSE ? "H" : "L")} -> {Id}");

            if (pulse == HIGH_PULSE)
            {
                HighPulseCount ++;
                return;
            }
            else
            {
                LowPulseCount ++;
            }

            var nextPulse = 0;
            
            if (On)
            {
                nextPulse = LOW_PULSE;
            }
            else
            {
                nextPulse = HIGH_PULSE;
            }

            On = !On;

            for (var c = 0; c < ConnectedComponents.Count; c++)
            {
                var comp = ConnectedComponents[c];
                actions.Enqueue(() => comp.ReceivePulse(nextPulse, Id, actions));
            }
        }
    }
}
