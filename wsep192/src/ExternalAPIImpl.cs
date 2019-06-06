
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
        private HttpClient client;
        public ExternalAPIImpl()
        {
            url = "https://cs-bgu-wsep.herokuapp.com/";
            client = new HttpClient();
        }

        public bool connect()
        {
            var values = new Dictionary<string, string>
                {
                    {"action_type","handshake" }
                };
            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;

                // by calling .Result you are synchronously reading the result
                string responseString = responseContent.ReadAsStringAsync().Result;
                if (responseString == "OK")
                    return true;
            }

            return false;

        }
        public int pay(string card_number,string month,string year,string holder,string ccv,string id)
        {

            var values = new Dictionary<string, string>
                {
                    { "action_type", "pay" },
                    { "card_number", card_number },
                    { "month", month },
                    { "year", year},
                    { "holder", holder },
                    { "ccv", ccv },
                    { "id", id }
                };
            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;

                return Int32.Parse(responseString);
            }
            return -1;
        }

        public bool cancel_pay(string transaction_id)
        {

            var values = new Dictionary<string, string>
                {
                    { "action_type", "cancel_pay" },
                    { "transaction_id", transaction_id }
                };
            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;
                if (responseString != "-1")
                    return true;
            }
            return false;
        }

        public int supply(string name, string address, string city, string country, string zip)
        {

            var values = new Dictionary<string, string>
                {
                    { "action_type", "supply" },
                    { "name", name },
                    { "address", address },
                    { "city", city},
                    { "country", country },
                    { "zip", zip },
                };
            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;

                return Int32.Parse(responseString);
            }
            return -1;
        }

        public bool cancel_supply(string transaction_id)
        {

            var values = new Dictionary<string, string>
                {
                    { "action_type", "cancel_supply" },
                    { "transaction_id", transaction_id }
                };
            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;
                if (responseString != "-1")
                    return true;
            }
            return false;
        }


    }

}
