using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace Przychodnia.TestyJednostkowe
{
    public class TestyRegex
    {
        [Theory]
        [InlineData("test@test.pl")]
        [InlineData("dominik123@gmail.com")]
        [InlineData("a_b-c@test.com")]
        public void WalidatorEmailPoprawneZwracaTrue(string email)
        {
            Assert.Matches(RegexPatterny.WALIDATOR_EMAIL, email);
        }

        [Theory]
        [InlineData("")]
        [InlineData("test")]
        [InlineData("@test.pl")]
        [InlineData("test@")]
        [InlineData("test@@test.pl")]
        [InlineData("test@.pl")]
        [InlineData("test@test")]
        [InlineData(" spacja@test.pl")]
        public void WalidatorEmailNiepoprawnyZwracaFalse(string email)
        {
            Assert.DoesNotMatch(RegexPatterny.WALIDATOR_EMAIL, email);
        }
    }
}
