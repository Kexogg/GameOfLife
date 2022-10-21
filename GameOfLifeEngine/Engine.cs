namespace GameOfLifeEngine;

public class GameOfLifeField
{
    private readonly bool[,] _grid;

    /// <summary>
    ///     Initializes a new instance of <see cref="GameOfLifeField" /> . Field is empty by default
    /// </summary>
    /// <param name="width">width of the field</param>
    /// <param name="height">height of the field</param>
    /// <exception cref="ArgumentOutOfRangeException">width or height is less than 1</exception>
    public GameOfLifeField(int width, int height)
    {
        if (width < 1 || height < 1) throw new ArgumentOutOfRangeException(width < 1 ? nameof(width) : nameof(height));
        _grid = new bool[width, height];
    }

    /// <inheritdoc cref="ProcessIteration()" />
    /// <param name="changes">array of changes</param>
    public void ProcessIteration(int[][] changes)
    {
        foreach (var coordinates in changes)
            FlipCell(coordinates[0], coordinates[1]);
    }

    /// <inheritdoc cref="GameOfLifeField" />
    /// <summary>
    ///     Updates <see cref="GameOfLifeField" />
    /// </summary>
    public void ProcessIteration()
    {
        ProcessIteration(CalculateChanges());
    }

    /// <summary>
    ///     Calculates difference between current and next iteration of <see cref="GameOfLifeField" />
    /// </summary>
    /// <returns>array of changes</returns>
    public int[][] CalculateChanges()
    {
        var changes = new List<int[]>();
        for (var x = 0; x < _grid.GetLength(0); x++)
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

        return changes.ToArray();
    }

    /// <summary>
    ///     Returns 32-bit integer which represents amount of elements in the specified dimension
    ///     <see cref="GameOfLifeField" />
    /// </summary>
    /// <param name="dimension">dimension of the field</param>
    /// <returns>amount of elements in specified dimension</returns>
    public int GetLength(int dimension)
    {
        return _grid.GetLength(dimension);
    }

    /// <inheritdoc cref="GetLength(int)" />
    /// <exception cref="ArgumentOutOfRangeException">dimension param does not math any dimension</exception>
    public int GetLength(char dimension)
    {
        return dimension switch
        {
            'x' or 'X' => GetLength(0),
            'y' or 'Y' => GetLength(1),
            _ => throw new ArgumentOutOfRangeException(nameof(dimension))
        };
    }

    /// <summary>
    ///     Gets state of a cell in <see cref="GameOfLifeField" />
    /// </summary>
    /// <param name="coordinateX">coordinate X of a cell</param>
    /// <param name="coordinateY">coordinate Y of a cell</param>
    /// <returns>state of a cell</returns>
    public bool GetCell(int coordinateX, int coordinateY)
    {
        if (coordinateX < 0)
            coordinateX = _grid.GetLength(0) - 1;
        if (coordinateX == _grid.GetLength(0))
            coordinateX = 0;
        if (coordinateY < 0)
            coordinateY = _grid.GetLength(1) - 1;
        if (coordinateY == _grid.GetLength(0))
            coordinateY = 0;
        return _grid[coordinateX, coordinateY];
    }

    /// <summary>
    ///     Flips cell in <see cref="GameOfLifeField" />
    /// </summary>
    /// <param name="coordinateX">coordinate X of a cell</param>
    /// <param name="coordinateY">coordinate Y of a cell</param>
    public void FlipCell(int coordinateX, int coordinateY)
    {
        _grid[coordinateX, coordinateY] = !_grid[coordinateX, coordinateY];
    }

    /// <summary>
    ///     Sets cell in <see cref="GameOfLifeField" />
    /// </summary>
    /// <param name="state">state of a cell</param>
    /// <param name="coordinateX">coordinate X of a cell</param>
    /// <param name="coordinateY">coordinate Y of a cell</param>
    public void SetCell(int coordinateX, int coordinateY, bool state)
    {
        _grid[coordinateX, coordinateY] = state;
    }

    /// <inheritdoc cref="FromString(string)" />
    /// <param name="separators">array of separation characters</param>
    public void FromString(string data, char[] separators)
    {
        var rows = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        for (var y = 0; y < rows.Length; y++)
        for (var x = 0; x < rows[y].Length; x++)
            SetCell(x, y, rows[y][x] == '1');
    }

    /// <inheritdoc cref="FromString(string)" />
    /// <param name="separator">separation character</param>
    public void FromString(string data, char separator)
    {
        FromString(data, new[] { separator });
    }

    /// <summary>
    ///     Sets <see cref="GameOfLifeField" /> from string of ones and zeroes, where 0 - dead cell, 1 - alive cell
    /// </summary>
    /// <param name="data">string of data</param>
    public void FromString(string data)
    {
        FromString(data, new[] { ';', ',', ' ', '\t' });
    }

    /// <summary>
    ///     Dumps <see cref="GameOfLifeField" /> into string
    /// </summary>
    /// <param name="separator">row separator</param>
    /// <returns>string with data</returns>
    public string ToString(char separator = ';')
    {
        var result = "";
        for (var y = 0; y < GetLength(1); y++)
        {
            for (var x = 0; x < GetLength(0); x++) result += GetCell(x, y) ? "1" : "0";
            if (y != GetLength(1) - 1) result += separator;
        }

        return result;
    }

    /// <summary>
    ///     Checks for any alive (non-empty) cells inside of <see cref="GameOfLifeField" />
    /// </summary>
    /// <returns>presence of alive cells</returns>
    public bool HasAliveCells()
    {
        return _grid.Cast<bool>().Any(cell => cell);
    }
}