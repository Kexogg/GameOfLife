using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace TestOfLife
{
    [TestFixture]
    public class Tests
    {
        
        [TestCase(false, false)]
        [TestCase(true, true)]
        public void TestForAliveCells(bool a, bool expected)
        {
            var grid = new [,] {{false, false}, {false, a}};
            
            Assert.AreEqual(expected, GameOfLifeEngine.GameOfLifeEngine.CheckForAliveCells(grid));
        }
        [TestCase(0, -1, false)]
        [TestCase(-1, -1, true)]
        public void TestForOverflowCoordinate(int coordinateX, int coordinateY, bool expected)
        {
            var grid = new [,] {{false, false}, {false, true}};
            Assert.AreEqual(expected, GameOfLifeEngine.GameOfLifeEngine.ParseCell(coordinateX, coordinateY, grid));
        }
        
        [TestCase(false, false, false, false, new int[] {1, 1}, false, false, false, true)]
        [TestCase(false, false, false, true, new int[] {1, 1}, false, false, false, false)]

        public void TestForIterationProcessing(bool a, bool b, bool c, bool d, int[] changes, bool a1, bool b1, bool c1, bool d1)
        {
            var grid = new [,] {{a, b}, {c, d}};
            var expectedGrid = new [,] {{a1, b1}, {c1, d1}};
            GameOfLifeEngine.GameOfLifeEngine.ProcessIteration(new []{changes}, grid);
            Assert.AreEqual(expectedGrid, grid);
        }

        [TestCase("0 0 0 0 1 0 0 0 0", "0 0 0 0 0 0 0 0 0")]
        public void TestForChanges(string gridRaw, string expectedChangesRaw)
        {
            var grid = new bool[3, 3];
            var expectedChanges = new List<int[]>();
            for (var index = 0; index < gridRaw.Split().Length; index++)
            {
                grid[index / (3 + 1), index % (3 + 1)] = gridRaw[index] == '1';
            }
            for (var index = 0; index < expectedChangesRaw.Split().Length; index++)
            {
                grid[index / (3 + 1), index % (3 + 1)] = expectedChangesRaw[index] == '1';
            }

            
        }
        
    }
    
}