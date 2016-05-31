using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Helpers
{
    public class WebHelper
    {
        public static string GetHttpResponse(string url)
        {
            WebRequest req = WebRequest.Create(url);
            var res = req.GetResponse();
            string data;
            using (TextReader tr = new StreamReader(res.GetResponseStream()))
            {
                data = tr.ReadToEnd();
            }

            return data;
        }
    }
}
