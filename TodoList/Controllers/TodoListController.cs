using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TodoList.Models;
using TodoList.Service;
using static TodoList.Models.AccountViewModels;

namespace TodoList.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        ITodoListService _todoservice;
        
        public TodoListController() : this(new TodoListService()) { }
        
      

        public TodoListController(ITodoListService todoservice)
        {
                   
            _todoservice = todoservice;
            
           
        }


        public UserManager<ApplicationUser> manager()
        {
            return _todoservice.Getmanager();
        }

        // GET: Todo List
        public ViewResult Index()
        {
           
            var currentUser = manager().FindById(User.Identity.GetUserId());
            IEnumerable<Todo> TodoListData = _todoservice.GetAllTodosByUser(currentUser);
            return View("Index",TodoListData);
        }
     



    // GET: /ToDo/All
    [Authorize(Roles = "Admin")]
        public ViewResult All()
        {
            IEnumerable<Todo> TodoListData = _todoservice.GetAllTodos();
            return View(TodoListData);
        }

        // GET: Todos/Details/5
        public ActionResult Details(int? id)
        {
            var currentUser = manager().FindById(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todoData = _todoservice.GetTodoByID(id);

            if (todoData == null)
            {
                return HttpNotFound();
            }
            if (todoData.User.Id != currentUser.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(todoData);
        }
        // GET: Todos/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,DueDate,Status,Priority")] Todo todoData)
        {

            try
            {                
                if (ModelState.IsValid)
                {
                    todoData.User = manager().FindById(User.Identity.GetUserId());
                   _todoservice.CreateNewTodo(todoData);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }


            return View(todoData);
        }

        // GET: Todos/Edit/5
        public ActionResult Edit(int? id)
        {
            var currentUser = manager().FindById(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var todoDataToEdit = _todoservice.GetTodoByID(id);
            if (todoDataToEdit == null)
            {
                return HttpNotFound();
            }
            if (todoDataToEdit.User.Id != currentUser.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(todoDataToEdit);
        }

        // POST: Todos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,DueDate,Status,Priority")] Todo todos)
        {

            if (ModelState.IsValid)
            {
                Todo todoData = _todoservice.GetTodoByID(todos.ID);
                try
                {
                    if (TryUpdateModel(todoData))
                    {
                        _todoservice.SaveChanges();
                        return RedirectToAction("index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }

            }
            return View(todos);
        }

        // GET: Todos/Delete/5
        public ActionResult Delete(int? id)
        {
            var currentUser = manager().FindById(User.Identity.GetUserId());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var todoToDel = _todoservice.GetTodoByID(id);
            if (todoToDel == null)
            {
                return HttpNotFound();
            }
            if (todoToDel.User.Id != currentUser.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(todoToDel);
        }
        // POST: Todos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _todoservice.DeleteTodo(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }


        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _todoservice.Dispose();                
            }
            base.Dispose(disposing);
        }

    }
}