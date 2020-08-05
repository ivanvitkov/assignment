
using Microsoft.EntityFrameworkCore;

using Cars.Models;

namespace CarsContext
{
    public class Context :DbContext
    {
        public Context(DbContextOptions options) :base(options)
        {
        }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
    }
}
