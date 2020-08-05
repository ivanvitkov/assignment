using Cars.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Repository.Interfaces
{
    public interface IRepositoryWrapper
    {
        IManufacturerRepository Manufacturer { get; }
        ICarModelRepository CarModel { get; }
        void Save();
    }
}
