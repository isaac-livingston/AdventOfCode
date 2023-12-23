using Challenge2023.Common;
using Challenge2023.Day20.Models;
using Challenge2023.Day20.Models.ComponentModels;

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

        foreach (var inputComponent in outputterInputComponents)
        {
            Console.WriteLine("");
            Console.WriteLine($"[{outputer.Id}] has input component: [{inputComponent.Id}], which is a {inputComponent.GetType().Name}");

            if (inputComponent is Conjunction conjunction)
            {
                Console.WriteLine("");
                Console.WriteLine($"Setting up observers for the inputs to [{inputComponent.Id}]...");
                Console.WriteLine("");
                var conjunctionInputComponents = Machine.GetInputComponentsFor(conjunction);
                foreach (var conjunctionInputComponent in conjunctionInputComponents)
                {
                    Console.WriteLine($"observing [{conjunctionInputComponent.Id}] -[HIGH]-> [{inputComponent.Id}], returning the count of button pushes when seen...");
                    var observation = new Observation(() => { return conjunctionInputComponent.NotablePulseObservedAtPushCount != null; },
                                                      () => { return conjunctionInputComponent.NotablePulseObservedAtPushCount.GetValueOrDefault(); });

                    observer.AddObservation(observation);
                }
            }
        }

        var solution = 0L;

        Console.WriteLine("");
        Console.WriteLine("Pushing button until observations are complete... hope it doesn't take 17 years... brb");

        (bool complete, List<object> data) observations = (false, []);
        while (!observations.complete)
        {
            _ = Machine.PusbButton();
            observations = observer.Observe();
        }

        Console.WriteLine("");
        Console.WriteLine($"All observations complete! [{string.Join(", ", observations.data)}]");
        solution = 1L;
        foreach (var obs in observations.data)
        {
            solution *= (long)obs;
        }

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution} (LCM = {string.Join(" * ", observations.data)})");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}
