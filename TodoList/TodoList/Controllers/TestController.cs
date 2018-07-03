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
        public IHttpActionResult PostTest(TestModel test)
        {
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            XElement last = (XElement)doc.Root.LastNode;

            XElement newElem = new XElement("Test");
            newElem.Add(new XElement("ID", int.Parse(last.Element("ID").Value) + 1));
            newElem.Add(new XElement("Commentaire", test.Commentaire));

            if (test.ID != 0)
            {
                return BadRequest();
            }
            doc.Root.Add(newElem);

            return Ok(doc);
        }
    }
}
