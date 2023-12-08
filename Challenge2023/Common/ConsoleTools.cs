namespace Challenge2023.Common
{
    internal class ConsoleTools
    {
        public static void PrintExceptionMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void PrintSolutionMessage(object solution)
        {
            Console.WriteLine("");
            Console.Write("solution: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(solution);
            Console.ResetColor();
        }

        public static void PrintAttributionMessage(object attribution)
        {
            Console.WriteLine();
            Console.Write("special thanks and recognition to: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(attribution);
            Console.ResetColor();
            Console.Write(", who's work was used in some part to arrive at this solution.");
            Console.WriteLine("");
        }

        public static void PrintDurationMessage(object duration, string unit = "ms")
        {
            Console.WriteLine();
            Console.Write("duration: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(duration);
            Console.ResetColor();
            Console.Write(" " + unit);
        }
    }
}
