using Cars.Helpers;
using Cars.Models;
using Cars.Repository.Interfaces;
using CarsContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Repository.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private Context _context;
        private IManufacturerRepository _manufacturer;
        private ICarModelRepository _carModel;

        public IManufacturerRepository Manufacturer
        {
            get
            {
                if (_manufacturer == null)
                {
                    _manufacturer = new ManufacturerRepository(_context);
                }
                return _manufacturer;
            }
        }
        public ICarModelRepository CarModel
        {
            get
            {
                if(_carModel == null)
                {
                    _carModel = new CarModelRepository(_context);
                }
                return _carModel;
            }
        }
        public RepositoryWrapper(Context context)
        {
            _context = context;
           

        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
