using System.ComponentModel.DataAnnotations;

namespace Lab2_VuThiTraGiang_CSE422.Models
{
    public class Device : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime DateOfEntry { get; set; }
        public int CategoryID { get; set; }

        public virtual DeviceCategory Category { get; set; }
    }
}
