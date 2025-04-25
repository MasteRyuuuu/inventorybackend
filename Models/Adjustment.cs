
using System.ComponentModel.DataAnnotations.Schema;
namespace InventorySystem.Models
{
public class Adjustment
{
    public int Id { get; set; }
    [Column("material_id")]
    public int MaterialId { get; set; }
    [Column("old_quantity")]
    public double OldQuantity { get; set; }
    [Column("new_quantity")]
    public double NewQuantity { get; set; }
    public string Reason { get; set; }
    public string Operator { get; set; }
    public string Remark { get; set; }
    [Column("adjusted_at")]
    public DateTime AdjustedAt { get; set; }
}
}