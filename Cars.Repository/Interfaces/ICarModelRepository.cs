using Cars.Helpers;
using Cars.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Repository.Repository
{
    public interface ICarModelRepository : IRepositoryBase<CarModel>
    {
        IEnumerable<CarModel> GetCarModels(int id);
        IEnumerable<CarModel> GetAllModels();
        CarModel GetById(int id);

        void CreateCarModel(CarModel model);
        void UpdateCarModel(CarModel model);
        void DeleteCarModel(CarModel model);

    }
}
