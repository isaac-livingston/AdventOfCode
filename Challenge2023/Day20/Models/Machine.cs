namespace Challenge2023.Day20.Models
{
    internal class Machine
    {
        public Dictionary<string, BaseComponent> Components { get; } = [];

        public Queue<Action> Actions { get; private set; } = new Queue<Action>();

        public (long highs, long lows) CycleMachine(int startPulse)
        {
            var newActions = new Queue<Action>();

            Components["broadcaster"].ReceivePulse(startPulse, "broadcaster", newActions);

            foreach (var action in newActions)
            {
                Actions.Enqueue(action);
            }

            while (Actions.Count > 0)
            {
                var action = Actions.Dequeue();
                action();
            }

            var l = BaseComponent.LowPulseCount;
            var h = BaseComponent.HighPulseCount;

            return (h, l);
        }
    }
}
