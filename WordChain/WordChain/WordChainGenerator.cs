using System.Collections.Generic;
using System.Linq;

namespace WordChain
{
    public class WordChainGenerator
    {
        private readonly IList<string> dictionary;

        public WordChainGenerator(IList<string> dictionary)
        {
            this.dictionary = dictionary;
        }

        public IEnumerable<string> GetWordChain(string inputWord, string finalWord)
        {
            var reducedDictionary = dictionary.Where(s => s.Length == inputWord.Length).ToList();
            var chains = new List<List<string>> {new List<string> {inputWord}};

            while (!chains.Last().Contains(finalWord))
            {
                chains = GoToNextTreeLevel(chains, reducedDictionary, finalWord);
            }

            return chains.Last();
        }

        private List<List<string>> GoToNextTreeLevel(List<List<string>> chains, List<string> reducedDictionary,
            string finalWord)
        {
            var newChains = new List<List<string>>();

            foreach (var wordChain in chains)
            {
                var nextWords =
                    reducedDictionary
                        .Where(s => HammingDistaceIsOne(wordChain.Last(), s)).ToList();

                if (nextWords.Contains(finalWord)) 
                {
                    wordChain.Add(finalWord); 
                    return new List<List<string>> {wordChain};
                }

                newChains.AddRange(nextWords.Select(s => NewChain(wordChain, s)).ToList());
            }
            
            return newChains;
        }

        private List<string> NewChain(List<string> chain, string word)
        {
            var list = chain.ToList();
            list.Add(word);
            return list;
        }

        private bool HammingDistaceIsOne(string string1, string string2)
        {
            if (string1.Length != string2.Length) return false;

            return string1
                       .Where((characterInString, characterIndex)
                           => characterInString != string2[characterIndex])
                       .Count() == 1;
        }
    }
}