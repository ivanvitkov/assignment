
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cars.Models
{
    [Table("manufacturer")]
    public class Manufacturer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Manufacturer name is required!")]
        public string Name{ get; set; }

        public ICollection<CarModel> CarModels { get; set; }
    }
}
