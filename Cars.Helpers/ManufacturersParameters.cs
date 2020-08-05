using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Helpers
{
   public class ManufacturersParameters : QueryStringParameters
    {
        public ManufacturersParameters()
        {
            OrderBy = "name";
        }
        public string Name { get; set; }
    }
}
