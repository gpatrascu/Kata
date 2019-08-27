using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace WordChain
{
    public class WordChainGenerator
    {
        private readonly IList<string> dictionary;

        public List<List<string>> chains;
        public WordChainGenerator(IList<string> dictionary)
        {
            this.dictionary = dictionary;
        }

        public IEnumerable<string> GetWordChain(string inputWord, string finalWord)
        {
             chains = new List<List<string>>
            {
                new List<string> { inputWord }
            };

            for (var i = 0 ; i < chains.Count; i ++)
            {
                var nextWords = GetNextInChain(chains[i]);
                foreach (var word in nextWords)
                {
                        var list = new List<string>();
                        list.AddRange(chains[i]);
                        list.Add(word);

                        if (word == finalWord)
                        {
                            return list;
                        }

                        chains.Add(list);
                }
            }
            
            return chains.First();
        }

        private IEnumerable<string> GetNextInChain(List<string> words)
        {
            return dictionary.Where(s => HammingDistaceIsOne(words.Last(), s) && !words.Contains(s));
        }

        private bool WordsHaveSameLength(string last, string s)
        {
            return last.Length == s.Length;
        }

        private bool HammingDistaceIsOne(string string1, string string2)
        {
            return this.WordsHaveSameLength(string1, string2) && string1.Where((characterInString, characterIndex) 
                    => characterInString != string2[characterIndex]).Count() == 1;
        }
    }
}