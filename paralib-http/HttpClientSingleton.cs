using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace com.paralib.Http
{
    public class HttpClientSingleton
    {
        private static HttpClient _httpClient;
        private static readonly object _lock = new object();
        private static bool _init;

        public static HttpClient Instance
        {
            get
            {
                if (_httpClient == null)
                {
                    lock(_lock)
                    {
                        if (_httpClient == null)
                        {
                            _httpClient = new HttpClient();
                        }
                    }
                }

                return _httpClient;
            }

        }

        public static void Reset()
        {
            //should only be called from android/mono
            _init = false;
        }

        public static void Init(TimeSpan timeout, byte[] authentication)
        {

            if (!_init)
            {
                /*
                    on windows, the timeout can only be specified once per singleton
                    on android (mono), it can be set multiple times

                    on android, we cannot assume our singleton won't get destroyed "randomly"
                    (not sure what causes this behavior)
                */


                Instance.Timeout = timeout;

                Instance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authentication));


                _init = true;
            }
        }


    }
}
