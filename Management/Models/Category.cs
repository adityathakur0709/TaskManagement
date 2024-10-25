using System.ComponentModel.DataAnnotations;

namespace Management.Models
{
    public class Category
    {
        [Key]
        public String CategoryId { get; set; }
        [Required]  
        public String Name { get; set; }
    }
}
