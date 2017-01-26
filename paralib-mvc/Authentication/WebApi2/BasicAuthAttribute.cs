using System;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace com.paralib.Mvc.Authentication.WebApi2
{
    //https://www.asp.net/web-api/overview/security/authentication-filters
    //https://samritchie.net/2014/02/27/basic-auth-with-a-web-api-2-iauthenticationfilter/

    public abstract class BasicAuthAttribute : Attribute, IAuthenticationFilter
    {
        
        public BasicAuthAttribute(string realm)
        {
            Realm = realm;
        }

        public string Realm { get; protected set; }

        protected abstract IPrincipal OnAuthenticate(string user, string password);

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            //we either set Principal (valid credentials) or ErrorResult (invalid credentials), or do nothing (no credentials)

            if (context.Request.Headers.Authorization == null) return;
            if (context.Request.Headers.Authorization.Scheme != "Basic") return;

            if (String.IsNullOrEmpty(context.Request.Headers.Authorization.Parameter))
            {
                //blank credentials
                context.ErrorResult = new UnauthorizedResult("Missing credentials",context.Request);
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
            IPrincipal principal = null;

            if (parts.Length == 2)
            {
                principal=OnAuthenticate(parts[0], parts[1]);
            }

            if (principal==null)
            {
                context.ErrorResult = new UnauthorizedResult("Invalid user or password", context.Request);
            }
            else
            {
                //apparently sets thread too
                context.Principal = principal;
            }

            await Task.CompletedTask;
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            //context.Result will either be the controller or the ErrorResult we set above

            context.Result = new BasicChallengeResult(context.Result, Realm);

            await Task.CompletedTask;

        }


        public virtual bool AllowMultiple
        {
            get { return false; }
        }
    }
}