using System;
using Xunit;
using back_end;
using PassThePigsGame.Program;
using PassThePigsGame.PassThePigsClass;

namespace back_end.Tests
{
    public class UnitTests
    {
        PigThrows PigPass = new PigThrows();

        [Fact]
        public void PassingAddTest()
        {
            Assert.Equal(4, Program.Add(2, 2));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void TheoryTest(int myNum)
        {
            Assert.True(Program.IsOdd(myNum));
        }
    }
}
