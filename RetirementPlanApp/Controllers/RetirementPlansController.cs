using RetirementPlanApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;


namespace RetirementPlanApp.Controllers
{

    public class RetirementPlansController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:55674/") };
        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/retirementplans");
            response.EnsureSuccessStatusCode();
            var plans = await response.Content.ReadAsAsync<List<RetirementPlan>>();
            var sortedPlans = plans.OrderBy(p => p.Id).ToList();

            return View(sortedPlans);
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> Create(RetirementPlan plan)
        {
            if (!ModelState.IsValid)
            {
                return View(plan);
            }

            // Fetch the existing plans
            var response = await _httpClient.GetAsync("api/retirementplans");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON string to a dynamic object
            var plans = JsonConvert.DeserializeObject<List<RetirementPlan>>(jsonString);

            // Check for duplicate names
            if (plans.Any(p => p.Name.Equals(plan.Name, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError("Name", "A plan with this name already exists.");
                return View(plan);
            }

            // If no duplicate, proceed to create the new plan
            response = await _httpClient.PostAsJsonAsync("api/retirementplans", plan);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Edit(int id)
        {

            var response = await _httpClient.GetAsync($"api/retirementplans/{id}");
            response.EnsureSuccessStatusCode();
            var plan = await response.Content.ReadAsAsync<RetirementPlan>();
            return View(plan);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RetirementPlan plan)
        {
            if (!ModelState.IsValid)
            {
                return View(plan);
            }

            var response = await _httpClient.GetAsync("api/retirementplans");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON string to a dynamic object
            var plans = JsonConvert.DeserializeObject<List<RetirementPlan>>(jsonString);

            // Check for duplicate names
            if (plans.Any(p => p.Name.Equals(plan.Name, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError("Name", "A plan with this name already exists.");
                return View(plan);
            }


            response = await _httpClient.PutAsJsonAsync($"api/retirementplans/{plan.Id}", plan);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/retirementplans/{id}");
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }

}