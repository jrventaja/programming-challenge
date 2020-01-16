using System;
using System.Collections.Generic;
using System.Text;

namespace DataloaderTool.Model
{
    public class Rating
    {
        public int TitleId { get; set; }
        public decimal Value { get; set; }
        public int NumVotes { get; set; }
    }
}
