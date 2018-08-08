using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MyTT.Web.Data;
using MyTT.Web.Models;

namespace MyTT.Web.Services
{
    public interface IPlaneService
    {
        Task<PlanItem[]> GetIncompletePlanAsync(IdentityUser user);
        Task<bool> AddPlanItemAsync(PlanItem item, IdentityUser user);
        Task<bool> MarkDoneAsync(Guid id, IdentityUser user);
    }

    public sealed class PlaneService : IPlaneService
    {
        private readonly ApplicationDbContext _context;

        public PlaneService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PlanItem[]> GetIncompletePlanAsync(IdentityUser user)
        {
            return await _context.PlanItems
                .Where(x => x.IsDone == false && x.UserId == user.Id)
                .ToArrayAsync();
        }

        public async Task<bool> AddPlanItemAsync(PlanItem item, IdentityUser user)
        {
            item.Id = Guid.NewGuid();
            item.UserId = user.Id;
            item.IsDone = false;

            _context.PlanItems.Add(item);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> MarkDoneAsync(Guid id, IdentityUser user)
        {
            var item = await _context.PlanItems
                .Where(x => x.Id == id && x.UserId == user.Id)
                .SingleOrDefaultAsync();

            if (item == null) return false;

            item.IsDone = true;
            
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated
        }
    }
}
