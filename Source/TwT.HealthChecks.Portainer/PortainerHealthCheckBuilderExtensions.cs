using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using System;
using System.Collections.Generic;

namespace TwT.HealthChecks.Portainer
{
    /// <summary>
    /// Health check for Portainer services 
    /// </summary>
    public static class PortainerHealthCheckBuilderExtensions
    {
        /// <summary>
        /// Add a health check for Portainer services 
        /// </summary>
        /// <param name="builder">The <see cref="IHealthChecksBuilder"/>.</param>
        /// <param name="uriToPortainer">Uri that will be used to connect to portainer. E.g. 'http://localhost:9000/' </param>
        /// <param name="name">The health check name. Optional. If <c>null</c> the type name 'Portainer' will be used for the name.</param>
        /// <param name="failureStatus">The <see cref="HealthStatus"/> that should be reported when the health check fails. Optional. If <c>null</c> then
        /// the default status of <see cref="HealthStatus.Unhealthy"/> will be reported.</param>
        /// <param name="tags">A list of tags that can be used to filter sets of health checks. Optional.</param>
        /// <param name="timeout">An optional System.TimeSpan representing the timeout of the check.</param>
        /// <returns>The <see cref="IHealthChecksBuilder"/>.</returns>
        public static IHealthChecksBuilder AddPortainer(this IHealthChecksBuilder builder, Uri uriToPortainer, string name = "Portainer", HealthStatus failureStatus = HealthStatus.Unhealthy, IEnumerable<string> tags = default, TimeSpan? timeout = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            builder.Services.AddSingleton(sp => new PortainerHealthCheck(uriToPortainer));

            return builder.Add(new HealthCheckRegistration(name, sp => sp.GetRequiredService<PortainerHealthCheck>(),
                failureStatus,
                tags,
                timeout));
        }
    }
}