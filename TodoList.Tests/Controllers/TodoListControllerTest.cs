using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoList.Controllers;
using TodoList.Models;
using TodoList.Service;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using System.Security.Principal;
using static TodoList.Models.AccountViewModels;
using Microsoft.AspNet.Identity;
using Moq;
using System.Security.Claims;
using System.Threading;

namespace TodoList.Tests.Controllers
{
    [TestClass]
    public class TodoListControllerTest
    {
        [TestMethod]
        public void IndexView()
        {
            var todocontroller = GetTodoListController(new InMemoryTodoRepo());
            ViewResult result = todocontroller.Index();
            Assert.AreEqual("Index", result.ViewName);
        }

        private static TodoListController GetTodoListController(ITodoListService todoList)
        {
            TodoListController todocontroller = new TodoListController(todoList);

            todocontroller.ControllerContext = new ControllerContext()
            {
                Controller = todocontroller,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };
            return todocontroller;
        }
        private static AccountController GetAccountController()
        {
            AccountController accountcontroller = new AccountController();

            accountcontroller.ControllerContext = new ControllerContext()
            {
                Controller = accountcontroller,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };
            return accountcontroller;
        }
        Todo GetTodo(int id, string name, string dueDate, string status, string priority, ApplicationUser user)
        {
            return new Todo
            {
                ID = id,
                Name = name,
                DueDate = Convert.ToDateTime(dueDate),
                Status = status,
                Priority = priority,
                User = user,

            };
        }

        //[TestMethod]
        //public void GetAllTodoesByID()
        //{
        //    // Arrange  
        //    Todo todoTask1 = GetTodo(1, "Coding Task", "2022/10/09", "New", "Urgent", new ApplicationUser() { UserName = "ancon1", Email = "ancon@mail.com" });
        //    Todo todoTask2 = GetTodo(2, "Gym", "2022/09/19", "In Progress", "QUITE IMPORTANT", new ApplicationUser() { UserName = "ancon1", Email = "ancon@mail.com" });
        //    InMemoryTodoRepo todoRepo = new InMemoryTodoRepo();
        //    todoRepo.Add(todoTask1);
        //    todoRepo.Add(todoTask2);
        //    var user = new ApplicationUser() { UserName = "Admin", Email = "ancon@mail.com" };

        //    var controller = GetTodoListController(todoRepo);
        //    var result = controller.Index();
        //    var datamodel = (IEnumerable<Todo>)result.ViewData.Model;
        //    CollectionAssert.Contains(datamodel.ToList(), todoTask1);        
        //    Assert.IsNotNull(result);
        //}

        [TestMethod]
        public void GetAllTodos()
        {
            // Arrange  
            Todo todoTask1 = GetTodo(1, "Coding Task", "2022/10/09", "New", "Urgent", new ApplicationUser() { UserName = "ancon1", Email = "ancon@mail.com" });
            Todo todoTask2 = GetTodo(2, "Gym", "2022/09/19", "In Progress", "QUITE IMPORTANT", new ApplicationUser() { UserName = "ancon1", Email = "ancon@mail.com" });
            InMemoryTodoRepo todoRepo = new InMemoryTodoRepo();
            todoRepo.Add(todoTask1);
            todoRepo.Add(todoTask2);

            var controller = GetTodoListController(todoRepo);
            var result = controller.All();
            var datamodel = (IEnumerable<Todo>)result.ViewData.Model;
            CollectionAssert.Contains(datamodel.ToList(), todoTask1);
            CollectionAssert.Contains(datamodel.ToList(), todoTask2);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreatePostTodoInRepository()
        {
            InMemoryTodoRepo todorepository = new InMemoryTodoRepo();
            TodoListController todocontroller = GetTodoListController(todorepository);
            Todo todoData = GetTodoID();
            todocontroller.Create(todoData);
            IEnumerable<Todo> todoDatas = todorepository.GetAllTodos();
            Assert.IsTrue(todoDatas.Contains(todoData));
        }

        Todo GetTodoID()
        {
            return GetTodo(1, "Coding Task", "2022/10/09", "New", "Urgent", new ApplicationUser() { UserName = "ancon1", Email = "ancon@mail.com" });
        }
        RegisterViewModel GetRegisterUser()
        {
            return RegisterViewModel("TestUser", "Abcd123", "Abcd123");
        }
        RegisterViewModel RegisterViewModel(string userName, string password, string confirmpassword)
        {
            return new RegisterViewModel
            {
                UserName = userName,
                Password = password,
                ConfirmPassword = confirmpassword

            };
        }
        [TestMethod]
        public void RegisterUserInRepository()
        {
            AccountController accountcontroller = GetAccountController();
            RegisterViewModel registerdata = GetRegisterUser();
            var result = accountcontroller.Register(registerdata);

            Assert.IsNotNull(result);
        }


        private class MockHttpContext : HttpContextBase
        {
            public readonly IPrincipal _user = new GenericPrincipal(new GenericIdentity("Admin"), null);

            public override IPrincipal User
            {
                get
                {
                    return _user;
                }
                set
                {
                    base.User = value;
                }
            }
        }



    }
}
