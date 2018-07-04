using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Linq;

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
            //Ma méthode
            List<TestModel> liste = new List<TestModel>();
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            IEnumerable<XElement> tests = doc.Root.Elements();
            foreach (var test in tests)
            {
                TestModel newTest = new TestModel()
                {
                    ID = int.Parse(test.Element("ID").Value),
                    Commentaire = test.Element("Commentaire").Value
                };
                liste.Add(newTest);
            }
            return liste;


            //Seulement Linq
            //XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            //return (from x in doc.Descendants("Test")
            //        select new TestModel
            //        {
            //            ID = int.Parse(x.Element("ID").Value),
            //            Commentaire = x.Element("Commentaire").Value
            //        }).ToList();

            //Expression lambda
            //XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            //return doc.Descendants("Test").Select(x => new TestModel
            //{
            //    ID = int.Parse(x.Element("ID").Value),
            //    Commentaire = x.Element("Commentaire").Value
            //}).ToList();


        }

        //GET : api/test/42
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult GetTest(int id)
        {

            //Ma Méthode
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            foreach (var x in doc.Descendants("Test"))
            {
                if (int.Parse(x.Element("ID").Value) == id)
                {
                    return Ok(new TestModel
                    {
                        ID = int.Parse(x.Element("ID").Value),
                        Commentaire = x.Element("Commentaire").Value
                    });
                }
            }
            return NotFound();

            //Linq

            /*var test = doc.Descendants("Test").SingleOrDefault(
                x => int.Parse(x.Element("ID").Value) == id);
            if (test == null)
            {
                return NotFound();
            }
            return Ok(new TestModel
            {
                ID = int.Parse(test.Element("ID").Value),
                Commentaire = test.Element("Commentaire").Value
            });*/
        }

        //POST : api/test
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult PostTest(TestModel test)
        {
            if (test.ID != 0)
            {
                return BadRequest();
            }

            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            var idMax = doc.Descendants("Test").Max(x => int.Parse(x.Element("ID").Value));
            idMax++;
            test.ID = idMax;


            XElement element = new XElement("Test");
            element.Add(new XElement("ID", test.ID));
            element.Add(new XElement("Commentaire", test.Commentaire));
            doc.Element("Tests").Add(element);
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));


            return CreatedAtRoute("DefaultApi", new { id = test.ID }, test);
        }

        //PUT : api/test
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult PutTest(int id,TestModel test)
        {
            //Tester l'id avec l'id de test
            if (id != test.ID)
            {
                return BadRequest();
            }

            //Récuperer le document XML
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            //Rechercher le Xelement en fonction de l'id et retourner NotFound si non trouvé
            var elem = doc.Descendants("Test").SingleOrDefault(
                x => int.Parse(x.Element("ID").Value) == id);
            if (elem == null)
            {
                return NotFound();
            }
            //Modifier les valeurs ID et Commentaire avec celles de test
            elem.Element("Commentaire").Value = test.Commentaire;
            //Sauvegarder
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            return StatusCode(HttpStatusCode.NoContent);
            /*return Ok(new TestModel
                    {
                        ID = int.Parse(elem.Element("ID").Value),
                        Commentaire = elem.Element("Commentaire").Value
                    });*/

        }

        //DELETE : api/test
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult DeleteTest(int id)
        {
            //Récuperer le document XML
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            //Rechercher le Xelement en fonction de l'id et retourner NotFound si non trouvé
            var elemASuppr = doc.Descendants("Test").SingleOrDefault(
                x => int.Parse(x.Element("ID").Value) == id);
            if (elemASuppr == null)
            {
                return NotFound();
            }

            //Supprimer l'élément
            elemASuppr.Remove();

            //Sauvegarder
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            return Ok(new TestModel
            {
                ID = id,
                Commentaire = elemASuppr.Element("Commentaire").Value
            });
        }

    }
}
