using JobApplication.Models;
using JobApplication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IJobService _jobService;

        public HomeController(ILogger<HomeController> logger, IJobService jobService)
        {
            _logger = logger;
            _jobService = jobService;

        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Index()
        {
            var joblist = await _jobService.GetList();
            return View(joblist);
        }


        [HttpPost]
        public async Task<JsonResult> AddJob(JobModel jobmodel)
        {
            // add job           
            var result = await _jobService.AddJob(jobmodel);

            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetJob(int Id)
        {
            // Get job
            var jobitem = await _jobService.GetJob(Id);

            return Json(jobitem);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateJob(JobModel jobmodel)
        {
            // UpdateJob
            var result = await _jobService.UpdateJob(jobmodel);

            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> DeleteJob(int Id)
        {
            // DeleteJob
            var result = await _jobService.DeleteJob(Id);

            return Json(result);
        }
    }
}
