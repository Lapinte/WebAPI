using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TodoList.Data;
using TodoList.Models;
using System.Data.Entity;

namespace TodoList.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        //ouverture de la connexion à la db
        private TodoListDbContext db = new TodoListDbContext();

        [ResponseType(typeof(Category))]
        public List<Category> GetCategories()
        {
            return db.Categories.Where(x=> !x.Deleted).ToList();
        }

        [Route("{name}")]
        [ResponseType(typeof(Category))]
        public List<Category> GetCategories(string name)
        {
            return db.Categories.Where(x => !x.Deleted && x.Name.Contains(name)).ToList();
        }

        [Route("{id:int}")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }


        [ResponseType(typeof(Category))]
        public  IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.ID }, category);
        }

        [Route("{id:int}")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (id != category.ID)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (db.Categories.Find(id).Deleted)
                return BadRequest();

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                return NotFound();

            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{id:int}")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            var elemASuppr = db.Categories.Find(id);
            if (elemASuppr == null)
            {
                return NotFound();
            }
            //db.Categories.Remove(elemASuppr);
            elemASuppr.Deleted = true;
            elemASuppr.DeletedAt = DateTime.Now;
            db.Entry(elemASuppr).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Ok(elemASuppr);
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
