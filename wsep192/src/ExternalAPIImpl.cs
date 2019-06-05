
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace src.Domain
{
    class ExternalAPIImpl
    {
        private string url;
        public ExternalAPIImpl()
        {
            url = "https://cs-bgu-wsep.herokuapp.com/";
        }

        public bool connect()
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<String,String>("action_type","handshake")
                    });
                var response = client.PostAsync(url,content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;

                    Console.WriteLine(responseString);
                }
            }
            return false;

        }
        public bool pay()
        {
            using (var client = new HttpClient())
            {   
                var content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<String,String>("action_type","pay"),
                    new KeyValuePair<String,String>("card_number","10"),
                    new KeyValuePair<String,String>("month","10"),
                    new KeyValuePair<String,String>("year","2019"),
                    new KeyValuePair<String,String>("holder","aviv"),
                    new KeyValuePair<String,String>("cvv","140"),
                    new KeyValuePair<String,String>("id","1000"),
                    });
                var response = client.PostAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;

                    Console.WriteLine(responseString);
                }
            }
            return false;

        }
    }
}
