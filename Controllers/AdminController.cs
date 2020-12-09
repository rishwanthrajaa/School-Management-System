using System;
using System.Collections.Generic;
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
    /// This class provides the data logic for admin side access to maintain the school database, updatation and key 
    /// code to alter principal, faculty and students data. Specified with individual login for the same. 
    /// </summary>
    public class AdminController : Controller 
    {
        public SchoolEntities db = new SchoolEntities();
        public static Admin admin = new Admin();
        public static Principal principal = new Principal();
        public static Faculty faculty = new Faculty();
        public ViewResult Login()// A view method for Admin's login view and its HttpPost method for validation. 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password) // HttpPost method for login validation and return its respected view.
        {
            ViewBag.Check = "";
            bool CheckAdminUsername = false, CheckAdminPassword = false;
            foreach (Admin show in db.Admins)
            {
                if (show.Username.Equals(username))
                {
                    CheckAdminUsername = true;
                }
                else
                {
                    ModelState.AddModelError("Username", "Invalid Username");
                    ViewBag.Check = "Invalid Username";
                }
                if (show.Password.Equals(password))
                {
                    CheckAdminPassword = true;
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid Password");
                    ViewBag.Check = "Invalid Password";
                }
                if(show.Username.Equals(username) && show.Password.Equals(password))
                {
                    admin = show;
                }
            }
            if (CheckAdminUsername == true && CheckAdminPassword==true)
            {
                return RedirectToAction("Home");
            }
            else
            {
                ViewBag.Check = "Invalid";
                return View();
            }
            
        }
        public ViewResult Home() // A method for admin's home page to list the actions to be performed by the admin.
        {
            foreach (Admin show in db.Admins)
            {
                if (show.Designation.Equals("Admin"))
                {
                    admin = show;
                }
            }

            ViewBag.Name = admin.Name;
            return View();
        }
        public ViewResult ViewDetails() // A view type method to respond for admin's user details request.
        {
            return View(admin);
        }
        public ViewResult ChangePassword() //A HttpGet Method to change admin's password in return its corresponding view. 
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string reEnterPassword)  // HttpPost action method to get the request from user and validate those passwords which returns it required view.
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMinimum8Chars = new Regex(@".{8,15}");

            ViewBag.Check = "";
            if (currentPassword.Equals(newPassword) || newPassword != reEnterPassword
            || currentPassword != admin.Password)
            {
                ViewBag.Check = "Invalid";
                return View();

            }

            else
            {
                if (hasNumber.IsMatch(newPassword) && hasUpperChar.IsMatch(newPassword) &&
                hasMinimum8Chars.IsMatch(newPassword) && hasLowerChar.IsMatch(newPassword))
                {
                    admin = db.Admins.Find(admin.Id);
                    if (admin != null)
                    {
                        admin.Password = newPassword;
                        db.SaveChanges();
                        return View("SuccessfullPassword");
                    }
                    else
                    {
                        ViewBag.Check = "Sorry!! Database is not up to date";
                        return View();
                    }
                   
                }
                else
                {
                    ViewBag.Check = "Password should contain minimum 8 characters, Uppercase, Lowercase and Digit !!";
                    return View();
                }

            }

        }
        public ViewResult ManagePrincipal() // A method to return Manage principal page view to manage principal data.
        {
            return View();
        }
        public ActionResult ViewPrincipalDetails() // A method to return all principal details to its corresponding views.
        {

            foreach (Principal search in db.Principals)
            {
                if (search.Designation.Equals("Principal"))
                {
                    principal = search;
                }
            }
            if (principal != null)
            {
                return View(principal);
            }
            else
            {
                return View("DatabaseNotUpdated");
            }
        }
        public ViewResult ChangePrincipalPassword() // A HttpGet view method to change the principal password via admin which get the input password and perform HttpPost method for validation.
        {
            return View();

        }
        [HttpPost]
        public ActionResult ChangePrincipalPassword(string currentPassword, string newPassword, string reEnterPassword) // HttpPost action method to get the request from user and validate those passwords which returns it required views.
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMinimum8Chars = new Regex(@".{8,15}");

            foreach (Principal show in db.Principals)
            {
                if (show.Designation.Equals("Principal"))
                {
                    principal = show;
                }
            }
            if (principal != null)
            {
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
                            return View("SuccessfullPrincipalPassword");

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
            else
            {
                return View("DatabaseNotUpdated");
            }
        }

        public ViewResult ManageFaculty() // A method to return a view with list of faculty data actions for admin.
        {
            return View();
        }
        public ViewResult ListFaculties()
        {
            return View(db.Faculties);
        }
        public ViewResult AddFaculty() // A View type method to add faculty via admin which returns its corresponding view.
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddFaculty(string firstName,string middleName, string lastName, string dob, string gender, string address, string contact)
        {
            ViewBag.firstName = ""; ViewBag.middleName = ""; ViewBag.lastName = ""; ViewBag.dob = "";ViewBag.contact = "";
            bool checkName = false; bool checkAddress = false; bool checkGender = false; bool checkContact = false;
            bool checkDob = false;
          // var namePattern = "^(([a-zA-Z])\\2?(?!\\2))+$";
          //var phoneNumberPattern = "(0/91)?[6-9][0-9]{9}"
          var namePattern = new Regex(@"^(([a-zA-Z])\2?(?!\2))+$");
            var numberPattern = new Regex(@"^[6789]\d{9}$");
            var addressPattern = new Regex(@"^[a-zA-z0-9]+[,@\./]*$");
            if (middleName == "")
            {
                if (lastName == "")
                {
                    if (namePattern.IsMatch(firstName))
                    {
                        checkName = true;
                    }
                    else
                    {
                        if (!namePattern.IsMatch(firstName))
                        {
                            ViewBag.firstName = "Invalid!";
                        }
                    }
                }
                else
                {
                    if (namePattern.IsMatch(firstName) && namePattern.IsMatch(lastName))
                    {
                        if (firstName.ToLower() != lastName.ToLower())
                        {
                            checkName = true;
                        }
                        else
                        {
                            ViewBag.firstName = "Oops!! First and last names are similar";
                        }
                    }
                    else
                    {
                        if (!namePattern.IsMatch(firstName))
                        {
                            ViewBag.firstName = "Invalid!";
                        }
                        if (!namePattern.IsMatch(lastName))
                        {
                            ViewBag.lastName = "Invalid!!!";
                        }
                    }

                }
            }
            else
            {
                if (lastName !="")
                {
                    if (namePattern.IsMatch(firstName) && namePattern.IsMatch(middleName) && namePattern.IsMatch(lastName))
                    {
                        if (firstName.ToLower() != middleName.ToLower() && firstName.ToLower() != lastName.ToLower() && middleName.ToLower() != lastName.ToLower())
                        {
                            checkName = true;
                        }
                        else
                        {
                            ViewBag.firstName = "Oops!! Names are similar";
                        }   
                    }
                    else
                    {
                        if (!namePattern.IsMatch(firstName))
                        {
                            ViewBag.firstName = "Invalid!";
                        }
                        if (!namePattern.IsMatch(middleName))
                        {
                            ViewBag.middleName = "Invalid!!";
                        }
                        if (!namePattern.IsMatch(lastName))
                        {
                            ViewBag.lastName = "Invalid!!!";
                        }
                    }
                }
                else
                {
                    if (!namePattern.IsMatch(firstName))
                    {
                        ViewBag.firstName = "Invalid!";
                        ViewBag.middleName = "Invalid!! Please enter in the last name column if middle name does not exist";
                    }
                    else
                    {
                        ViewBag.middleName = "Invalid!! Please enter in the last name column if middle name does not exist";

                    }
                }

            }
             checkDob = ValidateDate(dob);
            if (checkDob != true)
            {
                ViewBag.dob = "Invalid Date!!";
            }
            try
            {
                if (gender=="m" || gender=="f")
                {
                    checkGender = true;
                }
                else
                {
                    ViewBag.gender = "Please choose appropriate gender!!";
                }
            }
            catch(Exception e) {
                ViewBag.gender = "Please choose appropriate gender!!";
            }
            

            if (addressPattern.IsMatch(address))
            {
                checkAddress = true;
            }
            else
            {
                ViewBag.address = "Invalid Address!!";
            }

            if (numberPattern.IsMatch(contact))
            {
                checkContact = true;
            }
            else
            {
                ViewBag.contact = "Invalid Contact!!";
            }
           
            if(checkName==true && checkDob==true && checkGender==true && checkAddress==true && checkContact == true)
            {
                string firstNameConverted = "", middleNameConverted = "", lastNameConverted = "", fullName = "";
                firstName = firstName.ToLower(); middleName = middleName.ToLower(); lastName = lastName.ToLower();
                firstNameConverted += char.ToUpper(firstName[0]) + firstName.Substring(1);
                if (middleName != "")
                {
                    middleNameConverted+= char.ToUpper(middleName[0]) + middleName.Substring(1);
                }
                if (lastName != "")
                {
                    lastNameConverted += char.ToUpper(lastName[0]) + lastName.Substring(1);
                }
                fullName += firstNameConverted + " " + middleNameConverted + " " + lastNameConverted;
                if (gender == "m")
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }
                //To generate last id
                int userId = 0;
                IEnumerable<Faculty> facultyLastRecord = db.Faculties.SqlQuery("SELECT Top 1 * FROM Faculties ORDER BY Id DESC");
                foreach(Faculty list in facultyLastRecord)
                {
                    userId = ++list.Id;
                }
                faculty.Id = userId;
                string username = "", mail = "";
                
                foreach(Faculty list in db.Faculties)
                {
                    string[] usernameParts = list.Username.Split('.');
                    if (usernameParts[0] == firstName.ToLower())
                    {
                        if (lastName != "")
                        {
                            username = lastName + "." + faculty.Id;
                            break;
                        }
                        else
                        {
                            username = firstName + "." + faculty.Id;
                            break;
                        }
                    }
                    else
                    {
                        username = firstName + "." + faculty.Id;
                        break;
                    }
                }
                mail = username + "@school.com";
                faculty.Name = fullName;
                faculty.Gender = gender;
                faculty.Dob = dob;
                faculty.Designation = "Faculty";
                faculty.Address = address;
                faculty.Contact = contact;
                faculty.MailId = mail;
                faculty.Username = username;
                faculty.Password = "School@123";
               
                int rowsAffected = db.Database.ExecuteSqlCommand("Insert into Faculties values(" + userId + ",'" + fullName + "','" + dob + "','Faculty'" + ",'" + address + "','" +
                    contact + "','" + mail + "','" + username + "','School@123','"+gender+"')");
                return RedirectToAction("FacultyAddedSuccessfully"); 
            }
            else
            {
                return View();
            }
            
        }
        public ViewResult FacultyAddedSuccessfully()
        {

            return View(faculty);
        }
        [NonAction]
        private bool ValidateDate(string date)
        {
            try
            {
                // for US, alter to suit if splitting on hyphen, comma, etc.
                string[] dateParts = date.Split('/');
                var year = Convert.ToInt32(dateParts[2]);

                DateTime testDate = new
                    DateTime(Convert.ToInt32(dateParts[2]),
                    Convert.ToInt32(dateParts[1]),
                    Convert.ToInt32(dateParts[0]));

                if(year>=1965 && year < 2000)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch
            {
                // if a test date cannot be created, the
                // method will return false
                return false;
            }
        }
       

       
    }
}