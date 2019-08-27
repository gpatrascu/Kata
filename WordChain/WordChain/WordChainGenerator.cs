using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WordChain
{
    public class WordChainGenerator
    {
        const string alphabet = "abcdefghijklmnopqrstuvwxyz";

        private const string fileName = "..\\..\\..\\words.txt";

        private string firstWord;

        private string lastWord;

        private List<string> validWords;

        public WordChainGenerator(string firstWord, string lastWord)
        {
            this.firstWord = firstWord;
            this.lastWord = lastWord;
            var words = GetDictionary();
            validWords = words.Where(x => x.Length == firstWord.Length).ToList();
        }

        public string GetWordChain()
        {
            var wordCChain = new List<string> {firstWord};

            if (CheckIfWordsDifferByOneLetter(firstWord, lastWord))
            {
                wordCChain.Add(lastWord);
                return ProcessResult(wordCChain);
            }

            wordCChain =  GetResult(firstWord, GetWordsThatDifferByOneLetterFrom(firstWord, validWords), wordCChain);

            return ProcessResult(wordCChain);
        }

        private List<string> GetResult(string currentWord, List<string> similarWords, List<string> wordChain)
        {
            foreach (var similarWord in similarWords)
            {
                if (similarWord == lastWord)
                {
                    wordChain.Add(similarWord);
                    return wordChain;
                }

                if (GetWordsThatDifferByOneLetterFrom(similarWord).Count == 0)
                {
                    var smallChain = wordChain.Remove(similarWord);
                    //return;
                }
                
                wordChain.Add(similarWord);
                GetResult(similarWord, GetWordsThatDifferByOneLetterFrom(similarWord), wordChain);
            }
        }

        private List<string> GetWordsThatDifferByOneLetterFrom(string word)
        {
            return this.validWords.Where(validWord => CheckIfWordsDifferByOneLetter(word, validWord)).ToList();
        }
        
        private bool CheckIfWordsDifferByOneLetter(string firstWord, string lastWord)
        {
            return HemmingDistance(firstWord, lastWord) == 1;
        }
        
        private int HemmingDistance(string firstWord, string lastWord)
        {
            return firstWord.Where((t, i) => t != lastWord[i]).Count();
        }

        private List<string> GetDictionary()
        {
            string text = File.ReadAllText(fileName);
            return text.Split(Environment.NewLine).ToList();
        }

        private string ProcessResult(List<string> result)
        {
            return string.Join(" ", result.ToArray());
        }

        private string ReplaceLetterAtPosition(char c, int index, string firstWord)
        {
            StringBuilder strBuilder = new StringBuilder(this.firstWord);
            strBuilder[index] = c;
            return strBuilder.ToString();
        }
    }
}