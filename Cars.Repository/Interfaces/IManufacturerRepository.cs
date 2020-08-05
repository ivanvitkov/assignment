using Cars.Helpers;
using Cars.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Repository.Repository
{
   public interface IManufacturerRepository : IRepositoryBase<Manufacturer>
    {
        PagedList<Manufacturer> GetManufacturers(ManufacturersParameters parameters);
        Manufacturer GetById(int id);
        Manufacturer GetModels(int id);
        void CreateManufacturer(Manufacturer manufacturer);
        void UpdateManufacturer(Manufacturer manufacturer);
        void DeleteManufacturer(Manufacturer manufacturer);
       
    }
}
