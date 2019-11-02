using System;
using System.Net.Http;
using System.Text;

namespace com.paralib.Http
{
    public class EndPoint<T>
    {
        public virtual string Host { get; set; }
        public virtual string Path { get; set; }
        public virtual HttpMethods Method { get; set; }
        public virtual bool Secure { get; set; }
        public virtual string User { get; set; }
        public virtual string Password { get; set; }
        public virtual TimeSpan Timeout { get; set; }
        public virtual T Content { get; set; }
        public virtual int ResponseCode { get; protected set; }
        public virtual string ReasonPhrase { get; protected set; }
        public virtual T Response { get; protected set; }
        public virtual string RawResponse { get; protected set; }


        protected virtual bool OnUnauthorized()
        {
            return false;
        }

        public EndPoint()
        {

        }

        public EndPoint(string host, string path, HttpMethods method = HttpMethods.GET, bool secure = false)
        {
            Host = host;
            Path = path;
            Method = method;
            Secure = secure;

            Call();

        }

        public void Call()
        {
            //initialize singleton, if not already initialized
            HttpClientSingleton.Init(Timeout, Encoding.ASCII.GetBytes($"{User}:{Password}"));

            Uri uri = new Uri($"http{(Secure ? "s" : "")}://{Host}/{Path}");

            HttpResponseMessage response = null;

            if (Method == HttpMethods.GET)
            {
                if (Content != null)
                {
                    throw new Exception("Can't send content on GET operation");
                }

                response = HttpClientSingleton.Instance.GetAsync(uri).Result;
            }
            else if (Method == HttpMethods.PUT || Method == HttpMethods.POST)
            {
                //TODO which http methods send content? all except for GET?

                string jsonContent = Json.Serialize(Content);
                HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                if (Method == HttpMethods.PUT)
                {
                    response = HttpClientSingleton.Instance.PutAsync(uri, httpContent).Result;
                }
                else if (Method == HttpMethods.POST)
                {
                    response = HttpClientSingleton.Instance.PostAsync(uri, httpContent).Result;
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            ResponseCode = (int)response.StatusCode;
            ReasonPhrase = response.ReasonPhrase;
            RawResponse = response.Content.ReadAsStringAsync().Result;
            Response = default(T);

            if (response.IsSuccessStatusCode)
            {
                Response = Json.DeSerialize<T>(RawResponse);
            }
            else
            {
                if (ResponseCode == 401)
                {
                    OnUnauthorized();
                }
            }

        }

    }
}
