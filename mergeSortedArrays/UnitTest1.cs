namespace mergeSortedArrays;

public class UnitTest1
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 0, 0, 0 }, 3, new[] { 2, 5, 6 }, 3, new[] { 1, 2, 2, 3, 5, 6 })]
    [InlineData(new[] { 1 }, 1, new int[0], 0, new[] { 1 })]
    [InlineData(new[] { 0 }, 0, new[] { 1 }, 1, new[] { 1 })]
    [InlineData(new[] { 2,0 }, 1, new[] { 1 }, 1, new[] { 1, 2 })]
    [InlineData(new[] { -1,0,0,3,3,3,0,0,0 }, 6, new[] { 1,2,2 }, 3, new[] { -1,0,0,1,2,2,3,3,3 })]
    public void Test(int[] nums1, int m, int[] nums2, int n, int[] expectedArray)
    {
        Solution.Merge(nums1, m, nums2, n);
        Assert.Equal(expectedArray, nums1);
    }
}

public class Solution
{
    public static void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        var nums2Index = 0;
        var nums1Index = 0;

        if (n == 0)
        {
            return;
        }

        while (nums1Index < m + n && nums2Index < n)
        {
            if (nums2[nums2Index] < nums1[nums1Index])
            {
                for (var copyIndex = m + n - 1; copyIndex > nums1Index; copyIndex--)
                {
                    nums1[copyIndex] = nums1[copyIndex - 1];
                }

                nums1[nums1Index] = nums2[nums2Index];
                nums2Index++;
            }

            nums1Index++;
        }

        while (nums1Index < m + n && nums2Index < n)
        {
            nums1[nums1Index] = nums2[nums2Index];
            nums1Index++;
            nums2Index++;
        }

        while (nums2Index < n)
        {
            nums1[m + nums2Index] = nums2[nums2Index];
            nums2Index++;
        }
    }
}