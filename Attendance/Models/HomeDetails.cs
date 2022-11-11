using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Attendance.Models
{
    public class loginDetail
    {
        [Required(ErrorMessage = "User Name is required")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
    public class RegistrationDetails
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone no. is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string UPassword { get; set; }

        [Required(ErrorMessage = "Confirmation Password is required")]
        [Compare("UPassword", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public string UCPassword { get; set; }
    }

    public class TimesheetDetails
    {
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
    }

    public class TimesheetDet
    {
        public string username { get; set; }
        public string attendancesDatetime { get; set; }
        public string attendancesDate { get; set; }
        public string Status { get; set; }
    }
}