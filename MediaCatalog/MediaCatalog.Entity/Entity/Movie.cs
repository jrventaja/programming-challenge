using System;
using System.Collections.Generic;

namespace MediaCatalog.Entity
{
    public class Movie
    {
        public string Title { get; private set; }
        public int Year { get; private set; }
        public int RuntimeMinutes { get; private set; }
        public decimal Rating { get; private set; }
        public bool IsAdult { get; private set; }
        public string Director { get; private set; }
        public IEnumerable<string> Actors { get; private set; }
    }
}
