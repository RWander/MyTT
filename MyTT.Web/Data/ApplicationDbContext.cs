using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using MyTT.Web.Models;

namespace MyTT.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<PlanItem> PlanItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
