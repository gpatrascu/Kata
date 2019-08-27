using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace WordChain
{
    public class WordChainGeneratorTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public WordChainGeneratorTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldReturnTheDictionaryIfDictionaryOnlyHas2characters()
        {
            var generator = CreateSut();
            var result = generator.GetWordChain("a", "b");

            Assert.Equal(new List<string> {"a", "b"}, result);
        }

        private static WordChainGenerator CreateSut()
        {
            IList<string> dictionary = new List<string>
            {
                "a", "b"
            };

            var generator = new WordChainGenerator(dictionary);
            return generator;
        }

        [Fact]
        public void When_we_have_3_words_in_dictionary()
        {
            IList<string> dictionary = new List<string>
            {
                "aa",
                "ab",
                "bb"
            };

            var generator = new WordChainGenerator(dictionary);
            var result = generator.GetWordChain("aa", "bb");

            Assert.Equal(new List<string> {"aa", "ab", "bb"}, result);
        }

        [Fact]
        public void When_we_have_4_words_in_dictionary()
        {
            IList<string> dictionary = new List<string>
            {
                "aa",
                "ab",
                "ac",
                "bb"
            };

            var generator = new WordChainGenerator(dictionary);
            var result = generator.GetWordChain("aa", "bb");

            Assert.Equal(new List<string> {"aa", "ab", "bb"}, result);
        }

        [Fact]
        public void When_we_have_5_words_in_dictionary()
        {
            IList<string> dictionary = new List<string>
            {
                "aa",
                "ab",
                "ac",
                "bb"
            };

            var generator = new WordChainGenerator(dictionary);
            var result = generator.GetWordChain("aa", "bb");

            Assert.Equal(new List<string> { "aa", "ab", "bb" }, result);
        }

        [Fact]
        public void When_we_have_5_words_in_dictionary_and_different_Length()
        {
            IList<string> dictionary = new List<string>
            {
                "aa",
                "ab",
                "ac",
                "bb",
                "a"
            };

            var generator = new WordChainGenerator(dictionary);
            var result = generator.GetWordChain("aa", "bb");

            Assert.Equal(new List<string> { "aa", "ab", "bb" }, result);
        }

        [Fact]
        public void When_we_have_no_matching_words_in_dictionary()
        {
            IList<string> dictionary = new List<string>
            {
                "aa"
            };

            var generator = new WordChainGenerator(dictionary);
            var result = generator.GetWordChain("aa", "bb");

            Assert.Equal(new List<string> { "aa" }, result);
        }

        [Fact]
        public void Example_cat_dog()
        {
            var readAllLines = File.ReadAllLines("./words.txt");
            var generator = new WordChainGenerator(readAllLines);
            var wordChain = generator.GetWordChain("cat", "dog");

            foreach (var word in wordChain)
            {
                testOutputHelper.WriteLine(word);
            }

            foreach (var chain in generator.chains)
            {
                testOutputHelper.WriteLine(" ");
                foreach (var element in chain)
                {
                    testOutputHelper.WriteLine(element);
                }
            }
        }
    }
}