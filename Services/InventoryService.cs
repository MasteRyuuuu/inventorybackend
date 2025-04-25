using InventorySystem.Data;
using InventorySystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Services
{
    public class InventoryService
    {
        private readonly InventoryDbContext _context;

        public InventoryService(InventoryDbContext context)
        {
            _context = context;
        }

        public List<Inventory> GetInventory()
        {
            return _context.Inventory.ToList();
        }
    }
}
