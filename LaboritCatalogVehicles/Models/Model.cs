using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaboritCatalogVehicles.Models
{
    public class Model
    {

        //  public Model()
        //  {
        //      Vehicles = new List<Vehicle>();
        //  }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Brand")]
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        // public virtual List<Vehicle> Vehicles { get; set; }

    }
}
