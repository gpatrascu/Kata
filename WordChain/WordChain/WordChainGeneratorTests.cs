using System.Collections.Generic;
using Xunit;

namespace WordChain
{
    public class WordChainGeneratorTests
    {
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
    }
}