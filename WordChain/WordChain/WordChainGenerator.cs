using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

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
            var visited = new Dictionary<string, string> {{inputWord, inputWord}};
            return new List<string> {inputWord}.Union(GetNextWords(inputWord, finalWord,
                visited, new Dictionary<string, string>()));
        }

        private IEnumerable<string> GetNextWords(string inputWord, string finalWord, Dictionary<string, string> visited,
            Dictionary<string, string> lowerLevel)
        {
            var oneLetterAway = dictionary.Where(s => s.Length == inputWord.Length)
                .Where(s => HammingDistaceIsOne(inputWord, s)
                            && !visited.ContainsKey(s)
                            && !lowerLevel.ContainsKey(s)
                ).ToList();

            Print(oneLetterAway, inputWord);

            if (oneLetterAway.Contains(finalWord)) return new[] {finalWord};

            if (!oneLetterAway.Any()) return oneLetterAway;

            var lists = oneLetterAway
                .Select(word => BuildWordsList(finalWord, visited, word, oneLetterAway, lowerLevel))
                .ToList();
            ;

            var firstOrDefault = lists.Where(_ => _.Contains(finalWord))
                .OrderBy(_ => _.Count).FirstOrDefault();

            return firstOrDefault ?? new List<string>();
        }

        private List<string> BuildWordsList(string finalWord, Dictionary<string, string> visited, string word,
            List<string> oneLetterAway, Dictionary<string, string> lowerLevel)
        {
            var previous = oneLetterAway.ToList();
            previous.Remove(word);
            var list = new Dictionary<string, string>(lowerLevel);
            foreach (var w in previous) list.Add(w, w);

            return new List<string> {word}.Union(GetNextWords(word, finalWord, Visited(visited, word), list)).ToList();
        }

        private static Dictionary<string, string> Visited(Dictionary<string, string> visited, string word)
        {
            return new Dictionary<string, string>(visited) {{word, word}};
        }

        private void Print(IList<string> nextInChain, string inputWord)
        {
            Console.WriteLine();
            Console.WriteLine("word = " + inputWord);
            foreach (var w in nextInChain)
            {
                Console.Write(w);
                Console.Write(" ");
            }
        }

        private bool HammingDistaceIsOne(string string1, string string2)
        {
            return string1.Where((characterInString, characterIndex)
                       => characterInString != string2[characterIndex]).Count() == 1;
        }
    }
}