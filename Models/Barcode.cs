
using System.ComponentModel.DataAnnotations.Schema;
namespace InventorySystem.Models
{
    [Table("barcodes")]
    public class Barcode
{
    public int Id { get; set; }
    [Column("material_id")]
    public int MaterialId { get; set; }
    [Column("barcode")]
    public string Code { get; set; }
    public string? Note { get; set; }
}
}