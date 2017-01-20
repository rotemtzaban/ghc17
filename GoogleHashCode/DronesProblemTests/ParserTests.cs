using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DronesProblem;

namespace DronesProblemTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Parser_CheckOnExample()
        {
            DronesParser parser = new DronesParser();
            DronesInput input = parser.ParseFromData(Properties.Resources.Example);

            Assert.AreEqual(100, input.NumOfRows);
            Assert.AreEqual(100, input.NumOfColumns);
            Assert.AreEqual(50, input.NumOfTurns);
            Assert.AreEqual(500, input.MaxWeight);
            Assert.AreEqual(3, input.Drones.Count);
            Assert.AreEqual(2, input.WareHouses.Count);
            Assert.AreEqual(3, input.Products.Count);
            Assert.AreEqual(3, input.Orders.Count);
            
            // I Checked the items in debug
        }
    }
}
