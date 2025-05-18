#region

using System.IO;
using System.Net;

#endregion

namespace cope
{
    public static class WebHelper
    {
        public static string SendData(string url, string data)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.GetRequestStream().Write(data.ToByteArray(true), 0, data.Length);
            var response = request.GetResponse() as HttpWebResponse;
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        public static string SendData(string url, byte[] data)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.GetRequestStream().Write(data, 0, data.Length);
            var response = request.GetResponse() as HttpWebResponse;
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        public static string SendData(string url, params byte[][] data)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            foreach (byte[] b in data)
                request.GetRequestStream().Write(b, 0, b.Length);
            var response = request.GetResponse() as HttpWebResponse;
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}