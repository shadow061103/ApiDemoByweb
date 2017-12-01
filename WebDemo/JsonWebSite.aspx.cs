using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDemo.Models;

namespace WebDemo
{
    public partial class JsonWebSite : System.Web.UI.Page
    {
        string url = Properties.Settings.Default.ApiUrl;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //算BMI
            Person a = new Person("Kuan", 18);
            a.Height = 1.8;
            a.Weight = 70;
            double bmi = 0.0;

            string jsonData = JsonConvert.SerializeObject(a);
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url+"GetUserDataApi/GetBMI");
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/json";
            request.ContentLength = jsonByte.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(jsonByte, 0, jsonByte.Length);
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    using (var stream = response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        var temp = reader.ReadToEnd();

                        //反序列化
                        bmi = JsonConvert.DeserializeObject<double>(temp);
                    }
                }
            }
            lblBMI.Text = $"計算出的BMI為{bmi}";


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            List<Person> list = new List<Person>();



            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url+"GetUserDataApi/GetAllPeople");
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
                        list = JsonConvert.DeserializeObject<List<Person>>(temp);
                    }
                }
            }
            GridView1.DataSource = list;
            GridView1.DataBind();
        }
    }
}