using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoList.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Le champ nom est obligatoire")]
        [RegularExpression("^[A-Z][a-z0-9_-]{5, 20}$", ErrorMessage = "Mauvais format")]

        public string Name { get; set; }

        //public ICollection<Todo> Todos { get; set; }
    }
}