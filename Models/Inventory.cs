
using System.ComponentModel.DataAnnotations.Schema;
namespace InventorySystem.Models
{
public class Inventory
{
    public int Id { get; set; }

     [Column("material_id")]
        public int MaterialId { get; set; }
    public double Quantity { get; set; }
[Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
}