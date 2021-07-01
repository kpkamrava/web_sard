using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace web_lib
{
    public class sms
    {
        public string username { get; set; }
        public string password { get; set; }
        public string numfrom { get; set; }
        public async System.Threading.Tasks.Task<string> sendsmsAsync(string num, string msg, CancellationToken stoppingToken = default)
        {
            //https://localhost:44312/api/send?username=09143429614&password=31169614&from=10009611&to=09143429614&msg=%D8%AA%D8%B3%D8%AA%D8%AA%D8%B3%D8%AA

            var postData = "?username=" + username;
            postData += "&password=" + password;


            postData += "&from=" + numfrom;
            postData += "&to=" + num;
            postData += "&msg=" + System.Web.HttpUtility.UrlEncode(msg);
            string result = "";
            using (var client = new HttpClient(new HttpClientHandler { /*AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate */}))
            {
                client.BaseAddress = new Uri("http://sms.parianet.ir/api/send");


             //   postData= postData);
                HttpResponseMessage response = await client.GetAsync(postData, stoppingToken);
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
            }

            //    var postData = new FormUrlEncodedContent(new[]
            //{
            //         new KeyValuePair<string, string>("username", username),
            //         new KeyValuePair<string, string>("password", password),
            //         new KeyValuePair<string, string>("from", numfrom),
            //         new KeyValuePair<string, string>("to", num),
            //         new KeyValuePair<string, string>("msg", msg)
            //    });

            //    //"?username=" + username;
            //    //postData += "&password=" + password;


            //    //postData += "&from=" + numfrom;
            //    //postData += "&to=" + num;
            //    //postData += "&msg=" + msg;
            //    string result = "";
            //    using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            //    {
            //        client.BaseAddress = new Uri("http://sms.sror.ir/api/send");
            //        HttpResponseMessage response = await client.PostAsync("", postData, stoppingToken);
            //        response.EnsureSuccessStatusCode();
            //        result = await response.Content.ReadAsStringAsync();
            //    }


            return result;

        }
    }
}
