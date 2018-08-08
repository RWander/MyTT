using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using MyTT.Web.Models;
using MyTT.Web.Services;

namespace MyTT.Web.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlaneService _planeSrvc;
        private readonly UserManager<IdentityUser> _usrMgr;

        public PlanController(IPlaneService planeSrvc, UserManager<IdentityUser> usrMgr)
        {
            _planeSrvc = planeSrvc;
            _usrMgr = usrMgr;
        }

        public async Task<IActionResult> Index()
        {
            var curUsr = await _usrMgr.GetUserAsync(User);
            if (curUsr == null) return Challenge();

            var items = await _planeSrvc.GetIncompletePlanAsync(curUsr);

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

            var curUsr = await _usrMgr.GetUserAsync(User);
            if (curUsr == null) return Challenge();

            var successful = await _planeSrvc.AddPlanItemAsync(item, curUsr);
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

            var curUsr = await _usrMgr.GetUserAsync(User);
            if (curUsr == null) return Challenge();



            var successful = await _planeSrvc.MarkDoneAsync(id, curUsr);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }
            return RedirectToAction("Index");
        }
    }
}