using Xunit.Abstractions;

namespace nodeDistance;

public class TreeNode2
{
    public int val;
    public TreeNode2 left;
    public TreeNode2 right;

    public TreeNode2(int x)
    {
        val = x;
    }
}

public class BinaryTree
{
    private readonly ITestOutputHelper _testOutputHelper;
    public TreeNode2 root;

    // Encodes a tree to a single string.
    public static string Serialize(TreeNode2 root)
    {
        if (root == null)
        {
            return null;
        }

        Stack<TreeNode2> s = new Stack<TreeNode2>();
        s.Push(root);

        List<string> l = new List<string>();
        while (s.Count > 0)
        {
            TreeNode2 t = s.Pop();

            // If current node is NULL, store marker
            if (t == null)
            {
                l.Add("#");
            }
            else
            {
                // Else, store current node
                // and recur for its children
                l.Add(t.val.ToString());
                s.Push(t.right);
                s.Push(t.left);
            }
        }

        return string.Join(",", l);
    }

    static int t;

    public BinaryTree(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    // Decodes your encoded data to tree.
    public static TreeNode2 Deserialize(string data)
    {
        if (data == null)
            return null;
        t = 0;
        string[] arr = data.Split(',');
        return Helper(arr);
    }

    public static TreeNode2 Helper(string[] arr)
    {
        if (arr[t].Equals("#"))
            return null;

        // Create node with this item
        // and recur for children
        TreeNode2 root = new TreeNode2(int.Parse(arr[t]));
        t++;
        root.left = Helper(arr);
        t++;
        root.right = Helper(arr);
        return root;
    }

    // A simple inorder traversal used
    // for testing the constructed tree
    static void Inorder(TreeNode2 root)
    {
        if (root != null)
        {
            Inorder(root.left);
            Console.Write(root.val + " ");
            Inorder(root.right);
        }
    }

    // Driver code
    [Fact]
    public void Test()
    {
        _testOutputHelper.WriteLine("tes");
        // Construct a tree shown in the above figure
        BinaryTree tree = new BinaryTree(_testOutputHelper);
        tree.root = new TreeNode2(20);
        tree.root.left = new TreeNode2(8);
        tree.root.right = new TreeNode2(22);
        tree.root.left.left = new TreeNode2(4);
        tree.root.left.right = new TreeNode2(12);
        tree.root.left.right.left = new TreeNode2(10);
        tree.root.left.right.right = new TreeNode2(14);

        string serialized = Serialize(tree.root);
        _testOutputHelper.WriteLine("Serialized view of the tree:");
        _testOutputHelper.WriteLine(serialized);
        _testOutputHelper.WriteLine("");

        // Deserialize the// stored tree into root1
        TreeNode2 t = Deserialize(serialized);

        _testOutputHelper.WriteLine(
            "Inorder Traversal of the tree constructed"
            + " from serialized String:");
        Inorder(t);
    }
}