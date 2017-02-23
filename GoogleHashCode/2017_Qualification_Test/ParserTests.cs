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

            Assert.AreEqual(50, input.Videos[0].Size);
            Assert.AreEqual(50, input.Videos[1].Size);
            Assert.AreEqual(80, input.Videos[2].Size);
            Assert.AreEqual(30, input.Videos[3].Size);
            Assert.AreEqual(110, input.Videos[4].Size);

            Assert.AreEqual(1000, input.Endpoints[0].DataCenterLatency);
            Assert.AreEqual(3, input.Endpoints[0].ServersLatency.Count);

            Assert.AreEqual(500, input.Endpoints[1].DataCenterLatency);
            Assert.AreEqual(0, input.Endpoints[1].ServersLatency.Count);

            Assert.AreEqual(1500, input.RequestsDescriptions[0].NumOfRequests);
            Assert.AreEqual(1000, input.RequestsDescriptions[1].NumOfRequests);
            Assert.AreEqual(500, input.RequestsDescriptions[2].NumOfRequests);
            Assert.AreEqual(1000, input.RequestsDescriptions[3].NumOfRequests);
        }
    }
}
