namespace Challenge2023.Day20.Models
{
    internal abstract class BaseComponent
    {
        public const int HIGH_PULSE = 1;
        public const int LOW_PULSE = 0;

        public required string Id { get; set; }

        protected List<string> ConnectedComponentIds { get; } = [];

        public abstract void ReceivePulse(int pulse, string? from = null);

        public abstract void SendPulses();
    }
}
