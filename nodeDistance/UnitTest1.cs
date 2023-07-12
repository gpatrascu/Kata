using Xunit.Abstractions;

namespace nodeDistance;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData(new [] { 3, 5, 1, 6, 2, 0, 8, int.MinValue, int.MinValue, 7, 4 }, new[] { 5, 6, 2, int.MinValue, int.MinValue, 7, 4 },new[] { 1, 0, 8 } )]
    [InlineData(new [] { 0,1,2,int.MinValue,3,int.MinValue,5,4 }, new[] { 1, int.MinValue, 3, 4 },new[] { 2, int.MinValue, 5 } )]
    public void TestSplitArrays(int[] arrayInput, int[] expectedLeftArray, int[] expectedRightArray)
    {
        var array = ToNullableArray(arrayInput);

        (int?[] leftArray, int?[] rightArray) = TreeNode.SplitArrays(1, array);

        Assert.Equal(ToNullableArray(expectedLeftArray), leftArray);
        Assert.Equal(ToNullableArray(expectedRightArray), rightArray);
    }

    private static int?[] ToNullableArray(int[] arrayInput)
    {
        return arrayInput.Select(i => i == int.MinValue ? (int?)null : i).ToArray();
    }

    [Fact]
    public void TestBuildTree()
    {
        var array = new int?[] { 3, 5, 1, 6, 2, 0, 8, null, null, 7, 4 };
        var root = TreeNode.FromArray(array);
        _testOutputHelper.WriteLine(root.ToString());
    }
    
    [Fact]
    public void TestBuildTree2()
    {
        var array = new int?[] {  0,1,2,null,3,null,5,4 };
        var root = TreeNode.FromArray(array);
        _testOutputHelper.WriteLine(root.ToString());
    }
    
    
    [Theory]
    [InlineData(5,3)]
    [InlineData(2,5)]
    [InlineData(0,1)]
    public void FindParentTest(int targetValue, int parentValue)
    {
        var array = new int?[] { 3, 5, 1, 6, 2, 0, 8, null, null, 7, 4 };
        var root = TreeNode.FromArray(array);
        var target = root.FindNode(targetValue);

        var parent = Solution.FindParentOf(root, target);

        Assert.Equal(root.FindNode(parentValue), parent);
    }

    [Theory]
    [InlineData(new [] { 3, 5, 1, 6, 2, 0, 8, int.MinValue, int.MinValue, 7, 4 }, 5, 2, new [] { 7, 4, 1} )]
    [InlineData(new [] { 3, 5, 1, 6, 2, 0, 8, int.MinValue, int.MinValue, 7, 4 }, 2, 1, new [] { 7, 4, 5} )]
    [InlineData(new [] { 3, 5, 1, 6, 2, 0, 8, int.MinValue, int.MinValue, 7, 4 }, 2, 2, new [] { 6, 3} )]
    [InlineData(new [] { 0,1,2,int.MinValue,3,int.MinValue,5,4 }, 3, 3, new [] { 1,4,2} )]
    public void TestSolution1(int[] inputArrayParams, int targetValue, int k, int[] expectedResult)
    {
        int?[] inputArray = inputArrayParams.Select(i => i == int.MinValue ? (int?)null : i).ToArray();

        var root = TreeNode.Deserialize(inputArray);
        var target = root.FindNode(targetValue);

        _testOutputHelper.WriteLine(root.ToString());
        _testOutputHelper.WriteLine(target.ToString());

        var distanceK = new Solution().DistanceK(root, target, k);

        foreach (var el in distanceK)
        {
            _testOutputHelper.WriteLine(el.ToString());
        }
        Assert.Equal(expectedResult, distanceK);
    }
}

/*
 * Definition for a binary tree node.*/
public class TreeNode
{
    public int val;
    public TreeNode? left;
    public TreeNode? right;

    public TreeNode(int x)
    {
        val = x;
    }

