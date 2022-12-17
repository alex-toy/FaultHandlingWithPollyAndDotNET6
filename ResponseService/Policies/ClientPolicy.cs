using Polly;
using Polly.Retry;

namespace Policies
{
    public class ClientPolicy
    {
        public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; set; }
        public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; set; }
        public AsyncRetryPolicy<HttpResponseMessage> ExponentialBackOffHttpRetry { get; set; }

        public ClientPolicy()
        {
            ImmediateHttpRetry = Policy
                .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .RetryAsync(10);

            LinearHttpRetry = Policy
                .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(3));

            ExponentialBackOffHttpRetry = Policy
                .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
