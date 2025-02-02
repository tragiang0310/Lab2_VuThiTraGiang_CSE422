using System.ComponentModel.DataAnnotations;

namespace Lab2_VuThiTraGiang_CSE422.Models
{
    public class DeviceCategoryDto
    {
        [Required]
        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

    }
}
