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
            item.DueAt = DateTimeOffset.Now.AddDays(3);

            _context.PlanItems.Add(item);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
