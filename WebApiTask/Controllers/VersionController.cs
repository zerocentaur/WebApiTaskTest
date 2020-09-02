using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTask.Models;

namespace WebApiTask.Controllers
{
    [ApiController]
    [Route("api/version")]
    public class VersionController : ControllerBase
    {
        private DatabaseContext DbContext { get; set; }

        public VersionController(DatabaseContext dbContext)
        {
            DbContext = dbContext;
            if (!DbContext.Versions.Any())
            {
                DbContext.Versions.Add(new VersionModel() { Name = "version1", Type = "Draft" });
                DbContext.Versions.Add(new VersionModel() { Name = "version2", Type = "Draft" });
                DbContext.Versions.Add(new VersionModel() { Name = "version3", Type = "Draft" });
                DbContext.Versions.Add(new VersionModel() { Name = "version4", Type = "Main" });

                DbContext.SaveChanges();
            }
        }

        // Gets versions list
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VersionModel>>> Get()
        {
            return await DbContext.Versions.ToListAsync();
        }

        // GET api/version/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingModel>> Get(int id)
        {
            BuildingModel buildingModel = await DbContext.Buildings.FirstOrDefaultAsync(x => x.Id == id);
            if (buildingModel == null)
                return NotFound();
            return new ObjectResult(buildingModel);
        }

        // POST api/version
        [HttpPost]
        public async Task<ActionResult<BuildingModel>> Post(BuildingModel buildingModel)
        {
            if (buildingModel == null)
            {
                return BadRequest();
            }

            DbContext.Buildings.Add(buildingModel);
            await DbContext.SaveChangesAsync();
            return Ok(buildingModel);
        }

        // PUT api/version/
        [HttpPut]
        public async Task<ActionResult<BuildingModel>> Put(BuildingModel buildingModel)
        {
            if (buildingModel == null)
            {
                return BadRequest();
            }
            if (!DbContext.Buildings.Any(x => x.Id == buildingModel.Id))
            {
                return NotFound();
            }

            DbContext.Update(buildingModel);
            await DbContext.SaveChangesAsync();
            return Ok(buildingModel);
        }

        // DELETE api/version/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BuildingModel>> Delete(int id)
        {
            BuildingModel buildingModel = DbContext.Buildings.FirstOrDefault(x => x.Id == id);
            if (buildingModel == null)
            {
                return NotFound();
            }
            DbContext.Buildings.Remove(buildingModel);
            await DbContext.SaveChangesAsync();
            return Ok(buildingModel);
        }
    }
}