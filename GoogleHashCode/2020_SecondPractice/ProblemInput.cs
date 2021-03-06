﻿using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_SecondPractice
{
    public class ProblemInput
    {
        public bool[,] Slots { get; set; }
        public int NumOfRows { get; set; }
        public int RowSize { get; set; }
        public int NumOfServers { get; set; }
        public List<PoolDetails> Pools { get; set; }
        public int NumOfPools { get; set; }
        public int NumOfUnavliableSlots { get; set; }
        public List<Server> Servers { get; set; }
    }
}
