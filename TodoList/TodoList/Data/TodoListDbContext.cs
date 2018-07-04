using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoList.Models;

namespace TodoList.Data
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext() : base("TodoList")
        {
        }

        //public DbSet<Category> Categories { get; set; }
    }
}