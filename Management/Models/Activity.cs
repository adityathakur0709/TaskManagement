using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Management.Models
{
    public class Activity
    {
        [Key]
       public int TaskId {  get; set; }
        [Required]
       public String TaskName {  get; set; }
       public String Description { get; set; }
        public DateTime TaskDueDate { get; set; }
        public string PriorityId {  get; set; }
       public string StatusId {  get; set; }
       public string CategoryId { get; set; }
        public DateTime AssignDate { get; set; }
       public int AssignedTo { get; set; }
        public DateTime EndDate {  get; set; }
        public DateTime? Created_Date { get; set; }
        public int ?Created_By { get; set; }
        public DateTime? Updated_Date { get; set; }
        public int? Updated_By { get; set; }
        [ValidateNever]
        public Category category { get; set; } = null!;
        [ValidateNever]
        public Status status { get; set; } = null!;
        [ValidateNever]
        public Priority priority { get; set; } = null!;
        public bool overdue => StatusId == "open" && TaskDueDate < DateTime.Today;
        
      
        
    }
   
}
