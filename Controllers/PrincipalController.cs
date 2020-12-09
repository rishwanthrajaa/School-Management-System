using System.Text.RegularExpressions;
using System.Web.Mvc;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    /// <summary>
    /// A Complete management system for schools to maintain its office, principal, faculties and students records 
    /// with enhanced feature to scale up its data and tend to provide access for individuals to showcase their review,
    /// attandance, leaves and marks in an agile availability model. Entity domain classes are used for the back end
    /// data management and manipulation. 
    /// 
    /// A class with specific action methods to respond to principal side login, password change, review faculties
    /// attandance and leave approval/denial. 
    /// </summary>
    public class PrincipalController : Controller
    {
        public SchoolEntities db = new SchoolEntities();
        public static Principal principal = new Principal();
        public ActionResult Login() // A Action Result method for principals's login view and its HttpPost method for validation. 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password) 
        {
            ViewBag.Check = "";
            bool CheckPrincipal = false;
            foreach (Principal show in db.Principals)
            {
                if (show.Username.Equals(username) && show.Password.Equals(password))
                {
                    principal = show;
                    CheckPrincipal = true;
                }
            }
            if (CheckPrincipal == true)
            {
                return RedirectToAction("Home");
            }
            else
            {
                ViewBag.Check = "Invalid!!";
                return View();
            }
        }
        public ViewResult Home() // A Home method of ViewResult type for Principal's Home page.  
        {
            ViewBag.Name = principal.Name;
            return View();
        }

        public ViewResult ViewDetails() // A View method to list principal data.
        {
            return View(principal);
        }

        public ViewResult ChangePassword() // A HttpGet Method to change principal password in return its corresponding view. 
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string reEnterPassword) // HttpPost action method to get the request from user and validate those passwords which returns it required views.
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMinimum8Chars = new Regex(@".{8,15}");
            ViewBag.Check = "";
            if (currentPassword.Equals(newPassword) || newPassword != reEnterPassword
            || currentPassword != principal.Password)
            {
                ViewBag.Check = "Invalid";
                return View();

            }
            else
            {
                if (hasNumber.IsMatch(newPassword) && hasUpperChar.IsMatch(newPassword) &&
                hasMinimum8Chars.IsMatch(newPassword) && hasLowerChar.IsMatch(newPassword))
                {
                    principal = db.Principals.Find(principal.Id);
                    if (principal != null)
                    {
                        principal.Password = newPassword;
                        db.SaveChanges();
                        return View("SuccessfullPassword");
                    }
                    else
                    {
                        return View("DatabaseNotUpdated");
                    }
                }
                else
                {
                    ViewBag.Check = "Password should contain minimum 8 characters, Uppercase, Lowercase and Digit !!";
                    return View();
                }

            }

        }
        public ViewResult ListFaculty() // A view type method to return all list of faculty to its corresponding view.
        {
            return View(db.Faculties);
        }
    }
}