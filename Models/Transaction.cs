
using System.ComponentModel.DataAnnotations.Schema;
namespace InventorySystem.Models

{
public class Transaction
{
    public int Id { get; set; }
     
     [Column("material_id")]
      public int MaterialId { get; set; }
    public string Operation { get; set; } // 'in' or 'out'
    public double Quantity { get; set; }
    public string Barcode { get; set; }
    public string Source { get; set; }
    public string Operator { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
}