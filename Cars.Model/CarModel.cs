
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Cars.Models
{
    [Table("model")]
    public class CarModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Model name is required!")]
        public string Name { get; set; }
        public string Color { get; set; }
        [Column("make_year")]
        public int Year { get; set; }
        
        [ForeignKey(nameof(Manufacturer))]
        [Column("Manufacturer_id")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
