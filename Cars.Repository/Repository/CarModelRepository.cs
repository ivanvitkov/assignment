using Cars.Helpers;
using Cars.Models;
using CarsContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cars.Repository.Repository
{
    public class CarModelRepository : RepositoryBase<CarModel>, ICarModelRepository
    {
        public CarModelRepository(Context context) : base(context)
        {
            
        }
        public IEnumerable<CarModel> GetAllModels()
        {
            return FindAll().ToList();
        }
        public IEnumerable<CarModel> GetCarModels(int id)
        {
            return FindByCondition(model => model.ManufacturerId.Equals(id)).ToList();
        }
        public CarModel GetById(int id)
        {
            return FindByCondition(model => model.Id.Equals(id)).FirstOrDefault();
        }
        public void CreateCarModel(CarModel model)
        {
            Create(model);
        }
        public void UpdateCarModel(CarModel model)
        {
            Update(model);
        }
        public void DeleteCarModel(CarModel model)
        {
            Delete(model);
        }


    }
}
