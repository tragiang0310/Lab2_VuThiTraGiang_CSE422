using System.ComponentModel.DataAnnotations;

namespace Lab2_VuThiTraGiang_CSE422.Models
{
    public class UserDto
    {
        [Required]
        public string FullName { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string PhoneNumber { get; set; } = "";

    }
}
