using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Helpers
{
    public class CarModelsParameters : QueryStringParameters
    {
        public CarModelsParameters()
        {
            OrderBy = "Name";
        }
    }
}
