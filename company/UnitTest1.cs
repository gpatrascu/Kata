using Xunit;


public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var input = new int[] { 10, -10, -1, -1, 10 };

        var solution = new Solution().solution(input);
        Assert.Equal(1, solution);
    }
    
    [Fact]
    public void Test2()
    {
        var input = new int[] { -1, -1,-1, 1,1,1,1 };

        var solution = new Solution().solution(input);
        Assert.Equal(3, solution);
    }
    
    [Fact]
    public void Test3()
    {
        var input = new int[] { 5, -2,-3, 1 };

        var solution = new Solution().solution(input);
        Assert.Equal(0, solution);
    }
    
}