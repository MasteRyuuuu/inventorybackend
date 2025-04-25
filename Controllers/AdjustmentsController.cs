using Microsoft.AspNetCore.Mvc;
using InventorySystem.Models;
using InventorySystem.Data;

namespace InventorySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdjustmentsController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public AdjustmentsController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Adjustments.ToList());

        [HttpPost]
public IActionResult Add([FromBody] Adjustment adjustment)
{
    if (adjustment == null)
    {
        return BadRequest("Invalid adjustment data.");
    }

    // 验证 materialId 是否存在
    var materialExists = _context.Materials.Any(m => m.Id == adjustment.MaterialId);
    if (!materialExists)
    {
        return BadRequest($"MaterialId {adjustment.MaterialId} 不存在，请检查后再试。");
    }

    // 更新或创建库存记录
    var inventory = _context.Inventory.FirstOrDefault(i => i.MaterialId == adjustment.MaterialId);
    if (inventory != null)
    {
        inventory.Quantity = adjustment.NewQuantity;
        inventory.UpdatedAt = DateTime.Now;
    }
    else
    {
        _context.Inventory.Add(new Inventory
        {
            MaterialId = adjustment.MaterialId,
            Quantity = adjustment.NewQuantity,
            UpdatedAt = DateTime.Now
        });
    }

    // 添加 adjustment 记录
    adjustment.AdjustedAt = DateTime.Now;
    _context.Adjustments.Add(adjustment);
    _context.SaveChanges();

    return Ok(new { message = "库存调整成功并已同步更新库存。", adjustment });
}

    }
}
