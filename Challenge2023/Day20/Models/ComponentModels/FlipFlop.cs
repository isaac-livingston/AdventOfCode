﻿namespace Challenge2023.Day20.Models.ComponentModels
{
    internal class FlipFlop : BaseComponent
    {
        public bool On { get; private set; } = false;

        public override void ReceivePulse(int pulse, string fromId, Queue<Action> actions, long pushCount)
        {
            RegisterPulse(pulse);

            if (pulse == HIGH_PULSE)
            {
                return;
            }

            var nextPulse = On
                          ? LOW_PULSE
                          : HIGH_PULSE;

            On = !On;

            ScheduleActions(nextPulse, actions, pushCount);
        }
    }
}
