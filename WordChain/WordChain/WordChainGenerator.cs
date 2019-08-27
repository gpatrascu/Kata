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
            var chain = new List<string>();
            chain.Add(inputWord);

            chain.AddRange(GetNextInChain(inputWord, finalWord));

            if (!chain.Contains(finalWord))
            {
                chain.Add(finalWord);
            }

            return chain;
        }

        private IEnumerable<string> GetNextInChain(string inputWord, string finalWord)
        {
            return dictionary.Where(s => HammingDistaceIsOne(inputWord, s));
        }

        private bool HammingDistaceIsOne(string string1, string string2)
        {
            return string1.Where((characterInString, characterIndex) 
                    => characterInString != string2[characterIndex]).Count() == 1;
        }
    }
}