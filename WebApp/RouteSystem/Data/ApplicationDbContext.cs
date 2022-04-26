using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Route.Domain.Model;

namespace RouteSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Route.Domain.Model.WorkTeam> WorkTeam { get; set; }
        public DbSet<Route.Domain.Model.Person> Person { get; set; }
        public DbSet<Route.Domain.Model.City> City { get; set; }
    }
}
