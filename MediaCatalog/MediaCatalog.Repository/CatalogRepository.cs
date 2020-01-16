using Dapper;
using MediaCatalog.Entity;
using MediaCatalog.Entity.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MediaCatalog.Repository
{
    public class CatalogRepository
    {
        private string _connectionString;

        public CatalogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Page<Movie> GetNonAdultMoviesByCategory(int category, int page, int pageSize)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                string orderByString = $"ORDER BY TITLEGENRES_TITLE.ID OFFSET {(page - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";

                var sqlQuery = $@"      SELECT
											Title.ID As Id,
											Title.PRIMARYTITLE As Title,
                                            Title.STARTYEAR As Year,
                                            Title.RUNTIMEMINUTES As RuntimeMinutes,
                                            TitleRatings.RATING As Rating,
                                            Title.ISADULT As IsAdult,
											TitleGenres.GENRENAME As Genre
										FROM
                                            Title INNER JOIN TITLEGENRES_TITLE ON Title.ID = TITLEGENRES_TITLE.TITLEID
                                                INNER JOIN TitleRatings ON TitleRatings.TITLEID = Title.ID 
                                        WHERE
                                            TITLEGENRES_TITLE.GENREID = {category} AND ISADULT = 0 and RATING >= 6
                                            {orderByString}";

                var movieQueryResult = sqlConnection.Query<Movie>(sqlQuery);
                foreach (Movie m in movieQueryResult)
                {
                    m.Actors = sqlConnection.Query<string>($@" SELECT DISTINCT(People.PRIMARYNAME)
                                                    FROM
                                                        Title_People INNER JOIN People ON Title_People.PEOPLEID = People.ID
                                                        INNER JOIN Profession_People ON Profession_People.PEOPLEID = People.ID
                                                        INNER JOIN Profession ON Profession.ID = Profession_People.PROFESSIONID
                                                    WHERE TITLEID = {m.Id} AND PROFESSIONNAME IN ('actor','actress')");
                    m.Director = sqlConnection.QueryFirst<string>($@" SELECT People.PRIMARYNAME
                                                    FROM
                                                        Title_People INNER JOIN People ON Title_People.PEOPLEID = People.ID
                                                        INNER JOIN Profession_People ON Profession_People.PEOPLEID = People.ID
                                                        INNER JOIN Profession ON Profession.ID = Profession_People.PROFESSIONID
                                                    WHERE TITLEID = {m.Id} AND PROFESSIONNAME = 'director'");
                }
                return new Page<Movie>
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    Content = movieQueryResult
                };
            }
        }

        public Page<Movie> GetNonAdultTopMovies(int? year, int page, int pageSize)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {

                string orderByString = $"ORDER BY Rating DESC OFFSET {(page - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";

                var sqlQuery = $@"      SELECT
											Title.ID As Id,
											Title.PRIMARYTITLE As Title,
                                            Title.STARTYEAR As Year,
                                            Title.RUNTIMEMINUTES As RuntimeMinutes,
                                            TitleRatings.RATING As Rating,
                                            Title.ISADULT As IsAdult,
											TitleGenres.GENRENAME As Genre
										FROM
                                            Title INNER JOIN TITLEGENRES_TITLE ON Title.ID = TITLEGENRES_TITLE.TITLEID
                                                INNER JOIN TitleRatings ON TitleRatings.TITLEID = Title.ID 
                                        {(year == null ? string.Empty : $"WHERE STARTYEAR = { year }")}";

                var movieQueryResult = sqlConnection.Query<Movie>(sqlQuery);
                foreach (Movie m in movieQueryResult)
                {
                    m.Actors = sqlConnection.Query<string>($@" SELECT DISTINCT(People.PRIMARYNAME)
                                                    FROM
                                                        Title_People INNER JOIN People ON Title_People.PEOPLEID = People.ID
                                                        INNER JOIN Profession_People ON Profession_People.PEOPLEID = People.ID
                                                        INNER JOIN Profession ON Profession.ID = Profession_People.PROFESSIONID
                                                    WHERE TITLEID = {m.Id} AND PROFESSIONNAME IN ('actor','actress')");
                    m.Director = sqlConnection.QueryFirst<string>($@" SELECT People.PRIMARYNAME
                                                    FROM
                                                        Title_People INNER JOIN People ON Title_People.PEOPLEID = People.ID
                                                        INNER JOIN Profession_People ON Profession_People.PEOPLEID = People.ID
                                                        INNER JOIN Profession ON Profession.ID = Profession_People.PROFESSIONID
                                                    WHERE TITLEID = {m.Id} AND PROFESSIONNAME = 'director'");
                }
                return new Page<Movie>
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    Content = movieQueryResult
                };
            }
        }

    }
}
