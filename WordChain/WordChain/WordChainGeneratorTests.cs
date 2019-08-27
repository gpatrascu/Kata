namespace WordChain
{
    using Xunit;

    public class WordChainGeneratorTests
    {
        private const string Case1 = "cat,cot,cog,dog";

        [Fact]
        public void GetWordChainForAbacaAback()
        {
            string firstWord = "abaca";
            string lastWord = "aback";

            var generator = new WordChainGenerator(firstWord, lastWord);
            var wordChainResult = generator.GetWordChain();

            Assert.Equal("abaca aback", wordChainResult);
        }
        
        [Fact]
        public void GetWordChainForAbackAbaca()
        {
            string firstWord = "aback";
            string lastWord = "abaca";

            var generator = new WordChainGenerator(firstWord, lastWord);
            var wordChainResult = generator.GetWordChain();

            Assert.Equal("aback abaca", wordChainResult);
        } 
        
        [Fact]
        public void GetWordChainForCatCotCog()
        {
            string firstWord = "cat";
            string lastWord = "cog";

            var generator = new WordChainGenerator(firstWord, lastWord);
            var wordChainResult = generator.GetWordChain();

            Assert.Equal("cat cot cog", wordChainResult);
        }
    }
}
