﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetirementPlanApp.Models
{
    public class RetirementPlan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Plan Name")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Contribution is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter a valid number")]
        public double Contributions { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Start Date is required")]

        [Display(Name = "Plan Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "End Date is required")]
        [Display(Name = "Plan End Date")]

        public DateTime EndDate { get; set; }
    }

}