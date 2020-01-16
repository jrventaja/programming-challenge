using System.Collections.Generic;
using MediaCatalog.Entity;
using MediaCatalog.Entity.Entity;
using MediaCatalog.Entity.Model;

namespace MediaCatalog.Entity.Repository
{
    public interface ICatalogRepository
    {
        IEnumerable<Category> GetCategories();
        Page<Movie> GetNonAdultMoviesByCategory(int category, int page, int pageSize);
        Page<Movie> GetNonAdultTopMovies(int? year, int page, int pageSize, int minimumVotes);
    }
}