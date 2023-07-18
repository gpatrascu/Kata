using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;
using Xunit.Abstractions;


public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test2()
    {
        Tree tree = new Tree()
        {
            x = 8, 
            l = new Tree()
            {
                x = 2,
                l = new Tree()
                {
                    x = 8
                },
                r = new Tree()
                {
                    x = 7
                }
            },
            r = new Tree()
            {
                x = 6
            }
        };

        var solution = new Solution().solution(tree);
        
        Assert.Equal(2, solution);
    }
    
    [Fact]
    public void Test1()
    {
        Tree tree = new Tree()
        {
            x = 5,
            l = new Tree()
            {
                x = 3,
                l = new Tree()
                {
                    x = 20
                },
                r = new Tree()
                {
                    x = 21
                }
            },
            r = new Tree()
            {
                x = 10, 
                l = new Tree()
                {
                    x = 1
                }
            }
        };

        var solution = new Solution().solution(tree);
        
        Assert.Equal(4, solution);
    }
}


class Tree {
    public int x;
    public Tree l;
    public Tree r;
};