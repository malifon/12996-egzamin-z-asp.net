using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using TodoList.Helpers;

namespace ToDoList.Models
{
    public class TodoListViewModel
    {
        public TodoListViewModel()
        {
            using (var db = DbHelper.GetConnection())
            {
                this.EditableItem = new ToDoListItem();
                this.TodoItems = db.Query<ToDoListItem>("SELECT * FROM [Table] ORDER BY AddDate DESC").ToList();
            }
        }

        public List<ToDoListItem> TodoItems { get; set; }

        public ToDoListItem EditableItem { get; set; }
    }
}
