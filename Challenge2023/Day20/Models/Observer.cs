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

        public (bool observedAll, List<object> observationResults) Observe()
        {
            var observedAll = true;
            var observationResults = new List<object>();

            foreach (var observation in _observations)
            {
                var observed = observation.Observed();

                if (observed)
                {
                    observationResults.Add(observation.WhatToDoWhenObserved());
                }

                observedAll &= observed;
            }

            return (observedAll, observationResults);
        }
    }
}
