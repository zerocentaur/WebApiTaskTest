using System.ComponentModel.DataAnnotations;

namespace WebApiTask.Models
{
    public class BuildingModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string ObjectCode { get; set; }

        [Required]
        public decimal Budget { get; set; }
    }
}