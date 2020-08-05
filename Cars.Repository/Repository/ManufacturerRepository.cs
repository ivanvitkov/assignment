using Cars.Models;
using CarsContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cars.Helpers;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace Cars.Repository.Repository
{
   
    public class ManufacturerRepository : RepositoryBase<Manufacturer>,IManufacturerRepository
    {
        public ManufacturerRepository(Context context) :base(context)
        {
           
        }
        public PagedList<Manufacturer> GetManufacturers(ManufacturersParameters parameters)
        {
            var manufacturers = FindAll();

            ApplySort(ref manufacturers, parameters.OrderBy);
            
            return PagedList<Manufacturer>.ToPagedList(FindAll().OrderBy(x => x.Name),
                parameters.PageNumber, parameters.PageSize);
        }
        public Manufacturer GetById(int id)
        {
            return FindByCondition(m => m.Id.Equals(id)).FirstOrDefault();
        }
        public Manufacturer GetModels(int id)
        {
            return FindByCondition(c => c.Id.Equals(id)).Include(x => x.CarModels).FirstOrDefault();
        }
        public void CreateManufacturer(Manufacturer manufacturer)
        {
            Create(manufacturer);
        }
        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            Update(manufacturer);
        }
        public void DeleteManufacturer(Manufacturer manufacturer)
        {
            Delete(manufacturer);
        }
        private void ApplySort(ref IQueryable<Manufacturer> manufacturers, string orderByQueryString)
        {
            if (!manufacturers.Any())
                return;

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                manufacturers = manufacturers.OrderBy(x => x.Name);
                return;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Manufacturer).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                manufacturers = manufacturers.OrderBy(x=>x.Name);
                return;
            }

            manufacturers = manufacturers.OrderBy(orderQuery);
        }

    }
}
