using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using WebDemo.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace WebDemo
{
    public partial class HttpClientWebSite : System.Web.UI.Page
    {
        string url = Properties.Settings.Default.ApiUrl;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            //用非同步
            Person a = new Person("Kuan", 18);
            a.Height = 1.8;
            a.Weight = 70;
            double bmi = 0.0;
            var jsonText = JsonConvert.SerializeObject(a);
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(jsonText, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url+"GetUserDataApi/GetBMI", content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            bmi = JsonConvert.DeserializeObject<double>(responseBody);
            lblBMI.Text = $"計算出的BMI為{bmi}";
        }

        protected async void Button3_Click(object sender, EventArgs e)
        {
            List<Person> list = new List<Person>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5137/api/GetUserDataApi/GetAllPeople");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applicatioc/json"));
            HttpResponseMessage response = client.GetAsync(url+"GetUserDataApi/GetAllPeople").Result;
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Person>>(responseBody);
            }
            GridView1.DataSource = list;
            GridView1.DataBind();
        }

        protected async void Button2_Click(object sender, EventArgs e)
        {
            Person a = new Person("Kuan", 18);
            a.Height = 1.8;
            a.Weight = 70;
            double bmi = 0.0;
            var jsonText = JsonConvert.SerializeObject(a);
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(jsonText, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri("http://localhost:5137/");
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsync("/api/GetUserDataApi/GetBMI", content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            bmi = JsonConvert.DeserializeObject<double>(responseBody);
            lblBMI.Text = $"計算出的BMI為{bmi}";
        }
    }
}