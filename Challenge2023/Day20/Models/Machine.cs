using System.Collections.Frozen;

namespace Challenge2023.Day20.Models
{
    internal class Machine(Dictionary<string, BaseComponent> components)
    {
        public FrozenDictionary<string, BaseComponent> Components { get; } = components?.ToFrozenDictionary()
                                                                             ?? throw new ArgumentNullException(nameof(components));

        public Queue<Action> Actions { get; private set; } = new Queue<Action>();

        private readonly Broadcaster _broadcaster = components?.Values.Where(c => c is Broadcaster)
                                                                      .Select(c => c as Broadcaster)
                                                                      .Single() ?? throw new ArgumentNullException(nameof(components));

        public Outputer? GetOutputer()
        {
            return Components.Values.Where(c => c is Outputer)
                                    .Select(c => c as Outputer)
                                    .SingleOrDefault();
        }

        public List<BaseComponent> GetInputComponentsFor(BaseComponent? component)
        {
            if (component == null)
            {
                return [];
            }

            return Components.Values.Where(c => c.ConnectedComponents.Contains(component))
                                    .ToList();
        }

        public long GetLowPulseCountForAllComponents()
        {
            return Components.Values.Sum(c => c.LowPulseCount);
        }

        public long GetHighPulseCountForAllComponents()
        {
            return Components.Values.Sum(c => c.HighPulseCount);
        }

        private long _pushCount = 0L;

        public (long highs, long lows) PusbButton()
        {
            _pushCount++;

            CycleMachine(BaseComponent.LOW_PULSE, from: "button", _pushCount);

            var lowPulseCount = GetLowPulseCountForAllComponents();
            var hightPulseCount = GetHighPulseCountForAllComponents();

            return (hightPulseCount, lowPulseCount);
        }

        private void CycleMachine(int pulse, string from, long pushCount)
        {
            _broadcaster.ReceivePulse(pulse, from, Actions, pushCount);

            while (Actions.Count > 0)
            {
                var action = Actions.Dequeue();
                action();
            }
        }
    }
}
