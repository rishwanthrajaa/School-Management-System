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
    /// The class which provides functionality to respond Faculty side request and with its appropriate views. Which
    /// enables to search, update their attandance and class wise attandance as well. A data logic is maintained to
    /// respond appropriate views with the database entites.
    /// </summary>
    public class FacultyController : Controller
    {
        public SchoolEntities db = new SchoolEntities();
        public static Faculty faculty = new Faculty();
        public ActionResult Login() // A Action Result method for faculty's login view and its HttpPost method for validation purpose. 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password) // HttpPost method for user login validation and return its respected view.
        {
            bool CheckFaculty = false;
            foreach (Faculty show in db.Faculties)
            {
                if (show.Username.Equals(username) && show.Password.Equals(password))
                {
                    faculty = show;
                    CheckFaculty = true;
                }
            }
            if (CheckFaculty == true)
            {
                return RedirectToAction("Home");
            }
            else
            {
                ViewBag.Check = "Invalid!!";
                return View();
            }

        }
        public ViewResult Home() // A method to display once the login is successfull and lists the actions to be performed by the user.
        {
            return View(faculty);
            
        }
        public ViewResult ViewDetails() // A view type method to respond the particular user (faculty) details. 
        {
            return View(faculty);
        }

        public ViewResult ChangePassword() //A HttpGet Method to change particular faculty's password in return its corresponding view. 
        {
            return View();

        }
        [HttpPost]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string reEnterPassword) // HttpPost action method to get the request from user and validate those passwords which returns it required view.
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMinimum8Chars = new Regex(@".{8,15}");

            ViewBag.Check = "";
            if (currentPassword.Equals(newPassword) || newPassword != reEnterPassword
            || currentPassword != faculty.Password)
            {
                ViewBag.Check = "Invalid";
                return View();

            }

            else
            {
                if (hasNumber.IsMatch(newPassword) && hasUpperChar.IsMatch(newPassword) &&
               hasMinimum8Chars.IsMatch(newPassword) && hasLowerChar.IsMatch(newPassword))
                {
                    faculty = db.Faculties.Find(faculty.Id);
                    if (faculty != null)
                    {
                        faculty.Password = newPassword;
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
        /*
        [NonAction]
        public int GetDetails()
        {
            int Count = 0;

            foreach (Faculty Show in facultyList)
            {
                if (facultyUser.Username.Equals(Show.Username) && facultyUser.Password.Equals(Show.Password))
                {
                    break;
                }
                else
                {
                    Count += 1;
                }
            }
            return Count;
        
        } */
    }
}