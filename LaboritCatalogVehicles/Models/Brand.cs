using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaboritCatalogVehicles.Models
{
    public class Brand
    {

        // public Brand()
        // {
        //     Vehicles = new List<Vehicle>();
        // }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        //  public virtual List<Vehicle> Vehicles { get; set; }

    }
}
