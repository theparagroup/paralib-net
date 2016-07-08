using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Linq;

namespace com.paralib.Mvc.Authentication.WebApi2
{

    public class BasicAuthAttribute : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }


        private UnauthorizedResult Unauthorized(HttpRequestMessage request)
        {
            return new UnauthorizedResult(null, request);
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            //we either set Principal or ErrorResult


            if (context.Request.Headers.Authorization == null) return;
            if (context.Request.Headers.Authorization.Scheme != "Basic") return;

            if (String.IsNullOrEmpty(context.Request.Headers.Authorization.Parameter))
            {
                //blank credentials
                context.ErrorResult = Unauthorized(context.Request);
                return;
            }

            //https://username:password@www.example.com/index.html
            //or
            //username:password => Base64() =QWxhZGRpbjpPcGVuU2VzYW1l
            //Authorization: Basic QWxhZGRpbjpPcGVuU2VzYW1l

            byte[] credentialBytes = Convert.FromBase64String(context.Request.Headers.Authorization.Parameter);
            string credentials = Encoding.ASCII.GetString(credentialBytes);
            string[] parts = credentials.Split(':');

            //authenticate here

            if (true)
            {
                context.Principal = new GenericPrincipal(new GenericIdentity("user"), new string[] { });
            }
            else
            {
                context.ErrorResult = Unauthorized(context.Request);
            }

            await Nothing();
        }

        public Task Nothing()
        {
            return Task.CompletedTask;
        }




        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {


            HttpResponseMessage response = await context.Result.ExecuteAsync(cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Only add one challenge per authentication scheme?
                if (!response.Headers.WwwAuthenticate.Any((h) => h.Scheme == "Basic"))
                {
                    //WWW-Authenticate: Basic realm="myRealm"
                    response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic", "realm=The Realm"));
                }
            }

        }

        public virtual bool AllowMultiple
        {
            get { return false; }
        }
    }
}