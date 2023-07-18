using System.Collections.Generic;
using System.Numerics;
using Xunit;
using Xunit.Abstractions;


public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData(1, 1, 2, 3, 5)]
    [InlineData(2, 4, 2, 4, 8)]
    [InlineData(6, -23, 1, 4, 850)]
    [InlineData(-4000, 4000, 300, 100, 64040000)]
    [InlineData(-34, 45, 56, 100, 18077)]
    public void Test1(int a, int b, int c, int d, int expectedResult)
    {
        var sut = new Solution();
        _testOutputHelper.WriteLine("a");
        var solution = sut.solution(a, b, c, d);

        Assert.Equal(expectedResult, solution);
    }

    [Theory]
    [InlineData(1, 3, 1, 2, 1)]
    [InlineData(1, 2, 3, 1, 5)]
    [InlineData(2, 1, 1, 3, 5)]
    public void Squared(int q1, int p1, int q2, int p2, int expectedResult)
    {
        new Solution().Distance(new List<int>()
        {
            q1, p1, q2, p2
        });
    }
}