using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{  /// <summary>

   /// Project Name  :   School Management System
   /// Description   :   A Complete management system for schools to maintain its office, principal, faculties and students records 
   ///                   with enhanced feature to scale up its data and tend to provide access for individuals to showcase their review,
   ///                   attendance, leaves and marks in an agile availability model. Entity domain classes are used for the back end
   ///                   data management and manipulation. 
   /// Date Created  :   05-Nov-2020.
   /// Date Modified :   16-Nov-2020.
   /// Author Name   :   Rishwanth Rajaa
   /// 
   /// This is the default class during the start up of the application which tend to trigger the Home Page of the 
   /// application.
   /// </summary>
    public class HomeController : Controller
    {
        public ViewResult Index() // A ViewResult type method to respond for the default home page of the application.
        {
            return View();
        }
    }
}