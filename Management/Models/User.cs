using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Management.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage ="Required field")]
        [DisplayName("UserName")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Required field")]
        [DisplayName("Password")]
        [DataType(DataType.Password)] //-inform of bullets
        public string password { get; set; }
        [DisplayName("Email")]
        public string email { get; set; }
        [DisplayName("Created_Date")]
        public DateTime Created_Date { get; set; }
        [DisplayName("Created_By")]
        public int Created_By { get; set; }
        [DisplayName("Updated_Date")]
        public DateTime Updated_Date { get; set; }
        [DisplayName("Updated_By")]
        public int Updated_By { get; set; }
        [DisplayName("Active")]
        public bool active { get; set; }
        public string Role {  get; set; }
       


    }

}
    

