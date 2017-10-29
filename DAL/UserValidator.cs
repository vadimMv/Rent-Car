using System;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    /// <summary>
    /// validation user model
    /// </summary>
    [MetadataType(typeof(UserValidator))]
    public partial class User {

    }
   public class UserValidator
    {
        [Required(ErrorMessage = " must be selected")]
        //[MaxLength(9)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "ID must be numeric")]
        public int IdNumber { get; set; }
        [Required(ErrorMessage = " must be selected")]
        [RegularExpression("^([A-Za-z]+[,.]?[ ]?|[A-Za-z]+['-]?)+$", ErrorMessage = "Selected name not valid!!!")]
        [MaxLength(20, ErrorMessage = "Name can't be longer than 20 characters")]
        public string FullName { get; set; }
        [Required(ErrorMessage = " must be selected")]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = " must be selected")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = " must be selected")]
        public string Role { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public byte[] Image { get; set; }

    }
}
