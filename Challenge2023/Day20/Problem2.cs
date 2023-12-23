using Challenge2023.Common;
using Challenge2023.Day20.Models;
using Challenge2023.Day20.Models.ComponentModels;

namespace Challenge2023.Day20;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        Console.WriteLine("");
        Console.WriteLine("Reading inputs...");
        var inputs = GetInputs(folder: DAY_FOLDER, useTest: false);

        stopwatch.Start();

        Console.WriteLine("");
        Console.WriteLine($"Building a {typeof(Machine).Name}...");
        BuildMachine(inputs);

        if (Machine == null)
        {
            Console.WriteLine($"{typeof(Machine).Name} is null!");
            return;
        }

        Console.WriteLine("");
        Console.WriteLine($"Finding the {typeof(Machine).Name}'s {typeof(Outputer).Name}...");

        var outputer = Machine.GetOutputer();
        if (outputer == null)
        {
            Console.WriteLine($"{typeof(Outputer).Name} is null!");
            return;
        }

        Console.WriteLine($"{typeof(Outputer).Name}: [{outputer.Id}]");

        var outputterInputComponents = Machine.GetInputComponentsFor(outputer);

        if (outputterInputComponents.Count == 0)
        {
            Console.WriteLine($"No input components for {typeof(Outputer).Name}!");
            return;
        }

        Console.WriteLine("");
        Console.WriteLine($"I need to determine when a {nameof(BaseComponent.LOW_PULSE)} will be sent to {typeof(Outputer).Name} [{outputer.Id}]");
        Console.WriteLine("");
        Console.WriteLine($"If the input(s) to the {typeof(Outputer).Name} are {typeof(Conjunction).Name}, then they must remember all {nameof(BaseComponent.HIGH_PULSE)}");
        Console.WriteLine($"from their inputs in order to send a {nameof(BaseComponent.LOW_PULSE)} to the {typeof(Outputer).Name}...");
        Console.WriteLine("");
        Console.WriteLine("I should observe those...");
        
        Console.WriteLine("");
        Console.WriteLine($"Creating an {typeof(Observer).Name}...");
        var observer = new Observer();

        foreach (var inputComponent in outputterInputComponents)
        {
            Console.WriteLine("");
            Console.WriteLine($"[{outputer.Id}] has input component: [{inputComponent.Id}], which is a {inputComponent.GetType().Name}");

            if (inputComponent is Conjunction conjunction)
            {
                Console.WriteLine("");
                Console.WriteLine($"Setting up {typeof(Observation).Name}s for the inputs to [{inputComponent.Id}]...");
                Console.WriteLine("");
                var conjunctionInputComponents = Machine.GetInputComponentsFor(conjunction);
                foreach (var conjunctionInputComponent in conjunctionInputComponents)
                {
                    Console.WriteLine($"observing [{conjunctionInputComponent.Id}] -[{nameof(BaseComponent.HIGH_PULSE)}]-> [{inputComponent.Id}], returning the count of button pushes when seen...");
                    var observation = new Observation(() => { return conjunctionInputComponent.NotablePulseObservedAtPushCount != null; },
                                                      () => { return conjunctionInputComponent.NotablePulseObservedAtPushCount.GetValueOrDefault(); });

                    observer.AddObservation(observation);
                }
            }
        }

        Console.WriteLine("");
        Console.WriteLine($"Pushing button until {typeof(Observation).Name}s are complete... hope it doesn't take 17 years... brb");

        (bool complete, List<object> data) observations = (false, []);
        while (!observations.complete)
        {
            _ = Machine.PusbButton();
            observations = observer.Observe();
        }

        Console.WriteLine("");
        Console.WriteLine($"All {typeof(Observation).Name}s complete! [{string.Join(", ", observations.data)}]");
        
        var solution = 1L;
        foreach (var obs in observations.data)
        {
            solution *= (long)obs;
        }

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution} (LCM = {string.Join(" * ", observations.data)})");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}
