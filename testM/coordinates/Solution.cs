using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    public int solution(int A, int B, int C, int D)
    {
        return Permute(new[] { A, B, C, D }).Select(Distance).Max();
    }

    private static IList<IList<int>> Permute(int[] points)
    {
        return DoPermute(points, 0, points.Length - 1, new List<IList<int>>());
    }

    private static IList<IList<int>> DoPermute(int[] coordinates, int start, int end, IList<IList<int>> coordinatesPermutations)
    {
        if (start == end)
        {
            coordinatesPermutations.Add(new List<int>(coordinates));
            return coordinatesPermutations;
        }

        for (var i = start; i <= end; i++)
        {
            Swap(coordinates, start, i);
            DoPermute(coordinates, start + 1, end, coordinatesPermutations);
            Swap(coordinates, start, i);
        }

        return coordinatesPermutations;
    }

    private static void Swap(IList<int> nums, int first, int second)
    {
        var temp = nums[first];
        nums[first] = nums[second];
        nums[second] = temp;
    }


    public int Distance(IList<int> array)
    {
        return (int)Vector2.DistanceSquared(
            new Vector2
            {
                X = array[0],
                Y = array[1]
            },
            new Vector2
            {
                X = array[2],
                Y = array[3]
            });
    }
}