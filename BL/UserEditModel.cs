using System;
using System.ComponentModel.DataAnnotations;

namespace BL
{
    /// <summary>
    /// User editing helper class
    /// </summary>
    public class UserEditModel
    {
       // [RegularExpression("^[0-9]*$", ErrorMessage = "ID must be numeric")]
        public int IdNumber { get; set; }
       
      //  [RegularExpression("^([A-Za-z]+[,.]?[ ]?|[A-Za-z]+['-]?)+$", ErrorMessage = "Selected name not valid!!!")]
      //  [MaxLength(20, ErrorMessage = "Name can't be longer than 20 characters")]
        public string FullName { get; set; }
      
        public string  Sex { get; set; }
       
       // [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
       // [DataType(DataType.Password)]
        public string Password { get; set; }
      
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        
        public string Role { get; set; }

        public DateTime? BirthDate { get; set; }
        public byte[] Image { get; set; }
    }
}
