using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCatalog.Entity;
using Microsoft.AspNetCore.Mvc;

namespace MediaCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get(int year, string orderBy)
        {
            return null;
        }
        
        [HttpGet("{id}")]
        public ActionResult<Movie> Get(int id)
        {
            return null;
        }
    }
}
