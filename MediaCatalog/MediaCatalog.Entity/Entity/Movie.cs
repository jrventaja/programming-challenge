using System;
using System.Collections.Generic;

namespace MediaCatalog.Entity
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public int? RuntimeMinutes { get; set; }
        public decimal? Rating { get; set; }
        public bool IsAdult { get; set; }
        public string Director { get; set; }
        public IEnumerable<string> Actors { get; set; }
        public IEnumerable<string> Genres { get; set; }
    }
}
