using Calculator.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.DAL
{

    public class CalculatorDbContext:IdentityDbContext<User>
    {
        public CalculatorDbContext(DbContextOptions<CalculatorDbContext> options):base(options)
        {
            Database.EnsureCreated();
        } 
        public DbSet<Example> Examples { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Example>().HasKey(x => x.Id);
        }
    }
}
