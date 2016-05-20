using System;
using FluentAssertions;
using Xunit;

namespace CraftingCode.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void FailingTest()
        {
            //Assert.True(false);
            true.Should().BeFalse();
        }
    }
}
