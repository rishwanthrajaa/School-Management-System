//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{

    public partial class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dob { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string MailId { get; set; }
        [Required(ErrorMessage ="Can't be empty")]
        [RegularExpression(@"^[a-z]+$",ErrorMessage ="Invaild RegEx")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Passwod can't be empty")]
        public string Password { get; set; }
        public string Gender { get; set; }
    }
}
