
int day = 6;
int problem = 2;

if (args.Length == 2)
{
    day = int.Parse(args[0]);
    problem = int.Parse(args[1]);
}

switch (day, problem)
{
    case (1, 1):
    case (1, 2):
        new Challenge2023.Day01.Problem2().RunSolution();
        break;
    case (2, 1):
        new Challenge2023.Day02.Problem1().RunSolution();
        break;
    case (2, 2):
        new Challenge2023.Day02.Problem2().RunSolution();
        break;
    case (3, 1):
        new Challenge2023.Day03.Problem1().RunSolution();
        break;
    case (3, 2):
        new Challenge2023.Day03.Problem2().RunSolution();
        break;
    case (4, 1):
        new Challenge2023.Day04.Problem1().RunSolution();
        break;
    case (4, 2):
        new Challenge2023.Day04.Problem2().RunSolution();
        break;
    case (5, 1):
        new Challenge2023.Day05.Problem1().RunSolution();
        break;
    case (5, 2):
        new Challenge2023.Day05.Problem2().RunSolution();
        break;
    case (6, 1):
        new Challenge2023.Day06.Problem1().RunSolution();
        break;
    case (6, 2):
        new Challenge2023.Day06.Problem2().RunSolution();
        break;

}

Console.WriteLine();
Console.WriteLine("fin.");