using ApiDemo.Models;
using ApiDemo.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace ApiDemo.Controllers
{
    public class GetUserDataApiController : ApiController
    {
        //http://localhost:5137/api/GetUserDataApi/GetAllPeople
        [HttpGet]
        public IHttpActionResult GetAllPeople()
        {
            List<Person> kk = new List<Person>();
            kk = HealthService.GetAllPeople();
            if (kk == null)
            {
                return NotFound();
            }

            return Ok(kk);

        }
        [HttpPost]
        public IHttpActionResult GetBMI(Person human)
        {
            double bmi = HealthService.CalculateBMI(human);
            if (bmi < 0)
                return NotFound();

            return Ok(bmi);


        }
        [HttpPost]
        public HttpResponseMessage GetBMIForXml(HttpRequestMessage request)
        {//encoding="utf-8"才能接
            Person p = new Person();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(request.Content.ReadAsStreamAsync().Result);
            var str = xmlDoc.InnerXml;
            XmlSerializer serializer = new XmlSerializer(typeof(Person));
            using (StringReader reader = new StringReader(str))
            {
                p = (Person)(serializer.Deserialize(reader));
            }


            double bmi = HealthService.CalculateBMI(p);

            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new ObjectContent<double>(bmi, new XmlMediaTypeFormatter { UseXmlSerializer = true });

            return resp;




        }
        [HttpGet]
        public HttpResponseMessage GetAllPeopleForXml()
        {
            List<Person> kk = new List<Person>();
            kk = HealthService.GetAllPeople();

            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new ObjectContent<List<Person>>(kk, new XmlMediaTypeFormatter { UseXmlSerializer = true });

            return resp;

        }
    }
}
