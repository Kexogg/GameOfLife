using GameOfLifeEngine;

namespace GameOfLife;

internal static class GameOfLife

{
    private static void Main()
    {
        var field = SetupField();
        Console.Clear();
        PrintField(field);
        Console.WriteLine("Starting field set\nEnter maximum amount of iterations (-1 for infinity)");
        if (!int.TryParse(Console.ReadLine(), out var maxIterationsCount)) maxIterationsCount = - 1;
        Console.Clear();
        PrintField(field);
        Console.WriteLine("Set sleep time (in milliseconds)");
        if (!int.TryParse(Console.ReadLine(), out var sleepTime)) sleepTime = 0;
        Console.Clear();
        PrintField(field);
        for (var i = 1; i != maxIterationsCount; i++)
        {
            field.ProcessIteration(field.CalculateChanges());
            Console.Clear();            
            PrintField(field);
            Console.WriteLine($"Iteration {i}/{maxIterationsCount}");
            if (!field.HasAliveCells()) break;
            Thread.Sleep(sleepTime);
        }
        Console.WriteLine("Game finished");
    }

    private static GameOfLifeField SetupField()
    {
        Console.Clear();
        Console.WriteLine("Set field dimensions");
        var userInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
        var field = new GameOfLifeField(userInput[0], userInput[1]);
        SetStartingCells(field);
        return field;
    }


    private static void SetStartingCells(GameOfLifeField field)
    {
        Console.Clear();
        Console.WriteLine("Set amount of starting cells (-1 to disable limit)");
        var amountOfCells = int.Parse(Console.ReadLine() ?? "0");
        for (var i = 0; i != amountOfCells; i++)
        {
            Console.Clear();
            PrintField(field);
            Console.WriteLine($"Enter coordinates for cell ({i + 1}/{amountOfCells})");
            var input = Console.ReadLine();
            if (input == "" && amountOfCells == -1) break;
            try
            {
                var coordinates = input.Split().Select(int.Parse).ToArray();
                field.FlipCell(coordinates[0], coordinates[1]);
            }
            catch
            {
                i--;
            }
        }
    }

    private static void PrintField(GameOfLifeField field)
    {
        for (var x = 0; x < field.GetLength(0); x++)
        {
            for (var y = 0; y < field.GetLength(1); y++)
            {
                Console.Write(field.GetCell(x, y) ? "*" : "-");
                Console.Write(' ');
            }

            Console.WriteLine();
        }
    }
}