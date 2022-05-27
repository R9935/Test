using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_LOGIN_PAGE.Models
{
    public class AddEmp
    {
        public int E_ID { get; set; }
        [Required(ErrorMessage ="Naam dala Guru")]
        public string E_Name { get; set; }
        [Required(ErrorMessage ="Salary kitna ba ")]
        public int? E_Salary { get; set; }
        [Required]
        public string E_Company { get; set; }
        [Required]
        public string E_Dept { get; set; }
        [Required]
        public string E_Email_Id { get; set; }
        [Required(ErrorMessage ="ID Dala")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password Yaad Ba Ki Nahi")]
        public string password { get; set; }
    }
}