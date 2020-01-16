using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCatalog.Entity;
using MediaCatalog.Entity.Service;
using Microsoft.AspNetCore.Mvc;

namespace MediaCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICatalogSearchService _catalogSearchService;

        public CategoriesController(ICatalogSearchService catalogSearchService)
        {
            _catalogSearchService = catalogSearchService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAll()
        {
            return Ok(_catalogSearchService.GetCategories());
        }
    }
}
