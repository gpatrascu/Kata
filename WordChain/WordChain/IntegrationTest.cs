using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Xunit;

namespace WordChain
{
    public class IntegrationTest
    {
        //[Fact(Skip = "integration")]
        [Fact]
        public void Example_cat_dog()
        {
            TestFor("cat", "dog");
            TestFor("dog", "cat");
        }
        
        [Fact]
        public void Example_lead_gold()
        {
            TestFor("lead", "gold");
            TestFor("gold", "lead");
        }
        
        [Fact]
        public void Example_ruby_code()
        {
            TestFor("ruby", "code");
            TestFor("code", "ruby");
        }

        private static void TestFor(string inputWord, string finalWord)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var readAllLines = File.ReadAllLines("./words.txt");

            var wordChain = new WordChainGenerator(readAllLines).GetWordChain(inputWord, finalWord);

            foreach (var word in wordChain)
            {
                Console.WriteLine(word + ",");
            }

            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}