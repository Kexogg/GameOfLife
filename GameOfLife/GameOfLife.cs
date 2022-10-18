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
        var maxIterationsCount = int.Parse(Console.ReadLine() ?? "-1");
        for (var i = 1; i != maxIterationsCount; i++)
        {
            field.ProcessIteration(field.CalculateChanges());
            if (!field.HasAliveCells()) break;
        }

        PrintField(field);
        Console.WriteLine("Game finished");
    }

    private static GameOfLifeField SetupField()
    {
        var userInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
        Console.WriteLine("Set dimensions");
        var field = new GameOfLifeField(userInput[0], userInput[1]);
        SetStartingCells(field);
        return field;
    }


    private static void SetStartingCells(GameOfLifeField field)
    {
        Console.Clear();
        Console.WriteLine("Set amount of starting cells");
        var amountOfCells = int.Parse(Console.ReadLine() ?? "0");
        for (var i = 0; i < amountOfCells; i++)
        {
            Console.Clear();
            Console.WriteLine($"Set coordinates for cell ({i + 1}/{amountOfCells}) and press RETURN");
            var coordinates = Console.ReadLine().Split().Select(int.Parse).ToArray();
            field.FlipCell(coordinates[0], coordinates[1]);
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