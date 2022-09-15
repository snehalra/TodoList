using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoList.Models;
using static TodoList.Models.AccountViewModels;

namespace TodoList.App_Start
{
    public class MyDbInitializer : DropCreateDatabaseAlways<RecordContext>
    {
        protected override void Seed(RecordContext context)
        {
            //InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //private void InitializeIdentityForEF(RecordContext context)
        //{
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        //    var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        //    var myinfo = new MyUserInfo() { FirstName = "Snehal", LastName = "Ransing" };
        //    string name = "Admin";
        //    string password = "123456";
        //    string test = "test";

        //    //Create Role Test and User Test
        //    RoleManager.Create(new IdentityRole(test));
        //    UserManager.Create(new ApplicationUser() { UserName = test });

        //    //Create Role Admin if it does not exist
        //    if (!RoleManager.RoleExists(name))
        //    {
        //        var roleresult = RoleManager.Create(new IdentityRole(name));
        //    }

        //    //Create User=Admin with password=123456
        //    var user = new ApplicationUser();
        //    user.UserName = name;
        //    user.MyUserInfo = myinfo;
        //    var adminresult = UserManager.Create(user, password);

        //    //Add User Admin to Role Admin
        //    if (adminresult.Succeeded)
        //    {
        //        var result = UserManager.AddToRole(user.Id, name);
        //    }
        //}
    }
}