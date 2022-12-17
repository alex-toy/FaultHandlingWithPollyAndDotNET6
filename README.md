# Fault Handling With Polly And .NET 6

In this project, we will work with the Polly library to ensure we can handle any transient faults that may occur when one service calls the other. Transient faults are likely to occur for a short period of time and maybe disappear by themselves. For example :

- A network connection may be temporarily unavailable while a router reboots,
- Microservices unavailable while starting up,
- Server refusing connections due to connexion pool exhaustion.

We could very possibly eventually get a good response if we try later, which is better than abandon and accept failure. This is particularly advantageous in distributed microservice architectures. In this project, we will focus on the retry policy.

## Create project ResponseService

<img src="/pictures/response_service.png" title="response service"  width="800">


## Create project RequestService

### Add Nuget Packages RequestService
```
Install-Package Microsoft.Extensions.Http.Polly
```