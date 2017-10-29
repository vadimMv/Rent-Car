using PagedList;
using DAL;
namespace BL
{
    /// <summary>
    /// Class search logic model consist search parameters properties
    /// </summary>
    public class SearchCarsModel
    {
        public int? Page { get; set; }   // prop page
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public decimal? DailyCost { get; set; }
        public decimal? DelayCost { get; set; }
        public System.DateTime? YearManufacture { get; set; }
        public IPagedList<CarType> SearchResults { get; set; }  // paged list 
        public string Geer { get; set; }

        public string FreeText { get; set; }
    }
}
