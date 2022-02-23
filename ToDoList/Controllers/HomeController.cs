using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using TodoList.Helpers;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            return View("Index", viewModel);
        }

        public IActionResult Edit(int id)
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            viewModel.EditableItem = viewModel.TodoItems.FirstOrDefault(x => x.Id == id);
            return View("Index", viewModel);
        }

        public IActionResult Delete(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                ToDoListItem item = db.Get<ToDoListItem>(id);
                if (item != null)
                    db.Delete(item);
                return RedirectToAction("Index");
            }
        }

        public IActionResult CreateUpdate(TodoListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = DbHelper.GetConnection())
                {
                    if (viewModel.EditableItem.Id <= 0)
                    {
                        viewModel.EditableItem.AddDate = DateTime.Now;
                        viewModel.EditableItem.SetDate = DateTime.Now;
                        db.Insert<ToDoListItem>(viewModel.EditableItem);
                    }
                    else
                    {
                        ToDoListItem dbItem = db.Get<ToDoListItem>(viewModel.EditableItem.Id);
                        TryUpdateModelAsync<ToDoListItem>(dbItem, "EditableItem");
                        db.Update<ToDoListItem>(dbItem);
                    }
                }
                return RedirectToAction("Index");
            }
            else
                return View("Index", new TodoListViewModel());
        }

        public IActionResult ToggleIsDone(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                ToDoListItem item = db.Get<ToDoListItem>(id);
                if (item != null)
                {
                    item.STATUS = !item.STATUS;
                    db.Update<ToDoListItem>(item);
                }
                return RedirectToAction("Index");
            }
        }
    }
}