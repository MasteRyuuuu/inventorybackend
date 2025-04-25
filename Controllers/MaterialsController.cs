using Microsoft.AspNetCore.Mvc;
using InventorySystem.Models;
using InventorySystem.Data;

namespace InventorySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public MaterialsController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Materials.ToList());

        [HttpPost]
        public IActionResult Add([FromBody] Material material)
        {
            _context.Materials.Add(material);
            _context.SaveChanges();
            return Ok(material);
        }
    }
}
