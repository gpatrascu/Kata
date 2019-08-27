namespace WordChain
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class WordChainGenerator
    {
        const string alphabet = "abcdefghijklmnopqrstuvwxyz";

        private const string fileName = "..\\..\\..\\words.txt";

        private string firstWord;

        private string lastWord;

        public WordChainGenerator(string firstWord, string lastWord)
        {
            this.firstWord = firstWord;
            this.lastWord = lastWord;
        }

        public string GetWordChain()
        {
            var words = this.GetDictionary();
            var wordLength = this.firstWord.Length;
            var validWords = words.Where(x => x.Length == wordLength);

            var result = new List<string> { this.firstWord };

            for (int i = 0; i < this.firstWord.Length; i++)
            {
                var temp = this.firstWord;

                // iterate through alphabet letters
                foreach (char c in alphabet)
                {
                    // update word first letter with alphabet letter/s
                    temp = this.ReplaceChar(c, i);
                    if (validWords.Contains(temp))
                    {
                        result.Add(temp);
                        if (temp == this.lastWord)
                        {
                            return this.ProcessResult(result);
                        }
                    }

                    // check if the word exist in dictionary
                    // and check if it is similar with last Word
                    // if exists in dictionary but not last word, switch to the next letter in the first word
                    // check if it exist in the dictionary and if it's the last word
                    // if not, recursive to the first letter
                    // and so on..
                }
            }

            return this.ProcessResult(result);
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

        private string ReplaceChar(char c, int index)
        {
            StringBuilder strBuilder = new StringBuilder(this.firstWord);
            strBuilder[index] = c;
            return strBuilder.ToString();
        }
    }
}