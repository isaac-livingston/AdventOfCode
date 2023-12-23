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

        public (long highs, long lows) PusbButton()
        {
            CycleMachine(BaseComponent.LOW_PULSE, from: "button");

            var l = BaseComponent.LowPulseCount;
            var h = BaseComponent.HighPulseCount;

            return (h, l);
        }

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

        private void CycleMachine(int pulse, string from)
        {
            _broadcaster.ReceivePulse(pulse, from, Actions);

            while (Actions.Count > 0)
            {
                var action = Actions.Dequeue();
                action();
            }
        }
    }
}
