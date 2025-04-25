using Microsoft.AspNetCore.Mvc;
using InventorySystem.Data;
using InventorySystem.Models;
using System.Linq;

namespace InventorySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public InventoryController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _context.Inventory.ToList();
            return Ok(list);
        }

        [HttpGet("with-materials")]
        public IActionResult GetInventoryWithMaterial()
        {
            var data = from inv in _context.Inventory
                       join mat in _context.Materials
                       on inv.MaterialId equals mat.Id
                       select new
                       {
                           materialName = mat.Name,
                           unit = mat.Unit,
                           perPackageQty = mat.PerPackageQty,
                           quantity = inv.Quantity,
                           updatedAt = inv.UpdatedAt
                       };

            return Ok(data.ToList());
        }
    }
}
