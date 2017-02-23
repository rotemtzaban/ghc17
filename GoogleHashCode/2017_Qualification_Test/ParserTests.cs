using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _2017_Qualification;

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

            Assert.AreEqual(3, input.NumberOfCachedServers);
            Assert.AreEqual(2, input.NumberOfEndpoints);
            Assert.AreEqual(4, input.NumberOfRequestDescription);
            Assert.AreEqual(5, input.NumberOfVideos);
            Assert.AreEqual(100, input.ServerCapacity);
            Assert.Fail();
        }
    }
}
