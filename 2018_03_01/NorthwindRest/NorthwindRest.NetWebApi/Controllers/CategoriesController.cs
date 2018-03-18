using NorthwindRest.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;

namespace NorthwindRest.NetWebApi.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        [HttpGet, Route("all")]
        public async Task<IHttpActionResult> GetAll()
        {
            var db = new NorthwindDb();
            // Dann sind die Produkte nicht mehr gefüllt
            // db.Configuration.LazyLoadingEnabled = false;
            // Dann werden echte Category-Objekte (kleiner) verwendet
            db.Configuration.ProxyCreationEnabled = false;
            var repo = new GenericRepository<Category>(db);
            var result = await Task.Run<IEnumerable<Category>>(()=> repo.Find().ToList());
            db.Dispose();
            return Ok(result);
        }

        [HttpGet, Route("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost, Route("new")]
        public IHttpActionResult Post([FromBody]Category value)
        {
            value.CategoryId = 9;

            var link = Url.Link("DefaultApi", 
                new { controller = "Categories", id = value.CategoryId });
            return Created(link, value);
        }
    }
}