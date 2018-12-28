using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;

namespace lssAngular2.Controllers
{
    public class DataService
    {
        string Baseurl = "http://localhost:61612";

        public async Task PutAsync<T>(string apiPath, T myObject)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var myContent = JsonConvert.SerializeObject(myObject);

                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");


                HttpResponseMessage Res = await client.PutAsync(apiPath, stringContent);

                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;


                }

            }

        }
        public async Task PostAsync<T>( string apiPath, T myObject)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                     var myContent = JsonConvert.SerializeObject(myObject);
          
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");


                HttpResponseMessage Res = await client.PostAsync(apiPath, stringContent);

                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                   

                }

            }

        }
        public async Task<T> GetAsync<T>(string apiPath)
        {
            T view = default(T);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync(apiPath);

                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    view = JsonConvert.DeserializeObject<T>(Response);

                }

            }
            return view;
        }
    }
}