    public static TreeNode Deserialize(IList<int?> vals) {
        
        if(vals == null) return null;
        
        //Split string 
        if(vals.Count == 0) return null;
        
        Queue<TreeNode> q = new Queue<TreeNode>();
        
        //First value is root node
        TreeNode root = new TreeNode(vals[0].Value);
        q.Enqueue(root);
        
        TreeNode p = null;
        int? val;
        
        int i = 1;
        
        //Run a loop and traverse the array
        while(i < vals.Count) {
            p = q.Dequeue();
            val = vals[i++];
            
            if(val == null) {
                p.left = null;
            } else {
                p.left = new TreeNode(val.Value);
                q.Enqueue(p.left);
            }
            
            if(i < vals.Count) {
                val = vals[i++];
                if(val == null) {
                    p.right = null;
                } else {
                    p.right = new TreeNode(val.Value);
                    q.Enqueue(p.right);
                }
            }
        }
        
        
        return root;
       
    }
    
    public static TreeNode? FromArray(int?[] array)
    {
        TreeNode? node = From(array, 0);
        if (node == null)
        {
            return node;
        }
        
        (int?[] left, int?[] right) = SplitArrays(1, array);
        node.left = FromArray(left);
        node.right = FromArray(right);
        
        return node;
    }

    public override string ToString()
    {
        var trim = $"{val}-{left}-{right}".Trim('-');

        return trim.Contains("-") ? $"({trim})" : trim;
    }

    public static (int?[], int?[]) SplitArrays(int index, int?[] array)
    {
        IList<int?> leftArray = new List<int?>();
        IList<int?> rightArray = new List<int?>();
        int level = 0;
        while (index < array.Length)
        {
            var numberOfItemsToAdd = (int)Math.Pow(2, level);
            
            // add to left array
            for (int i = 0; i < numberOfItemsToAdd; i++)
            {
                if (index < array.Length)
                {
                    leftArray.Add(array[index]);
                }
                index++;
            }

            // add to right array
            for (int i = 0; i < numberOfItemsToAdd; i++)
            {
                if (index < array.Length)
                {
                    rightArray.Add(array[index]);
                }
                index++;
            }

            level++;
        }

        return (leftArray.ToArray(), rightArray.ToArray());
    }

    private static TreeNode? From(int?[] array, int index)
    {
        if (index >= array.Length)
        {
            return null;
        }

        if (array[index] == null)
        {
            return null;
        }

        return new TreeNode(array[index].Value);
    }

    public TreeNode? FindNode(int target)
    {
        if (this.val == target)
        {
            return this;
        }
        
        if (this.left != null)
        {
            var leftSearch = left.FindNode(target);
            if (leftSearch != null)
            {
                return leftSearch;
            }
        }

        return right?.FindNode(target);
    }
}

public class Solution
{
    
    public static TreeNode? FindParentOf(TreeNode? root, TreeNode? target)
    {
        if (target == root)
        {
            return null;
        }

        if (root.left == target || root.right == target)
        {
            return root;
        }

        if (root.left != null)
        {
            var parent = FindParentOf(root.left , target);
            if (parent != null)
            {
                return parent;
            }
        }
        
        if (root.right != null)
        {
            var parent = FindParentOf(root.right, target);
            if (parent != null)
            {
                return parent;
            }
        }

        return null;
    }
    
    public IList<int> DistanceK(TreeNode root, TreeNode? target, int k)
    {
        if (target == null)
        {
            return new List<int>();
        }
        
        if (k == 0)
        {
            return new List<int> { target.val };
        }
        var parent = FindParentOf(root, target);

        var result = new List<int>();
        
        result.AddRange(DistanceK(root, target.left, k-1));
        result.AddRange(DistanceK(root, target.right, k-1));
        result.AddRange(DistanceK(root, parent, k-1));


        var hashSet = result.ToHashSet();
        hashSet.Remove(target.val);
        return hashSet.ToList();
    }
}