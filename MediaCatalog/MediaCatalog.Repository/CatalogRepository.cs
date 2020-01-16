using Dapper;
using MediaCatalog.Entity;
using MediaCatalog.Entity.Entity;
using MediaCatalog.Entity.Model;
using MediaCatalog.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MediaCatalog.Repository
{
    public class CatalogRepository : ICatalogRepository
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
                string orderByString = $"ORDER BY ID OFFSET {(page - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";

                var sqlQuery = $@"      SELECT
											Title.ID As Id,
											Title.PRIMARYTITLE As Title,
                                            Title.STARTYEAR As Year,
                                            Title.RUNTIMEMINUTES As RuntimeMinutes,
                                            TitleRatings.RATING As Rating,
                                            Title.ISADULT As IsAdult
										FROM
                                            Title INNER JOIN TITLEGENRES_TITLE ON Title.ID = TITLEGENRES_TITLE.TITLEID
                                                INNER JOIN TitleRatings ON TitleRatings.TITLEID = Title.ID 
                                        WHERE
                                            TITLETYPE = 'movie' AND TITLEGENRES_TITLE.GENREID = {category} AND ISADULT = 0 and RATING >= 6
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
                    m.Director = sqlConnection.QueryFirstOrDefault<string>($@" SELECT People.PRIMARYNAME
                                                    FROM
                                                        Title_People INNER JOIN People ON Title_People.PEOPLEID = People.ID
                                                        INNER JOIN Profession_People ON Profession_People.PEOPLEID = People.ID
                                                        INNER JOIN Profession ON Profession.ID = Profession_People.PROFESSIONID
                                                    WHERE TITLEID = {m.Id} AND PROFESSIONNAME = 'director'");
                    m.Genres = sqlConnection.Query<string>($@" SELECT GENRENAME
                                                    FROM
                                                        TitleGenres INNER JOIN TitleGenres_Title ON TitleGenres.ID = TitleGenres_Title.GENREID
                                                    WHERE TITLEID = {m.Id}");
                }
                return new Page<Movie>
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    Content = movieQueryResult
                };
            }
        }

        public Page<Movie> GetNonAdultTopMovies(int? year, int page, int pageSize, int minimumVotes)
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
                                            Title.ISADULT As IsAdult
										FROM
                                            Title INNER JOIN TitleRatings ON TitleRatings.TITLEID = Title.ID
                                        WHERE
                                            RATING >= 6 AND NUMVOTES >= {minimumVotes} AND TITLETYPE = 'movie' AND ISADULT = 0 {(year == null ? string.Empty : $"AND STARTYEAR = { year }")} {orderByString}";

                var movieQueryResult = sqlConnection.Query<Movie>(sqlQuery);
                foreach (Movie m in movieQueryResult)
                {
                    m.Actors = sqlConnection.Query<string>($@" SELECT DISTINCT(People.PRIMARYNAME)
                                                    FROM
                                                        Title_People INNER JOIN People ON Title_People.PEOPLEID = People.ID
                                                        INNER JOIN Profession_People ON Profession_People.PEOPLEID = People.ID
                                                        INNER JOIN Profession ON Profession.ID = Profession_People.PROFESSIONID
                                                    WHERE TITLEID = {m.Id} AND PROFESSIONNAME IN ('actor','actress')");
                    m.Director = sqlConnection.QueryFirstOrDefault<string>($@" SELECT People.PRIMARYNAME
                                                    FROM
                                                        Title_People INNER JOIN People ON Title_People.PEOPLEID = People.ID
                                                        INNER JOIN Profession_People ON Profession_People.PEOPLEID = People.ID
                                                        INNER JOIN Profession ON Profession.ID = Profession_People.PROFESSIONID
                                                    WHERE TITLEID = {m.Id} AND PROFESSIONNAME = 'director'");
                    m.Genres = sqlConnection.Query<string>($@" SELECT GENRENAME
                                                    FROM
                                                        TitleGenres INNER JOIN TitleGenres_Title ON TitleGenres.ID = TitleGenres_Title.GENREID
                                                    WHERE TITLEID = {m.Id}");
                }
                return new Page<Movie>
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    Content = movieQueryResult
                };
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"      SELECT
											ID,
											GENRENAME As Name
										FROM
                                            TitleGenres";
                                       

                var categories = sqlConnection.Query<Category>(sqlQuery);
                return categories;
            }
        }

    }
}
