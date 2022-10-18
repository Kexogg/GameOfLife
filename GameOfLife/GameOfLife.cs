using GameOfLifeEngine;

namespace GameOfLife;

internal static class GameOfLife

{
    private static void Main()
    {
        var field = SetupField();
        SetStartingCells(field);
        Console.Clear();
        
        PrintField(field);
        Console.WriteLine("Starting field set\nEnter maximum amount of iterations (-1 for infinity)");
        if (!int.TryParse(Console.ReadLine(), out var maxIterationsCount)) maxIterationsCount = -1;
        Console.Clear();
        PrintField(field);
        Console.WriteLine("Set sleep time (in milliseconds)");
        if (!int.TryParse(Console.ReadLine(), out var sleepTime)) sleepTime = 0;
        Console.Clear();
        PrintField(field);
        for (var i = 1; i != maxIterationsCount; i++)
        {
            field.ProcessIteration(field.CalculateChanges());
            Console.SetCursorPosition(0, 0);
            PrintField(field);
            Console.WriteLine($"Iteration {i}/{maxIterationsCount}");
            if (!field.HasAliveCells()) break;
            Thread.Sleep(sleepTime);
        }

        Console.WriteLine("Game finished");
    }

    private static GameOfLifeField SetupField()
    {
        getInput:
        Console.Clear();
        Console.WriteLine("Set field dimensions");
        var userInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
        if (userInput.Length != 2) goto getInput;
        var field = new GameOfLifeField(userInput[0], userInput[1]);
        return field;
    }


    private static void SetStartingCells(GameOfLifeField field)
    {
        getInput:
        Console.Clear();
        Console.WriteLine("Set starting cells [Y/n/from (S)tring]?");
        var userInput = Console.ReadLine().ToLower();
        if (userInput is "n") return;
        if (userInput is "s")
        {
            Console.Clear();
            Console.WriteLine("Enter starting cells");
            var startingCells = Console.ReadLine();
            field.FromString(startingCells);
        }
        else if (userInput is not ("y" or "")) goto getInput;
        else
        {
            var errorMessage = "";
            while (true)
            {
                Console.Clear();
                PrintField(field);
                Console.WriteLine(errorMessage + "Enter coordinates for cell or press enter to return");
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