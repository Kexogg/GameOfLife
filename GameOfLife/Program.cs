namespace GameOfLife;

using GameOfLifeEngine;
class GameOfLife
{
    static void Main()
    {
        var grid = SetupGrid();
        PrintGrid(grid);
        for (int i = 0; i < 200; i++)
        {
            GameOfLifeEngine.ProcessIteration(GameOfLifeEngine.CalculateChanges(grid), grid);
            if (!GameOfLifeEngine.CheckForAliveCells(grid)) break;
        }
        Console.WriteLine("Game finished");
        PrintGrid(grid);
    }

    private static bool[,] SetupGrid()
    {
        var userInput = GetDimensions();
        var grid = new bool[userInput[0], userInput[1]];
        SetStartingCells(grid);
        return grid;
    }
    
    private static int[] GetDimensions()
    {
        Console.WriteLine("Set dimensions");
        return Console.ReadLine().Split().Select(Int32.Parse).ToArray();
    }

    private static void SetStartingCells(bool[,] grid)
    {
        Console.WriteLine("Set amount of starting cells");
        var amountOfCells = int.Parse(Console.ReadLine());
        Console.WriteLine("Set coordinates for cell and press RETURN");
        for (int i = 0; i < amountOfCells; i++)
        {
            var coordinates = Console.ReadLine().Split().Select(Int32.Parse).ToArray();
            grid[coordinates[0], coordinates[1]] = true;
        }
    }

    private static void PrintGrid(bool[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j])
                    Console.Write("* ");
                else Console.Write("- ");
            }
            Console.WriteLine();
        }
    }
    
}