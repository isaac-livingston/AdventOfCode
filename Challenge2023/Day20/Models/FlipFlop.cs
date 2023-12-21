namespace Challenge2023.Day20.Models
{
    internal class FlipFlop : BaseComponent
    {
        public bool On { get; private set; } = false;

        public override void ReceivePulse(int pulse, string fromId, Queue<Action> actions)
        {
            if (pulse == HIGH_PULSE)
            {
                return;
            }

            var nextPulse = 0;
            
            if (On)
            {
                nextPulse = HIGH_PULSE;
                HighPulseCount++;
            }
            else
            {
                nextPulse = LOW_PULSE;
                LowPulseCount++;
            }
            On = !On;
            //actions.Enqueue(new Action(() =>
            //{
                
            //}));

            for (var c = 0; c < ConnectedComponents.Count; c++)
            {
                var comp = ConnectedComponents[c];
                actions.Enqueue(() => comp.ReceivePulse(nextPulse, Id, actions));
            }

            
        }
    }
}
