namespace GameOfLife;

class GameOfLife
{
    static void Main()
    {
        var grid = SetupGrid();
        PrintGrid(grid);
        for (int i = 0; i < 200; i++)
        {
            ProcessIteration(CalculateChanges(grid), grid);
            if (!CheckForAliveCells(grid)) break;
        }
        Console.WriteLine("Game finished");
        PrintGrid(grid);
    }
    public static bool[,] SetupGrid()
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

    public static bool CheckForAliveCells(bool[,] grid)
    {
        var aliveCells = 0;
        foreach (var cell in grid)
        {
            if (cell)
                aliveCells++;
        }

        return aliveCells != 0;
    }
    
    public static int[][] CalculateChanges(bool[,] grid)
    {
        var changes = new List<int[]>();
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                var neighbors = 0;
                for (int k = 0; k < 3; k++)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        if (k == 1 & l == 1) continue;
                        if (ParseCell(i + k - 1, j + l - 1, grid))
                            neighbors++;
                    }
                }
                if (neighbors is not (2 or 3) && grid[i, j])
                    changes.Add(new []{i, j});
                else if (neighbors is 3 && !grid[i, j])
                    changes.Add(new []{i, j});
            }
        }
        return changes.ToArray();
    }

    public static bool ParseCell(int coordinateX, int coordinateY, bool[,] grid)
    {
        if (coordinateX < 0)
            coordinateX = grid.GetLength(0) - 1;
        else if (coordinateX == grid.GetLength(0))
            coordinateX = 0;
        if (coordinateY < 0)
            coordinateY = grid.GetLength(1) - 1;
        else if (coordinateY == grid.GetLength(0))
            coordinateY = 0;
        return grid[coordinateX, coordinateY];
    }

    public static void ProcessIteration(int[][] changes, bool[,] grid)
    {
        foreach (var coordinates in changes)
        {
            grid[coordinates[0], coordinates[1]] = !grid[coordinates[0], coordinates[1]];
        }
    }

    public static void PrintGrid(bool[,] grid)
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