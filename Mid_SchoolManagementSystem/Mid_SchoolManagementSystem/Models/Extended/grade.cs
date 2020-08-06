using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mid_SchoolManagementSystem.Models.DataAccess
{
    [MetadataType(typeof(admindata))]
    public partial class grade
    {
    }

    public class gradedata
    {
        [Display(Name = "Grade Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Grade Id is required")]
        public int gradeid { get; set; }

        [Display(Name = "Quiz 1 mark")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Quiz 2 mark is required")]
        public int quiz1 { get; set; }

        [Display(Name = "Quiz 2 mark")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Quiz 2 mark is required")]
        public int quiz2 { get; set; }

        [Display(Name = "Assignment mark")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Assignment mark is required")]
        public int assignment1 { get; set; }

        [Display(Name = "Half Yearly Grade")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Half Yearly Grade is required")]
        public int halfyearlygrade { get; set; }

        [Display(Name = "Quiz 3 mark")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Quiz 3 mark is required")]
        public int quiz3 { get; set; }

        [Display(Name = "Quiz 4 mark")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Quiz 4 mark is required")]
        public int quiz4 { get; set; }

        [Display(Name = "Assignment 2 mark")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Assignment 2 mark is required")]
        public int assignment2 { get; set; }

        [Display(Name = "Final Grade mark")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Final Grade is required")]
        public int finalexamgrade { get; set; }
    }
}