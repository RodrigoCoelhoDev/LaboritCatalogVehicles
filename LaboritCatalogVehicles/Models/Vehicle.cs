using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaboritCatalogVehicles.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public decimal Value { get; set; }
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        [ForeignKey("Model")]
        public int ModelId { get; set; }
        [Required]
        public int YearModel { get; set; }
        [Required]
        public string Fuel { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Model Model { get; set; }

        
    }
}
