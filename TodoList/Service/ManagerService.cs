using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoList.Models;
using static TodoList.Models.AccountViewModels;

namespace TodoList.Service
{
    public class ManagerService: IManagerService
    {
        private RecordContext _db = new RecordContext();
        public UserManager<ApplicationUser> Getmanager()
        {
            return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
        }
    }
}