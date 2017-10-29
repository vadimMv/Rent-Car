using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// validation car type model
    /// </summary>
    [MetadataType(typeof(CarTypeValidator))]
    public partial class CarType
    {

    }
    class CarTypeValidator
    {
        [Required(ErrorMessage = " must be selected")]
        [RegularExpression("^([A-Za-z]+[,.]?[ ]?|[A-Za-z]+['-]?)+$", ErrorMessage = "Selected name not valid!!!")]
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = " must be selected")]
        [RegularExpression("^(^[a-zA-Z0-9]+[,.]?[ ]?|^[0-9]+['-]?)+$", ErrorMessage = "Selected name not valid!!!")]
        public string Model { get; set; }

        [Required(ErrorMessage = " must be selected")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Cost must be number")]
        public decimal DailyCost { get; set; }
        [Required(ErrorMessage = " must be selected")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Cost must be number")]
        public decimal DelayCost { get; set; }

        [Required(ErrorMessage = " must be selected")]
        public System.DateTime YearManufacture { get; set; }

    }
}
