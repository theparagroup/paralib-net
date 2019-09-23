using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace com.paralib.Http
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
        public virtual T Content { get; set; }
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
                if (Content!=null)
                {
                    throw new Exception("Can't send content on GET operation");
                }

                response = HttpClient.GetAsync(uri).Result;
            }
            else if (Method == HttpMethods.PUT || Method == HttpMethods.POST)
            {
                //TODO which http methods send content? all except for GET?

                string jsonContent = Json.Serialize(Content);
                HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                if (Method == HttpMethods.PUT)
                {
                    response = HttpClient.PutAsync(uri, httpContent).Result;
                }
                else if (Method == HttpMethods.POST)
                {
                    response = HttpClient.PostAsync(uri, httpContent).Result;
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            ResponseCode = (int)response.StatusCode;
            ResponseJson = null;
            Response = default(T);

            if (response.IsSuccessStatusCode)
            {
                //call sync
                var responseContent = response.Content;
                ResponseJson = responseContent.ReadAsStringAsync().Result;

                //TODO which methods can return a response?

                //if (Method == HttpMethods.GET)
                //{
                //    Response = Json.DeSerialize<T>(ResponseJson);
                //}

                //let's say all of them
                Response = Json.DeSerialize<T>(ResponseJson);

            }

            if (ResponseCode==401)
            {
                OnUnauthorized();
            }

        }

    }
}
