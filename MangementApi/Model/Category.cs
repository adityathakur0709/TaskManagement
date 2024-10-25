using System.ComponentModel.DataAnnotations;

namespace MangementApi.Model
{
    public class Category
    {
        [Key]
       public int CategoryId {  get; set; }
        [Required]
        public String CategoryName { get; set; }

        public DateTime Created_Date {  get; set; }
        public int Created_By { get; set; }
        public DateTime Updated_Date { get; set; }
        public int Updated_By { get; set; }
    }
}
