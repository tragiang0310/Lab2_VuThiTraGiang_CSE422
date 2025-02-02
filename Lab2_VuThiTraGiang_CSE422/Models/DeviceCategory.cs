using System.ComponentModel.DataAnnotations;

namespace Lab2_VuThiTraGiang_CSE422.Models
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class DeviceCategory: BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Device> Devices { get; set; }

    }
}
