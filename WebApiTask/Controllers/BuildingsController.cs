using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTask.Models;

namespace WebApiTask.Controllers
{
    [ApiController]
    [Route("api/building")]
    public class BuildingsController : ControllerBase
    {
        private DatabaseContext DbContext { get; set; }

        public BuildingsController(DatabaseContext dbContext)
        {
            DbContext = dbContext;
            if (!DbContext.Buildings.Any())
            {
                DbContext.Buildings.Add(new BuildingModel() { Name = "Burj Khalifa", ObjectCode = "CodeWord1", Budget = 1.43m });
                DbContext.Buildings.Add(new BuildingModel() { Name = "Taipei 101", ObjectCode = "CodeWord2", Budget = 2.34m });
                DbContext.Buildings.Add(new BuildingModel() { Name = "Apple Park", ObjectCode = "CodeWord3", Budget = 3.21m });
                DbContext.Buildings.Add(new BuildingModel() { Name = "The Pentagon", ObjectCode = "CodeWord4", Budget = 4.12m });

                DbContext.SaveChanges();
            }
        }

        // Gets buildings list
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuildingModel>>> Get()
        {
            return await DbContext.Buildings.ToListAsync();
        }

        // GET api/building/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingModel>> Get(int id)
        {
            BuildingModel buildingModel = await DbContext.Buildings.FirstOrDefaultAsync(x => x.Id == id);
            if (buildingModel == null)
                return NotFound();
            return new ObjectResult(buildingModel);
        }

        // POST api/building
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

        // PUT api/building/
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

        // DELETE api/building/5
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