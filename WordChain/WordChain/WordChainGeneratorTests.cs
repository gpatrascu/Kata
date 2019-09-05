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
        
        [Fact]
        public void When_we_dont_have_a_route()
        {
            var generator = CreateSut("aa",
                "ac",
                "cc",
                "bb");
            var result = generator.GetWordChain("aa", "bb");

            Assert.Equal(new List<string>(), result);
        }
        
        [Fact]
        public void When_we_have_the_cat_dog_example_words_in_dictionary()
        {
            var generator = CreateSut("bat","bad", "bag", "bog", "cat", "cot", "cog", "dog");
            
            var result = generator.GetWordChain("cat", "dog");

            Assert.Equal(new List<string> {"cat", "cot", "cog", "dog"}, result);
        }
    }
}