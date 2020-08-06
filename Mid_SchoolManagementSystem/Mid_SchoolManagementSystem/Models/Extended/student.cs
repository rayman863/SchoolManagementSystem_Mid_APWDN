using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mid_SchoolManagementSystem.Models.DataAccess
{
    [MetadataType(typeof(studentdata))]
    public partial class student
    {
        public string studentconfirmpassword { get; set; }
    }
    public class studentdata
    {
        public int id { get; set; }

        [Display(Name = "Student ID")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Super Admin ID is required")]
        public string studentid { get; set; }
        [Display(Name = "Student Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Student Name is required")]
        public string studentname { get; set; }
        [Display(Name = "Student Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Student Password is required")]
        [MinLength(8, ErrorMessage = "Atleast 8 characters required")]
        public string studentpassword { get; set; }
        [Display(Name = "Student Date Of Birth")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Birth Date is required")]
        [DataType(DataType.Date)]
        public System.DateTime studentdob { get; set; }
        [Display(Name = "Student Phone")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number is required")]
        public int studentphone { get; set; }
        [Display(Name = "Student Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Student Address is required")]
        public string studentaddress { get; set; }
        public string studentemail { get; set; }
        public string studentbloodgroup { get; set; }
        public int studentfees { get; set; }
        public int classid { get; set; }
        public int sectionid { get; set; }
    }
}