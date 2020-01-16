using MediaCatalog.Entity.Entity;
using MediaCatalog.Entity.Model;
using MediaCatalog.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaCatalog.Entity.Service
{
    public class CatalogSearchService : ICatalogSearchService
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly int MINIMUMVOTESTORATE = 1000;
        private readonly int DEFAULTPAGESIZE = 10;

        public CatalogSearchService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _catalogRepository.GetCategories();
        }

        public Page<Movie> GetNonAdultTopMovies(int? year, int? page, int? pageSize)
        {
            return _catalogRepository.GetNonAdultTopMovies(year, page ?? 1, pageSize ?? DEFAULTPAGESIZE, MINIMUMVOTESTORATE);
        }

        public Page<Movie> GetNonAdultMoviesByCategory(int category, int? page, int? pageSize)
        {
            return _catalogRepository.GetNonAdultMoviesByCategory(category, page ?? 1, pageSize ?? DEFAULTPAGESIZE);
        }


    }
}
