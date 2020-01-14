using System;
using System.Collections.Generic;
using System.Text;

namespace DataloaderTool.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }
        public IEnumerable<string> Professions { get; set; }
        public IEnumerable<int> Titles { get; set; }
    }
}
