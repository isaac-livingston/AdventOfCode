﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2023.Day20.Models
{
    internal class FlipFlop : BaseComponent
    {
        public bool On { get; private set; } = false;

        public override void ReceivePulse(int pulse, string? from = null)
        {
            throw new NotImplementedException();
        }

        public override void SendPulses()
        {
            throw new NotImplementedException();
        }
    }
}
