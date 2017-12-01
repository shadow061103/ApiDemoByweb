using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDemo.Models;

namespace WebDemo
{
    public partial class WebClientWebSite : System.Web.UI.Page
    {
        string url = Properties.Settings.Default.ApiUrl;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Person a = new Person("Kuan", 18);
            a.Height = 1.8;
            a.Weight = 70;
            double bmi = 0.0;
            string jsonData = JsonConvert.SerializeObject(a);
            byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var response = client.UploadData(url+"GetUserDataApi/GetBMI", "POST", jsonByte);
            bmi = JsonConvert.DeserializeObject<double>(Encoding.UTF8.GetString(response));

            lblBMI.Text = $"計算出的BMI為{bmi}";

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            List<Person> list = new List<Person>();
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add(HttpRequestHeader.ContentType, "applicatioc/json");
            var body = client.DownloadString(url+"GetUserDataApi/GetAllPeople");
            list = JsonConvert.DeserializeObject<List<Person>>(body);
            GridView1.DataSource = list;
            GridView1.DataBind();
        }
    }
}