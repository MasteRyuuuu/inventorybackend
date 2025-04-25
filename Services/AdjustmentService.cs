using InventorySystem.Data;
using InventorySystem.Models;
using System.Collections.Generic;
using System.Linq;            

namespace InventorySystem.Services
{
    public class AdjustmentService
    {
        private readonly InventoryDbContext _context;

        public AdjustmentService(InventoryDbContext context)
        {
            _context = context;
        }

        public void AddAdjustment(Adjustment adj)
        {
            _context.Adjustments.Add(adj);
            _context.SaveChanges();
        }
    }
}
