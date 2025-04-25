using InventorySystem.Data;
using InventorySystem.Models;
using System.Collections.Generic;
using System.Linq;            

namespace InventorySystem.Services
{
    public class MaterialService
    {
        private readonly InventoryDbContext _context;

        public MaterialService(InventoryDbContext context)
        {
            _context = context;
        }

        public List<Material> GetAllMaterials()
        {
            return _context.Materials.ToList();
        }
    }
}
