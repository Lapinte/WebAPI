using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TodoList.Models
{
    [Table(name:"Todos")]
    public class Todo : BaseModel
    {
        //public int ID { get; set; }

        //[Column("Nom")]
        public string Name { get; set; }

        public bool Done { get; set; }

        public DateTime DeadLineDate { get; set; }

        public int Priority { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
    }
}