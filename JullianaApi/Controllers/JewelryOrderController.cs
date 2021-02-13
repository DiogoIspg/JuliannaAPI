using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JullianaDomainCore.Entity;

namespace JullianaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JewelryOrderController : ControllerBase
    {
        private readonly ILogger<JewelryOrderController> logger;

        private readonly IDbContext dbContext;

        public JewelryOrderController(ILogger<JewelryOrderController> logger, IDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<JewelryOrder> Get()
        {
            return dbContext.Set<JewelryOrder>();
        }
    }
}