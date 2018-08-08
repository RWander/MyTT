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

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPlanItem(PlanItem item)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var successful = await _planeSrvc.AddPlanItemAsync(item);
            if (!successful)
            {
                return BadRequest("Could not add item.");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }
            var successful = await _planeSrvc.MarkDoneAsync(id);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }
            return RedirectToAction("Index");
        }
    }
}