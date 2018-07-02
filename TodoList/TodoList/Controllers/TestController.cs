using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TodoList.Controllers
{
    public class TestModel
    {
        public int ID { get; set; }

        public string Commentaire { get; set; }
    }
    public class TestController : ApiController
    {
        //GET : api/test
        public List<TestModel> GetTests()
        {
            List<TestModel> liste = new List<TestModel>();
            liste.Add(new TestModel { ID = 42, Commentaire = "la réponse" });
            liste.Add(new TestModel { ID = 39, Commentaire = "la température actuelle" });
            liste.Add(new TestModel { ID = 98, Commentaire = "au hasard" });
            return liste;
        }
    }
}
