using System;

namespace MediaCatalog.Repository
{
    public class MoviesRepository
    {
        private string _connectionString;

        public MoviesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }



    }
}
