namespace mergeSortedArrays;

public class UnitTest1
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 0, 0, 0 }, 3, new[] { 2, 5, 6 }, 3, new[] { 1, 2, 2, 3, 5, 6 })]
    [InlineData(new[] { 1 }, 1, new int[0], 0, new[] { 1 })]
    [InlineData(new[] { 0 }, 0, new[] { 1 }, 1, new[] { 1 })]
    [InlineData(new[] { 2, 0 }, 1, new[] { 1 }, 1, new[] { 1, 2 })]
    [InlineData(new[] { -1, 0, 0, 3, 3, 3, 0, 0, 0 }, 6, new[] { 1, 2, 2 }, 3, new[] { -1, 0, 0, 1, 2, 2, 3, 3, 3 })]
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
        int[] result = new int[m + n];
        int nums1Index = 0;
        int nums2Index = 0;

        for (var i = 0; i < m + n; i++)
        {
            if (nums1Index == m)
            {
                result[i] = nums2[nums2Index];
                nums2Index++;
                continue;
            }

            if (nums2Index == n)
            {
                result[i] = nums1[nums1Index];
                nums1Index++;
                continue;
            }

            if (nums1[nums1Index] < nums2[nums2Index])
            {
                result[i] = nums1[nums1Index];
                nums1Index++;
                continue;
            }

            result[i] = nums2[nums2Index];
            nums2Index++;
        }

        for (int i = 0; i < m + n; i++)
        {
            nums1[i] = result[i];
        }
    }
}