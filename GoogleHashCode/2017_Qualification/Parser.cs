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
            input.NumberOfEndpoints = int.Parse(firstLineSplited[0]);
            input.NumberOfRequestDescription = int.Parse(firstLineSplited[0]);
            input.NumberOfCachedServers = int.Parse(firstLineSplited[0]);
            input.ServerCapacity = int.Parse(firstLineSplited[0]);

            string[] servers = reader.ReadLine().Split(' ');
            input.Videos = new List<Video>();
            for (int i = 0; i < servers.Length; i++)
            {
                Video video = new Video(i);
                video.Size = int.Parse(servers[i]);
                input.Videos.Add(video);
            }

            for (int index = 0; index < input.NumberOfEndpoints; index++)
            {
                // Do
            }


            return input;
        }
    }
}
