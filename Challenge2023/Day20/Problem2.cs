using Challenge2023.Common;
using Challenge2023.Day20.Models;

namespace Challenge2023.Day20;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        Console.WriteLine("Reading inputs...");
        var inputs = GetInputs(folder: DAY_FOLDER, useTest: false);

        stopwatch.Start();

        Console.WriteLine("Building the machine...");
        BuildMachine(inputs);

        if (Machine == null)
        {
            Console.WriteLine("Machine is null!");
            return;
        }

        var outputer = Machine.GetOutputer();
        if (outputer == null)
        {
            Console.WriteLine("outputer is null!");
            return;
        }

        Console.WriteLine("Creating an Observer...");
        var observer = new Observer();

        Console.WriteLine($"Outputer: {outputer.Id}");

        var outputterInputComponents = Machine.GetInputComponentsFor(outputer);

        if (outputterInputComponents.Count == 0)
        {
            Console.WriteLine("No input components for outputer!");
            return;
        }

        foreach(var inputComponent in outputterInputComponents)
        {
            Console.WriteLine($"Input component: {inputComponent.Id}, which is a {inputComponent.GetType().Name}");

            if (inputComponent is Conjunction conjunction)
            {
                var conjunctionInputComponents = Machine.GetInputComponentsFor(conjunction);
                foreach(var conjunctionInputComponent in conjunctionInputComponents)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Conjunction input component: {conjunctionInputComponent.Id}, which is a {conjunctionInputComponent.GetType().Name}");
                    Console.WriteLine($"creating an observation for {conjunctionInputComponent.Id} to look for the first High Pulse occurrence...");
                    var observation = new Observation(() => { return conjunctionInputComponent.HighPulseOccuredAt != null; }, 
                                                      () => { return conjunctionInputComponent.HighPulseOccuredAt.GetValueOrDefault(); });

                    observer.AddObservation(observation);
                }
            }
        }

        var solution = 0L;
        var iter = 1000000;

        Console.WriteLine("");
        Console.WriteLine($"Pushing the button, reporting every {iter:N0} pushes...");
        while (true)
        {
            _ = Machine.PusbButton();
            solution++;

            var (observedAll, observationResults) = observer.Observe();

            if (observedAll)
            {
                Console.WriteLine("");
                Console.WriteLine($"All observations have been observed!");
                Console.WriteLine($"Observation results: {string.Join(", ", observationResults)}");
                double lcm = 1L;
                foreach(var obs in observationResults)
                {
                    var lobs = (long)obs;
                    Console.WriteLine($"{lcm} * {lobs} = {lcm * lobs}");
                    lcm = lcm * (long)obs;
                }
                Console.WriteLine($"LCM: {lcm:N0}");
                break;
            }

            if (solution % iter == 0)
            {
                Console.WriteLine("");
                ConsoleTools.PrintIterationMessage("pushes", solution, stopwatch);
            }
        }

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}
