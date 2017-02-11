using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _2015_Qualification;

namespace _2017_Qualification_Test
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Parser_Test1()
        {
            Parser parser = new Parser();
            ProblemInput input = parser.ParseFromData(Properties.Resources.ExampleInput);

            Assert.AreEqual(2, input.Rows);
            Assert.AreEqual(5, input.Columns);
            Assert.Fail();
        }
    }
}
