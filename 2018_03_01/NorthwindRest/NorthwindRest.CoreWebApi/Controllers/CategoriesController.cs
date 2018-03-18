using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindRest.DataAccess;

namespace NorthwindRest.CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private NorthwindDb m_Db;

        public CategoriesController(ILogger<Controller> logger, NorthwindDb db)
        {
            logger.LogInformation("Ctor von ValuesController gerufen");
            m_Db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var list = await Task.Run<IEnumerable<Category>>(() => m_Db.Categories.ToList());
            return Ok(list);
        }

        [HttpGet("{id}", Name ="GetId")]
        public async Task<IActionResult> Get(int id)
        {
            var cat = await Task.Run<Category>(
                () => m_Db.Categories.SingleOrDefault(c => c.CategoryId == id));
            if (cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
        }

        [HttpPost("new")]
        public async Task<IActionResult> Post([FromBody]Category value)
        {
            string link;
            try
            {
                var cat = await Task.Run<Category>(
                    () => m_Db.Categories.SingleOrDefault(c => c.CategoryId == value.CategoryId));
                if (cat == null)
                {
                    m_Db.Categories.Add(value);
                    await m_Db.SaveChangesAsync();
                    link = Url.Link("GetId", new { id = value.CategoryId });
                    return Created(link, value);
                }
                cat.CategoryName = value.CategoryName;
                cat.Description = value.Description;
                await m_Db.SaveChangesAsync();
                link = Url.Link("GetId",new { id = cat.CategoryId });
                return Accepted(link, cat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
