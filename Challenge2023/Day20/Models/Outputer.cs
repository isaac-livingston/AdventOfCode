namespace Challenge2023.Day20.Models
{
    internal class Outputer : BaseComponent
    {
        public override void ReceivePulse(int pulse, string fromId, Queue<Action> actions)
        {
            RegisterPulse(pulse);

            //if (pulse == LOW_PULSE)
            //{
            //    Console.WriteLine($"OUTPUTTER [{Id}]: H:{HighPulseCount} L:{LowPulseCount}");
            //}

            //Console.WriteLine($"{fromId} -> {(pulse == HIGH_PULSE ? "H" : "L")} -> {Id}");

            //Console.WriteLine($"OUTPUTTER [{Id}]: H:{HighPulseCount} L:{LowPulseCount}");
        }
    }
}
