using Newtonsoft.Json;
using RetirementPlanApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace RetirementPlanApi.Repositories
{
    public class RetirementPlanRepository
    {
        //private readonly string _filePath = HttpContext.Current.Server.MapPath("~/App_Data/retirementPlans.json");
        //private readonly string _filePath = "L:\rps\RetirementPlanApi\RetirementPlanApi\App_Data\retirementPlans.json";
        private readonly string _filePath = @"L:\rps\RetirementPlanApi\RetirementPlanApi\App_Data\retirementPlans.json";
        public List<RetirementPlan> GetAll()
        {
            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<RetirementPlan>>(jsonData);
        }

        public RetirementPlan GetById(int id)
        {
            var plans = GetAll();
            return plans.FirstOrDefault(p => p.Id == id);
        }

        public void Create(RetirementPlan plan)
        {
            var plans = GetAll();
            plan.Id = plans.Any() ? plans.Max(p => p.Id) + 1 : 1;
            plans.Add(plan);
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(plans, Formatting.Indented));
        }

        public void Update(RetirementPlan plan)
        {
            var plans = GetAll();
            var existingPlan = plans.FirstOrDefault(p => p.Id == plan.Id);
            if (existingPlan != null)
            {
                plans.Remove(existingPlan);
                plans.Add(plan);
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(plans, Formatting.Indented));
            }
        }

        public void Delete(int id)
        {
            var plans = GetAll();
            var plan = plans.FirstOrDefault(p => p.Id == id);
            if (plan != null)
            {
                plans.Remove(plan);
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(plans, Formatting.Indented));
            }
        }

    }
}