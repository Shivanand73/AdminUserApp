using BusinessAccessLayer.IServices;
using BusinessAccessLayer.Services;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Security.Claims;

namespace AdminUserApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUserServics _userServics;

        public StudentController(IUserServics userServics)
        {
            _userServics = userServics;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            ViewData["returnedUrl"] = ReturnUrl;
            return View();
        }


        [HttpPost("Student/Login")]
        public IActionResult verify(string username, string password, string ReturnUrl)
        {
            if (username == "MegaMinds" && password == "MegaMinds")
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                claims.Add(new Claim(ClaimTypes.Name, username));
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                //Claim
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
                HttpContext.SignInAsync(principal);
                return Redirect(ReturnUrl);
            }
            return BadRequest();
        }

        [Authorize]
        public IActionResult Admin()
        {
            Category();
            return View();
        }

        public JsonResult GetAll()
        {
            List<User> emp = new List<User>();
            try
            {
                 emp = _userServics.GetAll();
            }
            catch (Exception ex)
            {

                throw;
            }
            return Json(emp);
        }



        public void Category()
        {
            List<Category> category = new List<Category>();
            category=_userServics.GetCategoryAll();
            ViewBag.cat = category;
        }
        [HttpPost]
        public IActionResult InsertData(User user)
        {
            string result = string.Empty;
            try
            {
                result  = _userServics.InsertData(user);
            }
            catch (Exception ex) { 
            
            
            }
            return Json(result);
        }

        public IActionResult Delete(int id)
        {
            string result= string.Empty;
            try
            {
                result= _userServics.Delete(id);
            }
            catch (Exception ex) { 

            }
            return Json(result);
        }

        public IActionResult GetById(int id)
        {
            User result =new User();
            try
            {
                result = _userServics.GetById(id);
            }
            catch (Exception ex)
            {

            }
            return Json(result);
        }

    }
}
