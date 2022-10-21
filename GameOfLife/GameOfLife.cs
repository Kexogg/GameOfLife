using GameOfLifeEngine;

namespace GameOfLife;

internal static class GameOfLife

{
    private static void Main()
    {
        var maxIterations = -1;
        var delay = 500;
        GameOfLifeField field = null;
        string errorMessage = null;
        while (true)
        {
            PrintMenu(field, delay, maxIterations);
            if (errorMessage != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errorMessage);
                Console.ResetColor();
            }

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    field = SetupField();
                    break;
                case "2":
                    if (field != null)
                    {
                        EditField(field);
                    }
                    else
                    {
                        errorMessage = "Unavailable when dimensions are not set";
                        continue;
                    }

                    break;
                case "3":
                    if (field != null)
                    {
                        field.FromString(GetInput("Enter data string"));
                    }
                    else
                    {
                        errorMessage = "Unavailable when dimensions are not set";
                        continue;
                    }

                    break;
                case "4":
                    int.TryParse(GetInput("Set maximum amount of iterations"), out maxIterations);
                    break;
                case "5":
                    int.TryParse(GetInput("Set game delay"), out delay);
                    break;
                case "6":
                    if (field != null)
                        Game(field, maxIterations, delay);
                    field = new GameOfLifeField(field.GetLength(0), field.GetLength(1));
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    errorMessage = "Invalid input!";
                    continue;
            }

            errorMessage = null;
        }
    }

    private static void PrintMenu(GameOfLifeField field, int delay, int maxIterations)
    {
        string dimensions = null;
        Console.Clear();
        Console.WriteLine("Game of life\nSelect action and press enter:");
        if (field != null) dimensions = $" ({field.GetLength(0)}X{field.GetLength(1)})";
        Console.WriteLine("1. Set field dimensions" + dimensions);
        if (field == null) Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("2. Edit field");
        Console.WriteLine("3. Input field from string");
        if (field == null) Console.ResetColor();
        Console.WriteLine($"4. Set maximum amount of iterations ({maxIterations})");
        Console.WriteLine($"5. Set game delay ({delay}ms)");
        Console.WriteLine("6. Start game");
        Console.WriteLine("0. Exit");
    }

    private static string GetInput(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        var input = Console.ReadLine();
        return input;
    }

    private static void Game(GameOfLifeField field, int maxIterations, int delay)
    {
        Console.Clear();
        PrintField(field);
        for (var i = 1; i != maxIterations; i++)
        {
            field.ProcessIteration();
            Console.SetCursorPosition(0, 0);
            PrintField(field);
            Console.WriteLine($"Iteration {i}/{maxIterations}");
            if (!field.HasAliveCells()) break;
            Thread.Sleep(delay);
        }

        Console.WriteLine("Game finished. Press enter to return to menu or 'q' to exit");
        while (true)
        {
            var input = Console.ReadKey().Key;
            switch (input)
            {
                case ConsoleKey.Enter:
                    return;
                case ConsoleKey.Q:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private static GameOfLifeField SetupField()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Set field dimensions");
            var userInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
            if (userInput.Length != 2) continue;
            var field = new GameOfLifeField(userInput[0], userInput[1]);
            return field;
        }
    }


    private static void EditField(GameOfLifeField field)
    {
        var errorMessage = "";
        while (true)
        {
            Console.Clear();
            PrintField(field);
            Console.WriteLine(errorMessage + "Enter zero-based coordinates or press enter to return to menu");
            var input = Console.ReadLine();
            if (input == "") break;
            try
            {
                var coordinates = input.Split().Select(int.Parse).ToArray();
                field.FlipCell(coordinates[0], coordinates[1]);
            }
            catch
            {
                errorMessage = "Invalid input! ";
                continue;
            }

            errorMessage = "";
        }
    }

    private static void PrintField(GameOfLifeField field)
    {
        for (var coordinateY = field.GetLength(1) - 1; coordinateY >= 0; coordinateY--)
        {
            for (var coordinateX = 0; coordinateX < field.GetLength(0); coordinateX++)
            {
                Console.Write(field.GetCell(coordinateX, coordinateY) ? "*" : "-");
                Console.Write(' ');
            }

            Console.WriteLine();
        }
    }
}