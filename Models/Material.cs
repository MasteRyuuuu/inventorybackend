
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models

{
public class Material
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int Id { get; set; }   
    public string Name { get; set; }
    public string Unit { get; set; }
    
    [Column("per_package_qty")]
        public int PerPackageQty { get; set; }
    public int Threshold { get; set; }
}
}