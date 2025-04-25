using InventorySystem.Data;
using InventorySystem.Models;
using System.Linq;

namespace InventorySystem.Services
{
    public class BarcodeService
    {
        private readonly InventoryDbContext _context;

        public BarcodeService(InventoryDbContext context)
        {
            _context = context;
        }

        public void AddBarcode(Barcode barcode)
        {
            _context.Barcodes.Add(barcode);
            _context.SaveChanges();
        }

        public Material? GetMaterialByBarcode(string code)
        {
            var barcode = _context.Barcodes.FirstOrDefault(b => b.Code == code);
            if (barcode == null) return null;

            return _context.Materials.FirstOrDefault(m => m.Id == barcode.MaterialId);
        }
    }
}
