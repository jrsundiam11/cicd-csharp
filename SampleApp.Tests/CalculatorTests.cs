using SampleApp;
using Xunit;

namespace SampleApp.Tests;

public class CalculatorTests
{
    private readonly Calculator _calculator = new();

    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(-2, 3, 1)]
    [InlineData(0, 0, 0)]
    public void Add_ReturnsCorrectSum(int a, int b, int expected)
    {
        var result = _calculator.Add(a, b);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(0, 5, -5)]
    public void Subtract_ReturnsCorrectDifference(int a, int b, int expected)
    {
        var result = _calculator.Subtract(a, b);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(4, 5, 20)]
    [InlineData(-3, 3, -9)]
    public void Multiply_ReturnsCorrectProduct(int a, int b, int expected)
    {
        var result = _calculator.Multiply(a, b);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Divide_ReturnsCorrectQuotient()
    {
        var result = _calculator.Divide(10, 2);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => _calculator.Divide(10, 0));
    }

    [Theory]
    [InlineData(2, true)]
    [InlineData(17, true)]
    [InlineData(4, false)]
    [InlineData(1, false)]
    [InlineData(-5, false)]
    public void IsPrime_IdentifiesPrimesCorrectly(int number, bool expected)
    {
        var result = _calculator.IsPrime(number);
        Assert.Equal(expected, result);
    }

    // ------------------------------------------------------------------
    // DEMO ONLY: uncomment this test to see the pipeline fail and block
    // the pull request from being merged (see README.md, "Demo a failure").
    // ------------------------------------------------------------------
    [Fact]
    public void Add_IntentionallyBroken_ToDemonstrateFailingPipeline()
    {
        var result = _calculator.Add(2, 2);
        Assert.Equal(5, result); // wrong on purpose 
    }
}
