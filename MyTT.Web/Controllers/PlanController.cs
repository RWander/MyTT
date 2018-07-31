using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MyTT.Web.Models;
using MyTT.Web.Services;

namespace MyTT.Web.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlaneService _planeSrvc;

        public PlanController(IPlaneService planeSrvc)
        {
            _planeSrvc = planeSrvc;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _planeSrvc.GetPlanAsync();

            var model = new PlanViewModel()
            {
                Items = items
            };
            return View(model);
        }
    }
}