namespace Challenge2023.Day20.Models
{
    internal class Broadcaster : BaseComponent
    {
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

            for (var c = 0; c < ConnectedComponents.Count; c++)
            {
                var component = ConnectedComponents[c];
                actions.Enqueue(() => component.ReceivePulse(pulse, Id, actions));
            }
        }
    }
}
