using System.Collections.Generic;
using Xunit;

namespace WordChain
{
    public class WordChainGeneratorTests
    {
        private static WordChainGenerator CreateSut(params string[] dictionary)
        {
            var generator = new WordChainGenerator(dictionary);
            return generator;
        }

        [Fact]
        public void ShouldReturnTheDictionaryIfDictionaryOnlyHas2characters()
        {
            var generator = CreateSut("a", "b");
            var result = generator.GetWordChain("a", "b");

            Assert.Equal(new List<string> {"a", "b"}, result);
        }


        [Fact]
        public void When_we_have_3_words_in_dictionary()
        {
            var generator = CreateSut("aa", "ab", "bb");
            var result = generator.GetWordChain("aa", "bb");

            Assert.Equal(new List<string> {"aa", "ab", "bb"}, result);
        }

        [Fact]
        public void When_we_have_4_words_in_dictionary()
        {
            var generator = CreateSut("aa",
                "ab",
                "ac",
                "bb");
            var result = generator.GetWordChain("aa", "bb");

            Assert.Equal(new List<string> {"aa", "ab", "bb"}, result);
        }
    }
}