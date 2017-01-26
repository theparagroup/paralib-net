using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.paralib.Mvc.Authentication.WebApi2
{
    public class BasicChallengeResult : IHttpActionResult
    {
        private IHttpActionResult _innerResult;
        private string _realm;

        public BasicChallengeResult(IHttpActionResult innerResult, string realm)
        {
            _innerResult = innerResult;
            _realm = realm;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await _innerResult.ExecuteAsync(cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic", $"realm=\"{_realm}\""));
            }

            return response;
        }
    }
}
