namespace GameOfLifeEngine;

public class GameOfLifeEngine
{
    private readonly bool[,] _grid;

    public GameOfLifeEngine(int width, int height)
    {
        _grid = new bool[width, height];
    }


    public void ProcessIteration(int[][] changes)
    {
        foreach (var coordinates in changes)
            _grid[coordinates[0], coordinates[1]] = !_grid[coordinates[0], coordinates[1]];
    }

    public int[][] CalculateChanges()
    {
        var changes = new List<int[]>();
        for (var x = 0; x < _grid.GetLength(0); x++)
        {
            for (var y = 0; y < _grid.GetLength(1); y++)
            {
                var neighbors = 0;
                for (var k = 0; k < 3; k++)
                for (var l = 0; l < 3; l++)
                {
                    if ((k == 1) & (l == 1)) continue;
                    if (GetCell(x + k - 1, y + l - 1))
                        neighbors++;
                }

                if (neighbors is not (2 or 3) && _grid[x, y])
                    changes.Add(new[] { x, y });
                else if (neighbors is 3 && !_grid[x, y])
                    changes.Add(new[] { x, y });
            }
        }

        return changes.ToArray();
    }

    public int GetLength(int dimension)
    {
        return _grid.GetLength(dimension);
    }

    public bool GetCell(int coordinateX, int coordinateY)
    {
        if (coordinateX < 0)
            coordinateX = _grid.GetLength(0) - 1;
        else if (coordinateX == _grid.GetLength(0))
            coordinateX = 0;
        if (coordinateY < 0)
            coordinateY = _grid.GetLength(1) - 1;
        else if (coordinateY == _grid.GetLength(0))
            coordinateY = 0;
        return _grid[coordinateX, coordinateY];
    }

    public void FlipCell(int coordinateX, int coordinateY)
    {
        _grid[coordinateX, coordinateY] = !_grid[coordinateX, coordinateY];
    }
    public void SetCell(int coordinateX, int coordinateY, bool state)
    {
        _grid[coordinateX, coordinateY] = state;
    }

    /// <summary>
    /// Set game field from string of ones and zeroes, where 0 - dead cell, 1 - alive cell. Rows are separated by defined character
    /// </summary>
    /// <param name="data">String</param>
    /// <param name="separator">Separation character</param>
    public void FromString(string data, char separator = ';')
    {
        var rows = data.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        for (var y = 0; y < rows.Length; y++)
        {
            for (var x = 0; x < rows[y].Length; x++)
            {
                _grid[x, y] = rows[y][x] == '1';
            }
        }
    }

    public string ToString(char separator = ';')
    {
        var result = "";
        for (var y = 0; y < _grid.GetLength(1); y++)
        {
            for (var x = 0; x < _grid.GetLength(0); x++)
            {
                result += _grid[x, y] ? "1" : "0";
            }
            if (y != _grid.GetLength(1) - 1) result += separator;
        }
        return result;
    }

    public bool HasAliveCells()
    {
        return _grid.Cast<bool>().Any(cell => cell);
    }
}