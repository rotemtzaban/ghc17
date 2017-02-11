using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _2015_Qualification;
using HashCodeCommon;
using System.Collections.Generic;

namespace _2015_Qualification_Test
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void Parser_Test()
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

        [TestMethod]
        public void Parser_TestBla()
        {
            Parser parser = new Parser();
            ProblemInput input = parser.ParseFromData(Properties.Resources.RealInput);

            List<Server> servers = input.Servers.OrderByDescending(_ => _.Capacity).ToList();
            List<Server> servers1 = input.Servers.OrderByDescending(_ => _.Slots).ToList();
            List<Server> servers2 = input.Servers.OrderByDescending(_ => ((double)_.Capacity) / _.Slots).ToList();

            int count = servers.Sum(_ => _.Slots);
        }
    }
}
