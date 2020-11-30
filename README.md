# TwT.HealthChecks.Mqtt
This health check add-on checks the state of a portainer service. The NuGet can be found [here](https://www.nuget.org/packages/TwT.HealthChecks.Portainer/).
============
[![Current Version](https://img.shields.io/badge/version-1.0.0-green.svg)](https://github.com/TimoTielens/TwT.HealthChecks.Portainer)
[![GitHub Issues](https://img.shields.io/github/issues/TimoTielens/TwT.HealthChecks.Mqtt.svg)](https://github.com/TimoTielens/TwT.HealthChecks.Portainer/issues)
[![GitHub Stars](https://img.shields.io/github/stars/TimoTielens/TwT.HealthChecks.Mqtt.svg)](https://github.com/TimoTielens/TwT.HealthChecks.Portainer) 

The `AddPortainer()` method adds a new health check with a specified name and the implementation of type `IHealthCheck`. This is a custom class that implements `IHealthCheck`, which takes a uri to the portainer service as a constructor parameter. This executes a simple query to check if the portainer service is up and running. This is done by contacting the API and asking the version via 'api/status'. It returns `HealthCheckResult.Healthy("Version V x.x.x")` if the query was executed successfully and a `FailureStatus` with the actual exception when it fails.

## Features
- Check if connection to service can be made
- Shows the version of the portainer

## Getting started
Configure the services and add Portainer Check this way:
    
            services
                .AddHealthChecksUI()
                .AddInMemoryStorage()
                .Services
                .AddPortainer(new Uri("http://localhost:9000/"))
                .AddMqtt(client, managedClient);

## Example
Besides the actual implementation this repo also holds an example project that can be used as a playground and test out the application. Do note that you need to provide your own portainer instance to test against.

## License/Copyright
This project is distributed under the Apache license version 2.0 (see the [LICENSE](https://github.com/TimoTielens/TwT.HealthChecks.Portainer/blob/main/LICENSE.txt) file in the project root).

By submitting a pull request to this project, you agree to license your contribution under the Apache license version 2.0 to this project.
