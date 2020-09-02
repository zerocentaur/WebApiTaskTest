using System.ComponentModel.DataAnnotations;

namespace WebApiTask.Models
{
    public class VersionModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Type { get; set; }
    }
}