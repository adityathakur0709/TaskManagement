using Management.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace Management.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly managementDbContext _managementDbContext;
        
        public HomeController(ILogger<HomeController> logger, managementDbContext managementDbContext)
        {
            _logger = logger;
           this._managementDbContext = managementDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User; //checks if the user is already logged in

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "User");
            return View();
            
        }
        [HttpPost]
        public async Task< IActionResult> Index(VMLogin modelLogin)
        {
         var user = _managementDbContext.Users.Where(u => u.userName == modelLogin.Username && u.password == modelLogin.Password).FirstOrDefault();
           
            if (user != null)
            {
                List<Claim> claims = new List<Claim>() {
                   new Claim(ClaimTypes.Name,user.userName),
                   new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)

                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme); //claims is list of claims

                    AuthenticationProperties Properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true
                        

                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), Properties);//properties,ClaimsIdentity is used.
                                                                         //HttpContext.Session.SetString("UserName", modelLogin.Username);//displaying name in dashboard.

                
                if (user.Role == "User")
                {
                    return RedirectToAction("Index", "User");
                }
                if (user.Role == "Admin")
                {

                    return RedirectToAction("Index", "User");
                }

            }
            ViewData["ValidateMessage"] = "User Not Found";
            return View();


        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Signup() { 

          return View();
        }
        [HttpPost]
        public IActionResult Signup(User obj) //when signup is hit data is stored here.
        {  
            obj.Created_Date = DateTime.Now;
            obj.Role = "User";
            if (obj!=null)
            {
               
                _managementDbContext.Users.Add(obj);
                _managementDbContext.SaveChanges();
                obj.Created_By = obj.Id;
                _managementDbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        //[HttpPost]
        //public IActionResult Signupp(User obj1) { 
        
        //    if(obj1!=null){
        //        _manahement.users.add(obj1);

        //    }return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
