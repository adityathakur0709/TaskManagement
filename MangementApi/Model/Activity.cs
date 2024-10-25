using System.ComponentModel.DataAnnotations;

namespace MangementApi.Model
{
    public class Activity
    {
        [Key]
       public int TaskId {  get; set; }
        [Required]
       public String TaskName {  get; set; }
        [Required]
       public  String Description {  get; set; }
       public  DateTime TaskDueDate {  get; set; }
        public String Priority {  get; set; }
      public String Category {  get; set; }
       public String Status { get; set; }
       public int CategoryId { get; set; }

       public DateTime AssignDate {  get; set; }
        public int AssignedTo { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Created_Date {  get; set; }
        public int Created_By { get; set; }
        public DateTime Updated_Date { get; set; }
        public int Updated_By { get; set; }
    }
}
