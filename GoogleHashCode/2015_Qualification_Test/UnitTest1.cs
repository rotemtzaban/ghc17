using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _2015_Qualification;
using HashCodeCommon;

namespace _2015_Qualification_Test
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Parser parser = new Parser();
            ProblemInput input = parser.ParseFromData(Properties.Resources.TestInput);

            Assert.AreEqual(2, input.Rows);
            Assert.AreEqual(5, input.Columns);
            Assert.AreEqual(2, input.Pools.Count);
            Assert.AreEqual(1, input.UnavilableSlots.Count);
            Assert.AreEqual(5, input.Servers.Count);

            Assert.AreEqual(new Coordinate(0,0), input.UnavilableSlots[0]);
        }
    }
}
