using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrudUsingCore.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        [StringLength(50)]
        [DisplayName("Full Name")]
        public string? FullName { get; set; }

        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress(ErrorMessage ="Please Enter a Valid Email")]
        [DisplayName("Email")]
        public string? Email { get; set; }
    }
}

