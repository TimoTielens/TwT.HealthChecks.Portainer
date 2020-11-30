using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Diagnostics.HealthChecks;

using Newtonsoft.Json;

namespace TwT.HealthChecks.Portainer
{
    /// <summary>
    /// Represents a health check, which can be used to check the status of the portainer service.
    /// </summary>
    public class PortainerHealthCheck : IHealthCheck
    {
        /// <summary>
        /// Uri that will be used to connect to portainer. E.g. 'http://localhost:9000/'
        /// </summary>
        public Uri UriToPortainer { get; private set; }

        /// <summary>
        /// Represents a health check, which can be used to check the status of the portainer service.
        /// </summary>
        /// <param name="uriToPortainer">Uri that will be used to connect to portainer. E.g. 'http://localhost:9000/'</param>
        public PortainerHealthCheck(Uri uriToPortainer)
        {
            UriToPortainer = uriToPortainer;
        }

        /// <summary>
        /// Check if the portainer services is up by contacting the API and asking the status
        /// </summary>
        /// <param name="context">A context object associated with the current execution.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the health check.</param>
        /// <returns>A <see cref="Task"/> that completes when the health check has finished, yielding the status of the component being checked.</returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                return HealthCheckResult.Healthy("Version V" + (await GetStatus()).Version);
            }
            catch (Exception e)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, "Portainer service is unavailable: " + e.Message);
            }
        }

        /// <summary>
        /// Contact Portainer via the API and get the status/Version.
        /// </summary>
        /// <returns>StatusModel that holds the version of the portainer service.</returns>
        private  async Task<StatusModel> GetStatus()
        {
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, new Uri(UriToPortainer, "/api/status") );
            request.Headers.TryAddWithoutValidation("accept", "application/json");

            return JsonConvert.DeserializeObject<StatusModel>(await (await httpClient.SendAsync(request)).Content.ReadAsStringAsync());
        }
    }
}
