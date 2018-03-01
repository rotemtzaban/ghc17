using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2017_Qualification
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            string[] firstLineSplited = reader.ReadLine().Split(' ');
            input.NumberOfVideos = int.Parse(firstLineSplited[0]);
            input.NumberOfEndpoints = int.Parse(firstLineSplited[1]);
            input.NumberOfRequestDescription = int.Parse(firstLineSplited[2]);
            input.NumberOfCachedServers = int.Parse(firstLineSplited[3]);
            input.ServerCapacity = int.Parse(firstLineSplited[4]);



            input.CachedServers = new List<CachedServer>();
            for (int i = 0; i < input.NumberOfCachedServers; i++)
            {
                input.CachedServers.Add(new CachedServer(i) { Capacity = input.ServerCapacity });
            }

            string[] videos = reader.ReadLine().Split(' ');
            input.Videos = new List<Video>();
            for (int i = 0; i < videos.Length; i++)
            {
                Video video = new Video(i);
                video.Size = int.Parse(videos[i]);
                input.Videos.Add(video);
            }

            input.Endpoints = new List<_2017_Qualification.EndPoint>();
            for (int index = 0; index < input.NumberOfEndpoints; index++)
            {
                string[] currDescription = reader.ReadLine().Split(' ');
                EndPoint endPoint = new _2017_Qualification.EndPoint(index);
                input.Endpoints.Add(endPoint);
                endPoint.DataCenterLatency = int.Parse(currDescription[0]);
                endPoint.ServersLatency = new Dictionary<CachedServer, int>();
                for (int i = 0; i < int.Parse(currDescription[1]); i++)
                {
                    string[] latencyServer = reader.ReadLine().Split(' ');
                    CachedServer server = input.CachedServers[int.Parse(latencyServer[0])];
                    int latency = int.Parse(latencyServer[1]);
                    endPoint.ServersLatency.Add(server, latency);
                }
            }

            input.RequestsDescriptions = new List<RequestsDescription>();
            for (int i = 0; i < input.NumberOfRequestDescription; i++)
            {
                string[] reqDesc = reader.ReadLine().Split(' ');
                RequestsDescription desc = new _2017_Qualification.RequestsDescription(i);
                desc.NumOfRequests = int.Parse(reqDesc[2]);
                desc.Video = input.Videos[int.Parse(reqDesc[0])];
                desc.Endpoint = input.Endpoints[int.Parse(reqDesc[1])];
                input.RequestsDescriptions.Add(desc);
            }

            return input;
        }
    }
}
