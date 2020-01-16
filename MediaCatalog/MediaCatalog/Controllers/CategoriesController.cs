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
    public class CategoriesController : ControllerBase
    {
        public CategoriesController() // ADD DI
        {

        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAll()
        {
            return null;
        }
    }
}
