using RetirementPlanApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;


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
            return View(plans);
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RetirementPlan plan)
        {
            var response = await _httpClient.PostAsJsonAsync("api/retirementplans", plan);
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
            var response = await _httpClient.PutAsJsonAsync($"api/retirementplans/{plan.Id}", plan);
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