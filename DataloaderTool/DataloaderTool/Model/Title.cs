using System;
using System.Collections.Generic;
using System.Text;

namespace DataloaderTool.Model
{
    public class Title
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? RuntimeMinutes { get; set; }
        public IEnumerable<string> Genres { get; set; }
    }
}
