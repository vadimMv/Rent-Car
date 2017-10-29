using DAL;
using System.Collections.Generic;
using System.Linq;

namespace vadim_final_326960382_91338._16.Areas.Staff.Models
{   
    /// <summary>
    /// Search rents helper model 
    /// </summary>
    public class SearchForReturn : GenericRepository<Entities, Car>
    {
        /// <summary>
        /// Searching cars in lasted rents
        /// </summary>
        /// <param name="NumCar">car number</param>
        /// <returns> cars list</returns>
        public IEnumerable<Car> GetItems(string NumCar)
        {
            var result = SearchFor(c => c.FreeForRent == false);  // all busy cars

            if (NumCar != null)
            {
                var CID = 0;
                bool match = int.TryParse(NumCar, out CID);
                if (match)
                    result = SearchFor(c => c.CarNumber == CID && c.FreeForRent == false);  // special car
            }
            return result.OrderBy(x => x.id);
        }
    }
}