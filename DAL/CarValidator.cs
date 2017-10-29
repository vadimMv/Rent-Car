using System.ComponentModel.DataAnnotations;
namespace DAL
{
    /// <summary>
    /// validation car model
    /// </summary>
    [MetadataType(typeof(CarValidator))]
    public partial class Car
    {

    }
    class CarValidator
    {
       
        public string CarModel { get; set; }
        [Required(ErrorMessage = " must be selected")]
        [RegularExpression("^[0-9]*$", ErrorMessage = " must be numeric")]
        public int CurrentDistance { get; set; }
        public byte[] Image { get; set; }
        [Required(ErrorMessage = " must be selected")]
        [RegularExpression("^[0-9]*$", ErrorMessage = " must be numeric")]
        public int CarNumber { get; set; }
        public string Branch { get; set; }
    }
}
