using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureBLL
{
    public class HttpClass
    {
        public string GetCoreDapper(string requestUrl)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            StringBuilder sbResponse = new StringBuilder();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            request.Timeout = 200 * 1000;

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    stream = new GZipStream(stream, CompressionMode.Decompress);
                }
                else if (response.ContentEncoding.ToLower().Contains("deflate"))
                {
                    stream = new DeflateStream(stream, CompressionMode.Decompress);
                }

                using (BufferedStream bs = new BufferedStream(stream))
                {
                    using (StreamReader streader = new StreamReader(bs))
                    {
                        sbResponse.Append(streader.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return sbResponse.ToString();
        }
    }
}
