using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDemo.Models;
using System.Xml.Serialization;
using System.IO;
using System.Net.Http;
namespace WebDemo
{
    public partial class XmlWebsite : System.Web.UI.Page
    {
        string url = Properties.Settings.Default.ApiUrl;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Person a = new Person("Kuan", 18);
            a.Height = 1.8;
            a.Weight = 70;
            double bmi = 0.0;
            var xmlData = new Utf8StringWriter();

            var serializer = new XmlSerializer(typeof(Person));
            serializer.Serialize(xmlData, a);




            byte[] xmlByte = Encoding.UTF8.GetBytes(xmlData.ToString());

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url+"GetUserDataApi/GetBMIForXml");
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/xml";// application/x-www-form-urlencoded";
            request.ContentLength = xmlByte.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(xmlByte, 0, xmlByte.Length);
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var stream = response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        var temp = reader.ReadToEnd();
                        var stringReader = new StringReader(temp);
                        var serializer2 = new XmlSerializer(typeof(double));
                        bmi = (double)serializer2.Deserialize(stringReader);


                    }
                }
            }
            lblBMI.Text = $"計算出的BMI為{bmi}";




        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            List<Person> list = new List<Person>();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url+"GetUserDataApi/GetAllPeopleForXml");
            request.Method = WebRequestMethods.Http.Get;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var stream = response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        var temp = reader.ReadToEnd();

                        //反序列化
                        var stringReader = new StringReader(temp);
                        var serializer2 = new XmlSerializer(typeof(List<Person>));
                        list = (List<Person>)serializer2.Deserialize(stringReader);
                    }
                }
            }
            GridView1.DataSource = list;
            GridView1.DataBind();

        }
    }
}