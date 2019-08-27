using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            var visited = new List<string> {};

            return new List<string> {inputWord}.Union(GetNextInChain(inputWord, finalWord, visited));
        }

        private IEnumerable<string> GetNextInChain(string inputWord, string finalWord, List<string> visited)
        {
            visited.Add(inputWord);
            IList<string> nextInChain = dictionary.Where(s => s.Length == inputWord.Length)
                .Where(s => HammingDistaceIsOne(inputWord, s)
                            && !visited.Contains(s)).ToList();

            if (nextInChain.Contains(finalWord)) return new[] {finalWord};

            if (!nextInChain.Any()) return nextInChain;

            var lists = nextInChain
                .Select(word => new List<string> {word}.Union(GetNextInChain(word, finalWord, visited)).ToList())
                .ToList();

            var @where = lists.Where(enumerable => enumerable.Contains(finalWord));
            
            var firstOrDefault = @where
                .OrderBy(enumerable => enumerable.Count).FirstOrDefault();

            return firstOrDefault?? new List<string>();
        }

        private bool HammingDistaceIsOne(string string1, string string2)
        {
            return string1.Where((characterInString, characterIndex)
                       => characterInString != string2[characterIndex]).Count() == 1;
        }
    }
}