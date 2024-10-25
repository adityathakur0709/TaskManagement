namespace Management.Models
{
    public class Filters
    { 
        public Filters(string filterStr) {
            FilterStr = filterStr ?? "all-all-all-all"; //setting in constructor
            string[]filters= FilterStr.Split('-'); 
            CategoryId = filters[0];
            Due = filters[1];
            StatusId = filters[2];
            PriorityId = filters[3];

        }
        public string FilterStr { get;  }
        public string CategoryId {  get; }
        public string Due {  get; }
        public string StatusId {  get; }
        public string PriorityId {  get; }
        public bool HasCategory => CategoryId.ToLower() != "all";
        public bool HasDue=>Due.ToLower() != "all";
        public bool HasStatus => StatusId.ToLower() != "all";
        public bool HasPriority=>PriorityId.ToLower() != "all";
       public static Dictionary<string, string> DueFilter =>
            new Dictionary<string, string>
            {
                { "future","Future" },
                {"past","Past" },
                {"today","Today" }
            };
        public bool IsPast=>Due.ToLower() == "past";
        public bool IsFuture => Due.ToLower() == "future";
        public bool IsToday => Due.ToLower() == "today";

    }
}
