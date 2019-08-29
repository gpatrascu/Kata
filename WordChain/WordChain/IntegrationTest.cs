using System;
using System.IO;
using System.Net;
using Xunit;

namespace WordChain
{
    public class IntegrationTest
    {
        [Fact(Skip = "integration")]
        //[Fact]
        public void Example_cat_dog()
        {
            var readAllLines = File.ReadAllLines("./words.txt");

            var wordChain = new WordChainGenerator(readAllLines).GetWordChain("cat", "dog");

            foreach (var word in wordChain)
            {
                Console.WriteLine(word + ",");
            }
        } 
    }
}