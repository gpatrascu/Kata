namespace WordChain
{
    using Xunit;

    public class UnitTest1
    {
        private const string Case1 = "cat,cot,cog,dog";

        [Fact]
        public void GetWordChainForCatDog()
        {
            string firstWord = "cat";
            string lastWord = "cog";

            var generator = new WordChainGenerator(firstWord, lastWord);
            var wordChainResult = generator.GetWordChain();

            Assert.Equal("cat cot cog", wordChainResult);
        }
    }
}
