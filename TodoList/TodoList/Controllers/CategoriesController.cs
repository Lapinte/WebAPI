using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class CategoriesController : ApiController
    {
        //ouverture de la connexion à la db
        private TodoListDbContext db = new TodoListDbContext();

        
        public List<Category> GetCategories()
        {
            return db.Categories.ToList();
        }

        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            var category = db.Categories.SingleOrDefault(x => x.ID == id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }



        [ResponseType(typeof(Category))]
        public  IHttpActionResult PostCategory(Category category)
        {
            if (! ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.ID }, category);
        }

        [ResponseType(typeof(Category))]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (id != category.ID)
            {
                return BadRequest();
            }

            var categoryToModify = db.Categories.SingleOrDefault(x => x.ID == id);
            if (categoryToModify == null)
                return NotFound();

            categoryToModify.Name = category.Name;

            db.SaveChanges();


            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            var elemASuppr = db.Categories.SingleOrDefault(x => x.ID == id);
            if (elemASuppr == null)
            {
                return NotFound();
            }
            db.Categories.Remove(elemASuppr);
            db.SaveChanges();
            return Ok(new Category
            {
                ID = id,
                Name = elemASuppr.Name
            });
        }

        //réécriture de la méthode dispose pour libérer en mmémoire le DbContext
        //et donc la connaxion
        //methode dispose appelée lorsque IIS n'utilise plus le controller
        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();//libère le db context
            base.Dispose(disposing);
        }
    }
}
