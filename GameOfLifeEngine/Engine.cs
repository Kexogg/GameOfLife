namespace GameOfLifeEngine;

public class GameOfLifeEngine
{
    public static void ProcessIteration(int[][] changes, bool[,] grid)
    {
        foreach (var coordinates in changes)
        {
            grid[coordinates[0], coordinates[1]] = !grid[coordinates[0], coordinates[1]];
        }
    }
    
    public static int[][] CalculateChanges(bool[,] grid)
    {
        var changes = new List<int[]>();
        for (var i = 0; i < grid.GetLength(0); i++)
        {
            for (var j = 0; j < grid.GetLength(1); j++)
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

    private static bool ParseCell(int coordinateX, int coordinateY, bool[,] grid)
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
}