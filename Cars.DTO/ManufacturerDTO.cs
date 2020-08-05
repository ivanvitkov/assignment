﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Cars.DTO
{
    public class ManufacturerDTO
    {
     
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<CarModelDTO> CarModels { get; set; }
    }
}
