using ContestProjectWPF;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContestProjectTests
{
    [TestClass]
    public class ContestTest
    {
        [TestMethod]
        public void Test1()
        {
            var c = new Code();

            var input = @"";

            var result = c.Execute(input);
            result.Should().Be(@"");
        }

        [TestMethod]
        public void Test2()
        {
            var c = new Code();

            var input = @"";

            var result = c.Execute(input);
            result.Should().Be(@"");
        }
    }
}