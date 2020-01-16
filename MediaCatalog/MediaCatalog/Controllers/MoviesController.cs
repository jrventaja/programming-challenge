using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCatalog.Entity;
using MediaCatalog.Entity.Model;
using MediaCatalog.Entity.Service;
using Microsoft.AspNetCore.Mvc;

namespace MediaCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ICatalogSearchService _catalogSearchService;

        public MoviesController(ICatalogSearchService catalogSearchService)
        {
            _catalogSearchService = catalogSearchService;
        }

        [HttpGet("top")]
        public ActionResult<Page<Movie>> GetTopMovies(int? year, int? page, int? pageSize)
        {
            if ((year ?? 1) <= 0 || (page ?? 1) <= 0 || (pageSize ?? 1) <= 0)
                return BadRequest();

            return Ok(_catalogSearchService.GetNonAdultTopMovies(year, page, pageSize));
            
        }
        
        [HttpGet]
        public ActionResult<Movie> GetMoviesByCategory(int category, int? page, int? pageSize)
        {
            if (category <= 0 || (page ?? 1) <= 0 || (pageSize ?? 1) <= 0)
                return BadRequest();

            return Ok(_catalogSearchService.GetNonAdultMoviesByCategory(category, page, pageSize));
        }
    }
}

