using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TodoList.Models.AccountViewModels;

namespace TodoList.Service
{
   public interface IManagerService
    {
        UserManager<ApplicationUser> Getmanager();
    }
}
