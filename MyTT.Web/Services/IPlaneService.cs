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
    }
}
