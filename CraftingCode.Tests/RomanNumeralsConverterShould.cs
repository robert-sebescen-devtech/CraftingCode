using System;
using FluentAssertions;
using Xunit;

namespace CraftingCode.Tests
{
    public class RomanNumeralsConverterShould
    {
        [Theory]
        [InlineData(1,"I")]
        [InlineData(2,"II")]
        [InlineData(3,"III")]
        [InlineData(5,"V")]
        [InlineData(7,"VII")]
        [InlineData(10,"X")]
        [InlineData(18,"XVIII")]
        public void ConvertDecimalToARomanNumber(int arabicNumeral, string romanNumeral)
        {
            RomanNumeralsConverter.Convert(arabicNumeral).Should().Be(romanNumeral);
        }
    }
}
