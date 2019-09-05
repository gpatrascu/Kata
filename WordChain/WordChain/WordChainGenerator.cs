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
            var reducedDictionary = GetReducedDictionary(inputWord);
            var chains = new List<List<string>> {new List<string> {inputWord}};

            while (chains.Any() && !chains.Last().Contains(finalWord))
                chains = GoToNextTreeLevel(chains, reducedDictionary, finalWord);

            return chains.Any() ? chains.Last() : new List<string>();
        }

        private List<string> GetReducedDictionary(string inputWord)
        {
            return dictionary.Where(s => s.Length == inputWord.Length).ToList();
        }

        private List<List<string>> GoToNextTreeLevel(List<List<string>> chains, IList<string> reducedDictionary,
            string finalWord)
        {
            var newChains = new List<List<string>>();

            foreach (var wordChain in chains)
            {
                var nextWords =
                    reducedDictionary
                        .Where(s => HammingDistaceIsOne(wordChain.Last(), s) && !wordChain.Contains(s)).ToList();

                if (nextWords.Contains(finalWord)) return new List<List<string>> {NewChain(wordChain, finalWord)};

                newChains.AddRange(nextWords.Select(s => NewChain(wordChain, s)));
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
            var differentCharacters = 0;

            for (var i = 0; i < string1.Length; i++)
            {
                if (string1[i] != string2[i]) differentCharacters++;

                if (differentCharacters > 1) return false;
            }

            return differentCharacters == 1;
        }
    }
}