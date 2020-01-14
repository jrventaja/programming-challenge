using DataloaderTool.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DataloaderTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(
                "appsettings.json", optional: true, reloadOnChange: true
                );
            IConfigurationRoot configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("Storage");
            var titleDataPath = configuration.GetSection("Paths").GetSection("MediaData").Value;
            var peopleDataPath = configuration.GetSection("Paths").GetSection("PeopleData").Value;
            var ratingDataPath = configuration.GetSection("Paths").GetSection("RatingData").Value;
            CatalogRepository catalogRepository = new CatalogRepository(connectionString);
            var lp = new Lineparser();
            using (StreamReader sr = new StreamReader(titleDataPath))
            {
                double count = 0;
                string lineRead;
                sr.ReadLine();
                while ((lineRead = sr.ReadLine()) != null)
                {
                    catalogRepository.AddTitle(lp.ParseTitleLine(lineRead));
                    count++;
                    if (count % 1000 == 0)
                        Console.WriteLine(count + " title lines completed.");
                }
                Console.WriteLine("Finished title data.");
            }

            using (StreamReader sr = new StreamReader(peopleDataPath))
            {
                double count = 0;
                string lineRead;
                sr.ReadLine();
                while ((lineRead = sr.ReadLine()) != null)
                {
                    catalogRepository.AddPerson(lp.ParsePersonLine(lineRead));
                    count++;
                    if (count % 1000 == 0)
                        Console.WriteLine(count + " person lines completed.");
                }
                Console.WriteLine("Finished person data.");
            }

            using (StreamReader sr = new StreamReader(ratingDataPath))
            {
                string lineRead;
                sr.ReadLine();
                while ((lineRead = sr.ReadLine()) != null)
                {
                    catalogRepository.AddRating(lp.ParseRatingLine(lineRead));
                }
                Console.WriteLine("Finished rating data.");
            }

            Console.WriteLine("End");


        }
    }
}
