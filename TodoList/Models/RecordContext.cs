using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using static TodoList.Models.AccountViewModels;

namespace TodoList.Models
{
    public class RecordContext : IdentityDbContext<ApplicationUser>
    {
        public RecordContext() : base("TodoListFinalRecords")
        {
        }
        public DbSet<Todo> Todo { get; set; }
        public DbSet<MyUserInfo> MyUserInfo { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            // Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");
        }
    }
}