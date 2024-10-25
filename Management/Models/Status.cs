using System.ComponentModel.DataAnnotations;

namespace Management.Models
{
    public class Status
    {
        [Key]
        public string StatusId {  get; set; }
        public string StatusName { get; set; } = string.Empty;

    }
}
