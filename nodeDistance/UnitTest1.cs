using System.Diagnostics;
using System.Text;
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
    [InlineData(new[] { 3, 5, 1, 6, 2, 0, 8, int.MinValue, int.MinValue, 7, 4 })]
    [InlineData(new[] { 1,2,3,4,5,6,7 })]
    [InlineData(new[] { 0, 1, 2, int.MinValue, 3, int.MinValue, 5, 4 })]
    [InlineData(
        new[]
        {
            0, 2, 1, 3, 6, 5, 9, 4, 22, 13, 28, 7, 11, int.MinValue, int.MinValue, 8, 21, int.MinValue, int.MinValue,
            int.MinValue, 14, 32, int.MinValue, int.MinValue, 33, 16, 19, 30, 10, int.MinValue, int.MinValue, 17, 18,
            42, 41, 40, int.MinValue, int.MinValue, 39, 23, int.MinValue, 34, int.MinValue, 12, int.MinValue, 29, 31,
            25, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue,
            int.MinValue, 48, int.MinValue, 36, int.MinValue, int.MinValue, 15, 20, int.MinValue, int.MinValue,
            int.MinValue, int.MinValue, 47, int.MinValue, int.MinValue, int.MinValue, 37, int.MinValue, 26, 24, 43,
            int.MinValue, int.MinValue, int.MinValue, int.MinValue, 38, int.MinValue, 45, int.MinValue, 27,
            int.MinValue, int.MinValue, 46, int.MinValue, int.MinValue, int.MinValue, 35, 49, int.MinValue,
            int.MinValue, 44
        })]
    public void PrintBFS(int[] inputArrayParams)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var inputArray = inputArrayParams.Select(i => i == int.MinValue ? (int?)null : i).ToArray();

        var root = TreeNode.Deserialize(inputArray);

        var printLevelOrder = new Solution().PrintLevelOrder(root);

        _testOutputHelper.WriteLine(printLevelOrder);

        stopwatch.Stop();
        _testOutputHelper.WriteLine("time spend = " + stopwatch.Elapsed.TotalSeconds);
    }
    

    [Theory]
    [InlineData(new[] { 3, 5, 1, 6, 2, 0, 8, int.MinValue, int.MinValue, 7, 4 }, 5, 2, new[] { 7, 4, 1 })]
    [InlineData(new[] { 3, 5, 1, 6, 2, 0, 8, int.MinValue, int.MinValue, 7, 4 }, 2, 1, new[] { 7, 4, 5 })]
    [InlineData(new[] { 3, 5, 1, 6, 2, 0, 8, int.MinValue, int.MinValue, 7, 4 }, 2, 2, new[] { 6, 3 })]
    [InlineData(new[] { 0, 1, 2, int.MinValue, 3, int.MinValue, 5, 4 }, 3, 3, new[] { 2 })]
    [InlineData(
        new[]
        {
            0, 2, 1, 3, 6, 5, 9, 4, 22, 13, 28, 7, 11, int.MinValue, int.MinValue, 8, 21, int.MinValue, int.MinValue,
            int.MinValue, 14, 32, int.MinValue, int.MinValue, 33, 16, 19, 30, 10, int.MinValue, int.MinValue, 17, 18,
            42, 41, 40, int.MinValue, int.MinValue, 39, 23, int.MinValue, 34, int.MinValue, 12, int.MinValue, 29, 31,
            25, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue,
            int.MinValue, 48, int.MinValue, 36, int.MinValue, int.MinValue, 15, 20, int.MinValue, int.MinValue,
            int.MinValue, int.MinValue, 47, int.MinValue, int.MinValue, int.MinValue, 37, int.MinValue, 26, 24, 43,
            int.MinValue, int.MinValue, int.MinValue, int.MinValue, 38, int.MinValue, 45, int.MinValue, 27,
            int.MinValue, int.MinValue, 46, int.MinValue, int.MinValue, int.MinValue, 35, 49, int.MinValue,
            int.MinValue, 44
        }, 11, 20, new int[] { })]
    public void TestSolution1(int[] inputArrayParams, int targetValue, int k, int[] expectedResult)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var inputArray = inputArrayParams.Select(i => i == int.MinValue ? (int?)null : i).ToArray();

        var root = TreeNode.Deserialize(inputArray);
        var target = root.FindNode(targetValue);

        var distanceK = new Solution().DistanceK(root, target, k);

        foreach (var el in distanceK)
        {
            _testOutputHelper.WriteLine(el.ToString());
        }

        Assert.Equal(expectedResult, distanceK);

        stopwatch.Stop();
        _testOutputHelper.WriteLine("time spend = " + stopwatch.Elapsed.TotalSeconds);
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

    public static TreeNode? Deserialize(IList<int?>? vals)
    {
        if (vals == null) return null;

        //Split string 
        if (vals.Count == 0) return null;

        Queue<TreeNode?> q = new Queue<TreeNode?>();

        //First value is root node
        var root = new TreeNode(vals[0].Value);
        q.Enqueue(root);

        int i = 1;

        //Run a loop and traverse the array
        while (i < vals.Count)
        {
            var p = q.Dequeue();

            if (vals[i] == null)
            {
                p.left = null;
            }
            else
            {
                p.left = new TreeNode(vals[i].Value);
                q.Enqueue(p.left);
            }
            
            i++;

            if (i < vals.Count)
            {
                if (vals[i] == null)
                {
                    p.right = null;
                }
                else
                {
                    p.right = new TreeNode(vals[i].Value);
                    q.Enqueue(p.right);
                }

                i++;
            }
        }


        return root;
    }

    
    
    
    public override string ToString()
    {
        var trim = $"{val}-{left}-{right}".Trim('-');

        return trim.Contains("-") ? $"({trim})" : trim;
    }

    public TreeNode? FindNode(int target)
    {
        if (val == target)
        {
            return this;
        }

        if (left != null)
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
    public string PrintLevelOrder(TreeNode root)
    {
        StringBuilder sb = new StringBuilder();
        Queue<TreeNode> queue = new Queue<TreeNode>();

        sb.AppendLine("enqueue " + root.val); 
        queue.Enqueue(root);
        while (queue.Count != 0) {
 
            var tempNode = queue.Dequeue();
            sb.AppendLine("dequeue and use " + tempNode.val);
            //sb.Append(tempNode.val + " ");
 
            // Enqueue left child
            if (tempNode.left != null) {
                sb.AppendLine("enqueue left node " + tempNode.left.val);
                queue.Enqueue(tempNode.left);
            }
 
            // Enqueue right child
            if (tempNode.right != null) {
                sb.AppendLine("enqueue right node " + tempNode.right.val);
                queue.Enqueue(tempNode.right);
            }
        }

        return sb.ToString();
    }
    
    private static TreeNode? FindParentOf(TreeNode? target, Dictionary<TreeNode, TreeNode?> parentDictionary)
    {
        return target != null && !parentDictionary.ContainsKey(target) ? null : parentDictionary[target];
    }

    public IList<int> DistanceK(TreeNode? root, TreeNode? target, int k)
    {
        var parentDictionary = new Dictionary<TreeNode, TreeNode?>();
        MapParents(root, parentDictionary);
        var distanceKWithPath = DistanceKWithPath(root, target, k, new HashSet<int>(), parentDictionary);
        return distanceKWithPath.ToList();
    }

    private void MapParents(TreeNode? root, Dictionary<TreeNode, TreeNode?> parentDictionary)
    {
        if (root == null)
        {
            return;
        }

        if (root.left != null)
        {
            parentDictionary[root.left] = root;
        }

        if (root.right != null)
        {
            parentDictionary[root.right] = root;
        }

        MapParents(root.left, parentDictionary);
        MapParents(root.right, parentDictionary);
    }

    private HashSet<int> DistanceKWithPath(TreeNode? root, TreeNode? target, int k, HashSet<int> pathWalked,
        Dictionary<TreeNode, TreeNode?> parentDictionary)
    {
        if (target == null)
        {
            return new HashSet<int>();
        }

        if (k == 0)
        {
            return new HashSet<int> { target.val };
        }

        pathWalked.Add(target.val);

        var result = new HashSet<int>();
        if (target.left != null && !pathWalked.Contains(target.left.val))
        {
            result.UnionWith(DistanceKWithPath(root, target.left, k - 1, pathWalked, parentDictionary));
        }

        if (target.right != null && !pathWalked.Contains(target.right.val))
        {
            result.UnionWith(DistanceKWithPath(root, target.right, k - 1, pathWalked, parentDictionary));
        }

        var parent = FindParentOf(target, parentDictionary);
        if (parent != null && !pathWalked.Contains(parent.val))
        {
            result.UnionWith(DistanceKWithPath(root, parent, k - 1, pathWalked, parentDictionary));
        }

        return result;
    }
}