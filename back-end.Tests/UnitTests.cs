using System;
using Xunit;
using back_end;
using PassThePigsGame.Program;
using PassThePigsGame.PlayMethods;

namespace back_end.Tests
{
    public class UnitTests
    {
        // PassThePigs myGame = new PassThePigs();

        [Fact(Skip = "This test was a practice example")]
        public void PassingAddTest()
        {
            Assert.Equal(4, Program.Add(2, 2));
        }

        [Theory(Skip = "This test was a practice example")]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void TheoryTest(int myNum)
        {
            Assert.True(Program.IsOdd(myNum));
        }
    }
}
