using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaboritCatalogVehicles.Models
{
    [Table("Models")]
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Brand")]
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

    }
}
