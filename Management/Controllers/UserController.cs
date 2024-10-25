using Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Mvc;
using PagedList;
using System.Linq;
using Microsoft.DotNet.MSIdentity.Shared;
using OfficeOpenXml;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Components.QuickGrid;


namespace Management.Controllers
{
    [Authorize] //only enter home if i am logged in 
    public class UserController : Controller

    {
      
        private readonly managementDbContext _managementDbContext;
        public UserController(managementDbContext managementDbContext)
        {
            _managementDbContext = managementDbContext;
        }

        protected int? GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return null;
        }
        protected string GetRole()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.Role);
            if (userIdClaim != null )
            {
                return userIdClaim.Value;
            }
            return null;
        }
        // GET: UserController
        protected string GetName()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.Name);
            if (userIdClaim != null)
            {
                return userIdClaim.ToString();
            }
            return string.Empty;
        }


        public IActionResult Index(string searchTerm,String id,int pg=1)
        {
            string userName = User.Identity.Name; // Get the username of the user 
            ViewBag.UserName = userName;
            var userId=GetUserId(); //Get the userId
            var role = GetRole(); //Get the role
            var filters = new Filters(id); //search in filters

            ViewBag.Filters = filters;  
            ViewBag.Categories=_managementDbContext.Categories.ToList();
            ViewBag.Statuses = _managementDbContext.Statuses.ToList();
            ViewBag.DueFilters = Filters.DueFilter;
            ViewBag.Priorities=_managementDbContext.Priorities.ToList();
            IQueryable<Activity> query;
            if (role == "Admin")
            {
                query = _managementDbContext.Activities
        .Include(t => t.category)
        .Include(t => t.status)
        .Include(t => t.priority)
        .AsQueryable();

            }
            else
            {
                query = _managementDbContext.Activities
        .Include(t => t.category)
        .Include(t => t.status)
        .Include(t => t.priority)
        .Where(t => t.Created_By == userId);
            }
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(t => t.TaskName.Contains(searchTerm));
                // You can expand this to include additional fields or search criteria
            }
            if (query != null)
            {
                if (filters.HasCategory)
                {
                    query = query.Where(t => t.CategoryId == filters.CategoryId);


                }
                if (filters.HasStatus)
                {
                    query = query.Where(t => t.StatusId == filters.StatusId);


                }
                if (filters.HasPriority)
                {
                    query = query.Where(t => t.PriorityId == filters.PriorityId);

                }
                if (filters.HasDue)
                {
                    var today = DateTime.Today;
                    if (filters.IsPast)
                    {
                        query = query.Where(t => t.TaskDueDate < today);

                    }
                    else if (filters.IsFuture)
                    {
                        query = query.Where(t => t.TaskDueDate > today);

                    }
                    else if (filters.IsPast)
                    {
                        query = query.Where(t => t.TaskDueDate == today);

                    }
                }
               
            }
            //var searchQuery = _managementDbContext.Activities
            //    .Include(b => b.PriorityId).AsQueryable();
            //if (!string.IsNullOrEmpty(searchQuery))
            //{
            //    query=query.Where(b=>
            //    b.)
            //}
            

            /*List<Activity> obj = _managementDbContext.Activities.ToList();*/
            //var tasks = query.OrderBy(t => t.TaskDueDate).ToList();//order by tasdkDue date
            /*  List<Activity> obj=[];
              if (role == "Admin")
              {
                   obj = _managementDbContext.Activities.ToList();

              }
              else if(role=="User"){
                   obj = _managementDbContext.Activities
                 .Where(a => a.Created_By == userId)
              .ToList();
              }
              else
              {
                  RedirectToAction("Index", "Home");
              }*/

            List<Activity> obj =query.ToList();
            const int pageSize = 7;  //-Pagination
            if (pg < 1)
                pg = 1;
            int recsCount= obj.Count();
            var pager=new Pager(recsCount,pg, pageSize);
            int recSkip=(pg-1)* pageSize;
            var data= obj.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager=pager;
            return View(data);
            //return View(obj);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories=_managementDbContext.Categories.ToList();
            ViewBag.Statuses = _managementDbContext.Statuses.ToList();
            ViewBag.Priorities = _managementDbContext.Priorities.ToList();
            var task = new Activity { StatusId = "open" };
            return View(task);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Activity obj)
        {  obj.Created_Date = DateTime.Now;
            var userId = GetUserId();
            obj.AssignDate = DateTime.Now;
            if (ModelState.IsValid) //data validation
            {
                obj.Created_By = userId;
                obj.EndDate = obj.TaskDueDate;
                _managementDbContext.Activities.Add(obj);
                _managementDbContext.SaveChanges();
                TempData["AlertMessage"] = "Task Created";
                 return RedirectToAction("Index");


            }
            else
            {
                ViewBag.Categories = _managementDbContext.Categories.ToList(); //reloads the viewbag
                ViewBag.Statues = _managementDbContext.Statuses.ToList();
                ViewBag.Priorities = _managementDbContext.Priorities.ToList();
                return View(obj);
            }
            
        }
        [HttpPost]
        public IActionResult Filter(int userId,string[] filter)
        {

            string id=string.Join('-', filter);
            return RedirectToAction("Index", new { userid =userId, ID = id });
        }
        [HttpPost]
        public IActionResult Complete(int id)
        {
            Activity? activity = _managementDbContext.Activities.Find(id);
            if (activity != null)
            {
                activity.StatusId = "closed";
                activity.EndDate= DateTime.Now;
                _managementDbContext.SaveChanges();
                TempData["AlertMessage"] = "Task Completed";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Priority(string id)
        {
            return View();
        }

        public async Task< IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.Categories = _managementDbContext.Categories.ToList();
            ViewBag.Statuses = _managementDbContext.Statuses.ToList();
            ViewBag.Priorities = _managementDbContext.Priorities.ToList();
            if (id == null)
            {
                return NotFound();
            }
            Activity? activity = _managementDbContext.Activities.Find(id);
            return View(activity);
        }
        [HttpPost]
        public IActionResult Edit(Activity obj)
        {
            var id = GetUserId();
            if (ModelState.IsValid)
            {
                obj.Created_By = id;
                _managementDbContext.Update(obj);
                
                _managementDbContext.SaveChanges();
                
                TempData["AlertMessage"] = "Task updated";
                return RedirectToAction("Index","User");
            }
            return View();

            
        }
        public IActionResult Details(int? id) { 
            if (id == null)
            {
                return NotFound();
            }
            Activity? activity = _managementDbContext.Activities
                .Include(b=>b.category)
                .Include(b=>b.priority)
                .Include(b=>b.status)
                 .FirstOrDefault(m=>m.TaskId==id);
          return View(activity);
        }

        // GET: UserController/Delete/5
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Activity? activity = _managementDbContext.Activities.Find(id);

        //    return View(activity);
        //}

        // POST: UserController/Delete/5

        [Authorize(Roles ="Admin")]
        public IActionResult Admin()
        {
            List<User> obj = _managementDbContext.Users.ToList();

            return View(obj);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Activity? activity = _managementDbContext.Activities.Find(id); 
            if (activity == null)
            {
                return NotFound();
            }
            _managementDbContext?.Activities.Remove(activity);
            _managementDbContext.SaveChanges();
            return RedirectToAction("Index","User");
        }
        public IActionResult Calendar()
        {
            var userid = GetUserId();
            var activities = _managementDbContext.Activities.Where(x=>x.Created_By==userid).ToList();
            return View(activities);
        }
        public IActionResult AdminCalendar()
        {
          
            var activities = _managementDbContext.Activities.ToList();
            return View(activities);
        }
        //public IActionResult ExportToExcel()
        //{
           
        //}
        public async Task<IActionResult> ExportToExcel()
        {
            var userId = GetUserId();
            
            IQueryable<Activity> ActivityQuery = _managementDbContext.Activities
                .OrderByDescending(u => u.Created_Date);

            var activ = await ActivityQuery.ToListAsync();

            byte[] fileContents = GenerateAsExcel(activ);

            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Task.xlsx");
        }

        private byte[] GenerateAsExcel(List<Activity> activities)

        {

            using (var package = new ExcelPackage())

            {


                var worksheet = package.Workbook.Worksheets.Add("Task");

                var userid = GetUserId();
                var UserName = GetName();

                var user = _managementDbContext.Users.FirstOrDefault(u => u.Id == userid);


                // Add headers

                worksheet.Cells[1, 1].Value = "Full Name";

                worksheet.Cells[2, 1].Value = "Email";

                worksheet.Cells[5, 1].Value = "Task Name";

                worksheet.Cells[5, 2].Value = "Description";

                worksheet.Cells[5, 3].Value = "Assign Date";

                worksheet.Cells[5, 4].Value = "Due Date";
                worksheet.Cells[5, 5].Value = "Created By";

                string fullName = user.userName;

                string email = user.email;

                worksheet.Cells[1, 2].Value = fullName;

                worksheet.Cells[2, 2].Value = email;

                // Add data

                int row = 6;

                foreach (var act in activities)
                    {


                        worksheet.Cells[row, 1].Value =act.TaskName;

                        worksheet.Cells[row, 2].Value = act.Description;

                        worksheet.Cells[row, 3].Value = act.AssignDate.ToShortDateString();

                        worksheet.Cells[row, 4].Value = act.TaskDueDate.ToShortDateString();

                    var assign = _managementDbContext.Users.FirstOrDefault(x => x.Id == act.Created_By);
                    if (assign != null)
                    {
                        worksheet.Cells[row, 5].Value = assign.userName;
                    }
                    else
                    {
                        worksheet.Cells[row, 5].Value = "No User Found";
                    }


                    row++;
                    }

                return package.GetAsByteArray();

            }

        }

    }
}
