using Dapper;
using DataloaderTool.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataloaderTool.Repository
{
    public class CatalogRepository
    {
        private readonly string _connectionString;

        public CatalogRepository(string connectionString)
        {
            _connectionString = connectionString;

        }

        public void AddTitle(Title title)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                try
                {
                    sqlConnection.Execute(@"INSERT INTO TITLE (ID, TITLETYPE, PRIMARYTITLE, ORIGINALTITLE, ISADULT, STARTYEAR, ENDYEAR, RUNTIMEMINUTES)
                                     VALUES (@Id, @Type, @PrimaryTitle, @OriginalTitle, @IsAdult, @StartYear, @EndYear, @RuntimeMinutes) ", title);
                } catch (SqlException e)
                {
                    Console.WriteLine($"Error processing Id {title.Id}'s title row. Skipped." + e.Message);
                }
                foreach (var g in title.Genres)
                {
                    try
                    {
                        var reader = sqlConnection.ExecuteReader(@"SELECT ID FROM TITLEGENRES WHERE GENRENAME = @Name", new { Name = g });
                        if (!reader.Read())
                        {
                            sqlConnection.Execute(@"INSERT INTO TITLEGENRES (GENRENAME) VALUES (@Name)", new { Name = g });
                        }
                        sqlConnection.Execute(@"INSERT INTO TITLEGENRES_TITLE (TITLEID, GENREID)
                                     VALUES (@TitleId, (SELECT ID FROM TITLEGENRES WHERE GENRENAME = @Name)) ", new { TitleId = title.Id, Name = g });
                    } catch (SqlException e)
                    {
                        Console.WriteLine($"Error processing Id {title.Id}'s genre row. Skipped." + e.Message);
                    }
                }
            }
        }

        public void AddPerson(Person person)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                try
                {
                    sqlConnection.Execute(@"INSERT INTO PEOPLE (ID, PRIMARYNAME, BIRTHYEAR, DEATHYEAR)
                                     VALUES (@Id, @Name, @BirthYear, @DeathYear) ", person);
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"Error processing Id {person.Id}'s person row. Skipped." + e.Message);
                }

                foreach (var p in person.Professions)
                {
                    try
                    {
                        var reader = sqlConnection.ExecuteReader(@"SELECT ID FROM PROFESSION WHERE PROFESSIONNAME = @Name", new { Name = p });
                        if (!reader.Read())
                        {
                            sqlConnection.Execute(@"INSERT INTO PROFESSION (PROFESSIONNAME) VALUES (@Name)", new { Name = p });
                        }
                        sqlConnection.Execute(@"INSERT INTO PROFESSION_PEOPLE (PEOPLEID, PROFESSIONID)
                                     VALUES (@PeopleId, (SELECT ID FROM PROFESSION WHERE PROFESSIONNAME = @Name)) ", new { PeopleId = person.Id, Name = p });
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine($"Error processing Id {person.Id}'s profession row. Skipped." + e.Message);
                    }

                    foreach (var t in person.Titles)
                    {
                        try {
                        sqlConnection.Execute(@"INSERT INTO TITLE_PEOPLE (TITLEID, PEOPLEID) VALUES (@TitleId, @PeopleId)", new { TitleId = t, PeopleId = person.Id });
                        }
                        catch (SqlException e)
                        {
                            Console.WriteLine($"Error processing Id {person.Id}'s title row. Skipped." + e.Message);
                        }
                    }

                }
            }
        }

        public void AddRating(Rating rating)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                try
                {
                    sqlConnection.Execute(@"INSERT INTO TITLERATINGS (TITLEID, RATING, NUMVOTES)
                                     VALUES (@TitleId, @Value, @NumVotes) ", rating);
                }catch (SqlException e)
                {
                    Console.WriteLine($"Error processing Title {rating.TitleId}'s rating row. Skipped." + e.Message);
                }
            }
        }
    }
}
