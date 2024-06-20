using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetirementPlanApi.Models
{
    public class RetirementPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Contributions { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

