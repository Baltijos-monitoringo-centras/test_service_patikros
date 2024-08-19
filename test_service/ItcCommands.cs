using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Xml;

namespace test_service
{

    class ItcCommands
    {
        public string phoneNr;



        public string itcStatus(string phone)
        {
            //Token();
            phone.Replace("+", "");
            phone.Replace(" ", "");
            Console.WriteLine(phone);
            phoneNr = phone;

            return Nce("https://api.1nce.com/management-api/v1/sims/8988" + phone + "/connectivity_info?subscriber=true&ussd=false");


        }



        public string getLocation(string phone)
        {


            string s = Nce("https://api.1nce.com/management-api/v1/sims/8988" + phone + "/connectivity_info?subscriber=true&ussd=false");
            Console.WriteLine(s);
            return (s);

        }





        public string Get(string uri)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.UserAgent = "1nceLocator/1.0";
            request.Accept = "application/json";
            request.ContentType = "application/x-www-form-urlencoded";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                //return reader.ReadToEnd();
                return reader.ReadToEnd();
            }
        }

        public string Token()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.1nce.com/management-api/oauth/token");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("Authorization", "Basic ODEwMTQzODFfMzpEU2ZvVUNUcGpFaTRqMENH");
            request.UserAgent = "1nceLocator/1.0";
            request.Accept = "application/json";
             
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                Console.WriteLine("puciam");
                streamWriter.Write("grant_type=client_credentials");
                streamWriter.Flush();
                streamWriter.Close();
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string s = reader.ReadToEnd();

                JObject joResponse = JObject.Parse(s);
                string apitoken = (string)joResponse["access_token"];
                //Console.WriteLine("va tokenas: " + apitoken);


                return apitoken;
            }
            
        }




        public string Nce(string uri)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            string s = "";
            try
            {

                request.Method = "GET";

                request.Headers.Add("Authorization", "Bearer " + Token());
                request.Accept = "application/json";
                request.UserAgent = "1nceLocator/1.0";
                request.ContentType = "application/x-www-form-urlencoded";


                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    s = reader.ReadToEnd();

                    Console.WriteLine(s);
                    s = (NceLoc(s));


                }
            }
            catch (Exception ex) { if (ex.Message.Contains("403")) s = "No data from Mobile Operator, please contact Operating Center."; }
 return s;
        }



        public string NceLoc(string s)
        {
            try
            {
                JObject joResponse = JObject.Parse(s);


                string s1 = ((string)joResponse["subscriber_info"].ToString());
                JObject joResponse1 = JObject.Parse(s1);
                string s2 = ((string)joResponse1["location"].ToString());
                JObject joResponse2 = JObject.Parse(s2);
                string age = (string)joResponse2["age_of_location"];
                string s3 = ((string)joResponse2["cell_global_id"].ToString());
                JObject joResponse3 = JObject.Parse(s3);
                string mcc = (string)joResponse3["mcc"].ToString();
                string mnc = (string)joResponse3["mnc"].ToString();
                string lac = (string)joResponse3["lac"].ToString();
                string cid = (string)joResponse3["cid"].ToString();


                return age;

            }
            catch { return "Device is not working, please contact Operating Center"; }
        }

    }
}