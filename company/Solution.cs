using System.Linq;

class Solution {
    public int solution(int[] A) {
        // Implement your solution here
        
        int index = A.Length-1;
        int numberOfRelocations = 0;
        var list = A.ToList();

        while (index > 0)
        {
            list.RemoveAt(index);
            while (list.Sum() < 0)
            {
                list.Remove(list.Min());
                numberOfRelocations++;
                index--;
            }
            index--;
        }

        return numberOfRelocations;
    }
}