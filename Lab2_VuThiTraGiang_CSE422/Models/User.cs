using System.ComponentModel.DataAnnotations;

namespace Lab2_VuThiTraGiang_CSE422.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
