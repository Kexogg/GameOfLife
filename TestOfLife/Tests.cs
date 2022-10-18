using System;
using NUnit.Framework;

namespace TestOfLife;
using GameOfLifeEngine;
[TestFixture]
public class Tests
{
    private static (int width, int height) GetDimensionsFromRawData(string data, char separator)
    {
        var width = !data.Contains(separator) ? data.Length : data.IndexOf(separator);
        var height = data.Split(separator, StringSplitOptions.RemoveEmptyEntries).Length;
        return (width, height);
    }
    
    [TestCase(false, false)]
    [TestCase(true, true)]
    public void TestForAliveCells(bool cellState, bool expected)
    {
        var field = new GameOfLifeField(2, 2);
        field.SetCell(1, 1, cellState);
        Assert.AreEqual(expected, field.HasAliveCells());
    }

    [TestCase("00;01",';')]
    [TestCase("00,00",',')]
    [TestCase("010,010",',')]
    public void TestToStringConversion(string fieldRaw, char separator)
    {
        var (width, height) = GetDimensionsFromRawData(fieldRaw, separator);
        var field = new GameOfLifeField(width, height);
        field.FromString(fieldRaw, separator);
        Assert.IsTrue(field.ToString(separator) == fieldRaw);
    }
    
    [TestCase("01;01", ';')]
    [TestCase("0101", ';')]
    [TestCase("01;01;01", ';')]
    [TestCase("0;0", ';')]
    [TestCase("0,0", ',')]
    public void TestFromStringConversion(string data, char separator)
    {
        var (width, height) = GetDimensionsFromRawData(data, separator);
        var field = new GameOfLifeField(width, height);
        field.FromString(data, separator);
        Assert.AreEqual(data, field.ToString(separator));
    }
    
    [TestCase(0, -1, false)]
    [TestCase(-1, -1, true)]
    public void TestForOverflowCoordinate(int coordinateX, int coordinateY, bool expected)
    {
        var field = new GameOfLifeField(2, 2);
        field.SetCell(1, 1, true);
        Assert.AreEqual(expected, field.GetCell(coordinateX, coordinateY));
    }

    [TestCase("00;00", new[] { 1, 1 }, "00;01")]
    [TestCase("00;01", new[] { 1, 1 }, "00;00")]
    public void TestForIterationProcessing(string fieldRaw, int[] changes, string fieldExpectedRaw)
    {
        var field = new GameOfLifeField(2, 2);
        field.FromString(fieldRaw);
        field.ProcessIteration(new[] { changes });
        Assert.AreEqual(fieldExpectedRaw, field.ToString());
    }
    //TODO: more tests
    [TestCase("0000;0110;0100;0000", "0000;0110;0110;0000", ';')]
    public void TestForChanges(string fieldRaw, string expectedChangesRaw, char separator)
    {
        var (width, height) = GetDimensionsFromRawData(fieldRaw, separator);
        var field = new GameOfLifeField(width, height);
        field.FromString(fieldRaw);
        field.ProcessIteration(field.CalculateChanges());
        Assert.AreEqual(expectedChangesRaw, field.ToString());
    }
}