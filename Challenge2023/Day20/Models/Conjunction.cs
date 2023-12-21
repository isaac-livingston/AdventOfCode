using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2023.Day20.Models
{
    internal class Conjunction : BaseComponent
    {
        private Dictionary<string, int> Memory { get; } = [];

        public void RegisterComponentWithMemory(string id)
        {
            Memory.Add(id, LOW_PULSE);
        }

        public override void ReceivePulse(int pulse, string? from = null)
        {
            ArgumentNullException.ThrowIfNull(from);

            if (!Memory.ContainsKey(from))
            {
                throw new ArgumentException($"Component with id {from} is not registered with this conjunction.");
            }

            Memory[from] = pulse;

            if (Memory.All(x => x.Value == HIGH_PULSE))
            {
                
            }
            else
            {

            }
        }

        public override void SendPulses()
        {
            throw new NotImplementedException();
        }
    }
}
