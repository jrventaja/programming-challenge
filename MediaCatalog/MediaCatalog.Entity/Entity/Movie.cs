using System;
using System.Collections.Generic;

namespace MediaCatalog.Entity
{
    public class Movie
    {
        public int Id { get; }
        public string Title { get; }
        public int? Year { get; }
        public int? RuntimeMinutes { get; }
        public decimal? Rating { get; }
        public bool IsAdult { get; }
        public string Director { get; set; }
        public IEnumerable<string> Actors { get; set; }
        public string Genre { get; }

        public Movie(int id, string title, int? year, int? runtimeMinutes, decimal? rating, bool isAdult, string genre)
        {
            Id = id;
            Title = title;
            Year = year;
            RuntimeMinutes = runtimeMinutes;
            Rating = rating;
            IsAdult = isAdult;
            Genre = genre;
        }
    }
}
