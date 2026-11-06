namespace ExternalServices.Clients.OpenAi
{
    using System;
    using System.ClientModel.Primitives;

    /// <summary>
    /// The default client retry policy, except that a request that timed out is retried at most
    /// <c>maxTimeoutRetries</c> times. A timed-out generation is still billed by the API, and
    /// retrying it re-runs (and re-bills) the whole generation, so timeouts get fewer retries
    /// than cheap transient failures (429s, 5xx, dropped connections).
    /// </summary>
    internal sealed class TimeoutAwareRetryPolicy : ClientRetryPolicy
    {
        private readonly int maxTimeoutRetries;

        public TimeoutAwareRetryPolicy(int maxRetries, int maxTimeoutRetries)
            : base(maxRetries)
        {
            this.maxTimeoutRetries = maxTimeoutRetries;
        }

        // Only the sync hook is overridden: the base ShouldRetryAsync delegates to it, so overriding
        // both would count each timeout twice.
        protected override bool ShouldRetry(PipelineMessage message, Exception? exception)
        {
            return this.AllowTimeoutRetry(message, exception) && base.ShouldRetry(message, exception);
        }

        private bool AllowTimeoutRetry(PipelineMessage message, Exception? exception)
        {
            // A network timeout surfaces as a cancellation the caller didn't ask for.
            if (exception is not OperationCanceledException || message.CancellationToken.IsCancellationRequested)
            {
                return true;
            }

            message.TryGetProperty(typeof(TimeoutRetryCount), out var value);
            var timeoutRetries = value is int count ? count : 0;
            if (timeoutRetries >= this.maxTimeoutRetries)
            {
                return false;
            }

            message.SetProperty(typeof(TimeoutRetryCount), timeoutRetries + 1);
            return true;
        }

        /// <summary>Key for the per-request timeout retry count stored on the message.</summary>
        private sealed class TimeoutRetryCount
        {
        }
    }
}
