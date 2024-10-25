using System.ComponentModel.DataAnnotations;

namespace Management.Models
{
    public class Priority
    {
        [Key]
        public String PriorityId { get; set; }
        public string Name { get; set; }
    }
}
