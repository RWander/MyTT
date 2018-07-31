using System;
using System.Threading.Tasks;

using MyTT.Web.Models;

namespace MyTT.Web.Services
{
    public interface IPlaneService
    {
        Task<PlanItem[]> GetPlanAsync();
    }

    public sealed class FakePlaneService : IPlaneService
    {
        public Task<PlanItem[]> GetPlanAsync()
        {
            var item1 = new PlanItem
            {
                Title = "Learn ASP.NET Core",
                DueAt = DateTimeOffset.Now.AddDays(1)
            };
            var item2 = new PlanItem
            {
                Title = "Build awesome apps",
                DueAt = DateTimeOffset.Now.AddDays(2)
            };
            return Task.FromResult(new[] { item1, item2 });
        }
    }
}
