using DataloaderTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataloaderTool
{
    public class Lineparser
    {
        private readonly int COLUMNDELIMITERASCIICODE = 9;
        private readonly char DELIMITER = ',';

        public Person ParsePersonLine(string input)
        {
            var splittedInput = input.Split(Convert.ToChar(COLUMNDELIMITERASCIICODE));
            var newPerson = new Person();
            newPerson.Id = int.Parse(splittedInput[0].Substring(2));
            newPerson.Name = splittedInput[1];
            if (splittedInput[2] == "\\N")
                newPerson.BirthYear = null;
            else
                newPerson.BirthYear = int.Parse(splittedInput[2]);
            if (splittedInput[3] == "\\N")
                newPerson.DeathYear = null;
            else
                newPerson.DeathYear = int.Parse(splittedInput[3]);
            newPerson.Professions = splittedInput[4].Split(DELIMITER).ToList();
            if (splittedInput[5] != "\\N")
            {
                var titlesArray = Array.ConvertAll(splittedInput[5].Replace("tt", string.Empty).Split(DELIMITER), item => int.Parse(item));
                newPerson.Titles = titlesArray.ToList();
            }
            else
                newPerson.Titles = new List<int>();
            return newPerson;
        }

        public Title ParseTitleLine(string input)
        {
            var splittedInput = input.Split(Convert.ToChar(COLUMNDELIMITERASCIICODE));
            var newTitle = new Title();
            newTitle.Id = int.Parse(splittedInput[0].Substring(2));
            newTitle.Type = splittedInput[1];
            newTitle.PrimaryTitle = splittedInput[2];
            newTitle.OriginalTitle = splittedInput[3];
            newTitle.IsAdult = splittedInput[4] == "1" ? true : false;
            if (splittedInput[5] == "\\N")
                newTitle.StartYear = null;
            else
                newTitle.StartYear = int.Parse(splittedInput[5]);
            if (splittedInput[6] == "\\N")
                newTitle.EndYear = null;
            else
                newTitle.EndYear = int.Parse(splittedInput[6]);
            if (splittedInput[7] == "\\N")
                newTitle.RuntimeMinutes = null;
            else
                newTitle.RuntimeMinutes = int.Parse(splittedInput[7]);
            newTitle.Genres = splittedInput[8].Split(DELIMITER).ToList();
            return newTitle;
        }

        public Rating ParseRatingLine(string input)
        {
            var splittedInput = input.Split(Convert.ToChar(COLUMNDELIMITERASCIICODE));
            var newRating = new Rating();
            newRating.TitleId = int.Parse(splittedInput[0].Substring(2));
            newRating.Value = decimal.Parse(splittedInput[1], System.Globalization.CultureInfo.InvariantCulture);
            newRating.NumVotes = int.Parse(splittedInput[2]);
            return newRating;
        }
    }
}
