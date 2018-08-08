using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MyTT.Web.Data;
using MyTT.Web.Models;

namespace MyTT.Web.Services
{
    public interface IPlaneService
    {
        Task<PlanItem[]> GetPlanAsync();
        Task<bool> AddPlanItemAsync(PlanItem item);
        Task<bool> MarkDoneAsync(Guid id);
    }

    public sealed class PlaneService : IPlaneService
    {
        private readonly ApplicationDbContext _context;

        public PlaneService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PlanItem[]> GetPlanAsync()
        {
            return await _context.PlanItems
                .Where(x => x.IsDone == false)
                .ToArrayAsync();
        }

        public async Task<bool> AddPlanItemAsync(PlanItem item)
        {
            item.Id = Guid.NewGuid();
            item.IsDone = false;

            _context.PlanItems.Add(item);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
            var item = await _context.PlanItems
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (item == null) return false;

            item.IsDone = true;
            
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated
        }
    }
}
