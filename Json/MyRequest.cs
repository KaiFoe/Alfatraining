using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Foundation;
using Newtonsoft.Json;
using UIKit;

namespace Json
{
    public class MyRequest
    {

        public MyRequest()
        {
        }

        public void getRequest(string param)
        {
            var request = WebRequest.Create(string.Format(@"https://rxnav.nlm.nih.gov/REST/RxTerms/rxcui/{0}/allinfo", param));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    Console.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                        Console.WriteLine("Response contained empty body");
                    else
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(content);
                        string json = JsonConvert.SerializeXmlNode(xmlDoc);

                        
                        Console.WriteLine("Response body: \r\n {0}", json);
                    }
                }
            }
        }
    }
}