namespace Challenge2023.Day20.Models
{
    internal class Observer
    {
        private readonly List<Observation> _observations = [];

        public void AddObservation(Observation observation)
        {
            ArgumentNullException.ThrowIfNull(observation);

            _observations.Add(observation);
        }

        public (bool observedAll, List<object> observationOutput) Observe()
        {
            var observedAll = true;
            var observationOutput = new List<object>();

            foreach (var observation in _observations)
            {
                var observed = observation.Observed();

                if (observed)
                {
                    observationOutput.Add(observation.WhatToDoWhenObserved());
                }

                observedAll &= observed;
            }

            return (observedAll, observationOutput);
        }
    }
}
