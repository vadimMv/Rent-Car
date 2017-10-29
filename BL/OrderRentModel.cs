using System.Collections.Generic;
using DAL;
namespace BL
{
    /// <summary>
    /// Helper class for rent logic realization
    /// </summary>
    public  class OrderRentModel
    {
        public User user { get; set; }

        public List<Car> cars { get; set; }

        public CarsRent order { get; set; }
    }
}
