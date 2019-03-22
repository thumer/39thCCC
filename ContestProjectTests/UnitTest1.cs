using System;
using ContestProjectWPF;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContestProjectTests
{
    [TestClass]
    public class Code2Tests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var c = new Code();

            var input = @"1000 9999 4
3736 7 6
0 0 0 0 0 0
0 0 0 0 0 0
0 0 0 0 0 0
0 0 0 0 0 0
0 0 0 0 0 0
0 0 0 0 0 0
0 0 0 0 0 0
4260 7 5
0 0 0 0 0
0 0 0 0 0
0 0 0 698 639
0 0 0 553 113
0 0 0 0 0
0 0 0 0 0
0 0 0 0 0
6547 7 5
0 0 0 0 0
0 0 0 0 0
0 0 29 0 0
0 478 395 0 0
0 617 0 0 0
0 0 0 0 0
0 0 0 0 0
7263 5 5
0 843 2 0 0
0 250 602 0 0
0 0 0 0 0
0 0 0 0 0
0 0 0 0 0";

            c.Execute(input).Should().Be(
@"4260 7263 2
6547 6547 1");
        }

        [TestMethod]
        public void TestMethod2()
        {
            var c = new Code();

            var input = @"1000 9999 4
3940 6 3
0 0 0
0 755 0
0 437 682
0 0 791
0 0 0
0 0 0
4182 5 5
0 0 0 0 0
0 0 0 0 0
0 275 619 0 0
0 435 171 0 0
0 0 0 0 0
5300 6 6
0 0 0 0 0 0
0 0 0 0 0 0
0 0 0 0 0 0
0 0 0 0 0 0
0 0 0 0 0 0
0 0 0 0 0 0
5496 3 4
0 268 0 0
0 234 13 0
0 0 371 0";

            c.Execute(input).Should().Be(
@"3940 5496 2
4182 4182 1");
        }
    }
}
