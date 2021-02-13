using JullianaDomainCore.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JullianaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JewelrySavedController : ControllerBase
    {
        private readonly IDbContext dbContext;

        public JewelrySavedController(IDbContext db)
        {
            dbContext = db;
        }

        [HttpGet]
        public IEnumerable<JewelrySaved> Get()
        {
            return dbContext.Set<JewelrySaved>();
        }

        [HttpGet("{id}")]
        public JewelrySaved Get(string id)
        {
            return dbContext.Set<JewelrySaved>().FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public void Post([FromBody] JewelrySaved value)
        {
            dbContext.Set<JewelrySaved>().Add(value);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            if (id == null)
                return;

            dbContext.Set<JewelrySaved>().Remove(dbContext.Set<JewelrySaved>().FirstOrDefault(x => x.Id == id));
        }
    }
}