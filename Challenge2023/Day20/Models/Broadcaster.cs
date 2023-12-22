namespace Challenge2023.Day20.Models
{
    internal class Broadcaster : BaseComponent
    {
        public override void ReceivePulse(int pulse, string fromId, Queue<Action> actions)
        {
            RegisterPulse(pulse);

            ScheduleActions(pulse, actions);
        }
    }
}
