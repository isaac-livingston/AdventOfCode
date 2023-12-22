namespace Challenge2023.Day20.Models
{
    internal class Observation(Func<bool> howToObserve, Func<object> whatToDoWhenObserved)
    {
        public Func<bool> Observed { get; } = howToObserve
                                           ?? throw new ArgumentNullException(nameof(howToObserve));

        public Func<object> WhatToDoWhenObserved { get; } = whatToDoWhenObserved
                                                         ?? throw new ArgumentNullException(nameof(whatToDoWhenObserved));
    }
}
