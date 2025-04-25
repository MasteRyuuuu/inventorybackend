using Microsoft.AspNetCore.Mvc;
using InventorySystem.Data;
using InventorySystem.Models;
using System;
using System.Linq;

namespace InventorySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public TransactionsController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpPost("in")]
        public IActionResult ScanIn([FromBody] Transaction transaction)
        {
            if (transaction == null) return BadRequest("Invalid data.");

            transaction.Operation = "in";
            transaction.CreatedAt = DateTime.Now;
            _context.Transactions.Add(transaction);

            var inventory = _context.Inventory.FirstOrDefault(i => i.MaterialId == transaction.MaterialId);
            if (inventory != null)
            {
                inventory.Quantity += transaction.Quantity;
                inventory.UpdatedAt = DateTime.Now;
            }
            else
            {
                _context.Inventory.Add(new Inventory
                {
                    MaterialId = transaction.MaterialId,
                    Quantity = transaction.Quantity,
                    UpdatedAt = DateTime.Now
                });
            }

            _context.SaveChanges();
            return Ok(new { message = "Scan-in successful." });
        }

        [HttpPost("out")]
        public IActionResult ScanOut([FromBody] Transaction transaction)
        {
            if (transaction == null) return BadRequest("Invalid data.");

            transaction.Operation = "out";
            transaction.CreatedAt = DateTime.Now;
            _context.Transactions.Add(transaction);

            var inventory = _context.Inventory.FirstOrDefault(i => i.MaterialId == transaction.MaterialId);
            if (inventory == null || inventory.Quantity < transaction.Quantity)
            {
                return BadRequest("Insufficient stock.");
            }

            inventory.Quantity -= transaction.Quantity;
            inventory.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return Ok(new { message = "Scan-out successful." });
        }

        [HttpPost("undo")]
public IActionResult Undo()
{
    var lastTransaction = _context.Transactions
        .OrderByDescending(t => t.CreatedAt)
        .FirstOrDefault();

    if (lastTransaction == null)
        return NotFound("No transactions to undo.");

    var inventory = _context.Inventory
        .FirstOrDefault(i => i.MaterialId == lastTransaction.MaterialId);

    if (inventory == null)
        return BadRequest("Inventory record not found.");

    if (lastTransaction.Operation == "in")
    {
        inventory.Quantity -= lastTransaction.Quantity;
    }
    else if (lastTransaction.Operation == "out")
    {
        inventory.Quantity += lastTransaction.Quantity;
    }

    inventory.UpdatedAt = DateTime.Now;

    _context.Transactions.Remove(lastTransaction);
    _context.SaveChanges();

    return Ok(new { message = "Last transaction undone." });
}

    }
}
