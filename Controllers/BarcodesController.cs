using Microsoft.AspNetCore.Mvc;
using InventorySystem.Models;
using InventorySystem.Data;
using System.Linq;

namespace InventorySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarcodesController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public BarcodesController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet("{code}")]
public IActionResult GetByCode(string code)
{
    var result = (from b in _context.Barcodes
                  join m in _context.Materials on b.MaterialId equals m.Id
                  where b.Code == code
                  select new
                  {
                      barcodeId = b.Id,
                      barcode = b.Code,
                      materialId = m.Id,
                      materialName = m.Name,
                      unit = m.Unit,
                      perPackageQty = m.PerPackageQty,
                      threshold = m.Threshold
                  }).FirstOrDefault();

    if (result == null)
    {
        return NotFound(new { message = "未找到该条码" });
    }
     return Ok(result);
}


        [HttpPost]
        public IActionResult Add([FromBody] Barcode barcode)
        {
            if (barcode == null)
                return BadRequest("Invalid barcode data.");

            _context.Barcodes.Add(barcode);
            _context.SaveChanges();
            return Ok(barcode);
        }
    }
}
