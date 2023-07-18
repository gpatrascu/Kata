using System;
using System.Collections.Generic;

class Solution {
    public int solution(Tree T) {
        IList<Tree> visibleNodes = new List<Tree>();
        SearchVisible(T, T.x, visibleNodes);
        
        return visibleNodes.Count;
    }

    private void SearchVisible(Tree tree, int maxValue, IList<Tree> visibleNodes)
    {
        if (tree.x >= maxValue)
        {
            visibleNodes.Add(tree);
        }

        if (tree.l != null)
        {
            SearchVisible(tree.l, Math.Max(maxValue, tree.x), visibleNodes);
        }
        
        if (tree.r != null)
        {
            SearchVisible(tree.r, Math.Max(maxValue, tree.x), visibleNodes);
        }
    }
}