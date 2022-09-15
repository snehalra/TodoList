﻿namespace TodoList.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TodoList.Models;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using static TodoList.Models.AccountViewModels;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<TodoList.Models.RecordContext>
    {
            public Configuration()
            {
                AutomaticMigrationsEnabled = false;
            }
           
        protected override void Seed(RecordContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        private void InitializeIdentityForEF(RecordContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //var myinfo = new MyUserInfo() { FirstName = "Snehal", LastName = "Ransing" };
            string name = "Admin";
            string password = "123456";

            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(name))
            {
                var roleresult = RoleManager.Create(new IdentityRole(name));
            }

            //Create User=Admin with password=123456
            var user = new ApplicationUser();
            user.UserName = name;
            var adminresult = UserManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, name);
            }
        }


    }

}
