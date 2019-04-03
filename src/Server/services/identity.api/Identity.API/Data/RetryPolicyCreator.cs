using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Identity.API.Data
{
    public class RetryPolicyCreator
    {
        public static AsyncRetryPolicy CreatePolicy(ILogger logger, string prefix, int retries = 3)
		{
			return Policy.Handle<SqlException>().
				WaitAndRetryAsync(
					retryCount: retries,
					sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
					onRetry: (exception, timeSpan, retry, ctx) =>
					{
						logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
					}
				);
		}
    }
}