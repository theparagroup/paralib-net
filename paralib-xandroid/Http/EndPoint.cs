using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Http
{
    public class EndPoint<T>
    {
        private static HttpClient _httpClient;
        public virtual string Host {get; set;}
        public virtual string Path { get; set; }
        public virtual HttpMethods Method { get; set; }
        public virtual bool Secure { get; set; }
        public virtual string User { get; set; }
        public virtual string Password { get; set; }
        public virtual TimeSpan Timeout { get; set; }
        public virtual int ResponseCode { get; protected set; }
        public virtual T Response { get; protected set; }
        public virtual string ResponseJson { get; protected set; }

        protected static HttpClient HttpClient
        {
            get
            {
                if (_httpClient==null)
                {
                    _httpClient = new HttpClient();
                }

                return _httpClient;
            }

        }

        protected virtual bool OnUnauthorized()
        {
            return false;
        }

        public EndPoint()
        {

        }

        public EndPoint(string host, string path, HttpMethods method=HttpMethods.GET, bool secure=false)
        {
            Host = host;
            Path = path;
            Method = method;
            Secure = secure;

            Call();

        }

        public void Call()
        {
            var byteArray = Encoding.ASCII.GetBytes($"{User}:{Password}");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            Uri uri = new Uri($"http{(Secure ? "s" : "")}://{Host}/{Path}");
            HttpClient.Timeout = Timeout;

            HttpResponseMessage response = null;

            if (Method == HttpMethods.GET)
            {
                response = HttpClient.GetAsync(uri).Result;
            }
            else if (Method == HttpMethods.PUT)
            {
                response = HttpClient.PutAsync(uri,null).Result;
            }
            else
            {
                throw new NotImplementedException();
            }

            ResponseCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                //call sync
                var responseContent = response.Content;
                ResponseJson = responseContent.ReadAsStringAsync().Result;

                Response = JsonConvert.DeserializeObject<T>(ResponseJson);

            }

            if (ResponseCode==401)
            {
                OnUnauthorized();
            }

        }

    }
}
