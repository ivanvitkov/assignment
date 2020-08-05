using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cars.DTO
{
    public class CreateManufacturerDTO
    {

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
    }
}
