using System;
using Microsoft.Extensions.Logging;
using RetailBay.Core.Entities.SystemDb;
using RetailBay.Core.Interfaces;

namespace RetailBay.Infrastructure.Logging
{
    /// <summary>
    /// LoggerAdapter implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="RetailBay.Core.Interfaces.IAppLogger{T}" />
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;
        private readonly Tenant _tenant;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerAdapter{T}"/> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="tenant">The tenant.</param>
        public LoggerAdapter(ILoggerFactory loggerFactory, Tenant tenant)
        {
            _logger = loggerFactory.CreateLogger<T>();
            _tenant = tenant;
        }

        /// <summary>
        /// Logs the critical message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void LogCritical(string message, params object[] args)
        {
            _logger.LogCritical(ConstructMessage(message), args);
        }

        /// <summary>
        /// Logs the debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void LogDebug(string message, params object[] args)
        {
            _logger.LogDebug(ConstructMessage(message), args);
        }

        /// <summary>
        /// Logs the error message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void LogError(Exception exception, string message, params object[] args)
        {
            _logger.LogError(0, exception, ConstructMessage(message), args);
        }

        /// <summary>
        /// Logs the information message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(ConstructMessage(message), args);
        }

        /// <summary>
        /// Logs the trace message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void LogTrace(string message, params object[] args)
        {
            _logger.LogTrace(ConstructMessage(message), args);
        }

        /// <summary>
        /// Logs the warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(ConstructMessage(message), args);
        }

        private string ConstructMessage(string message)
        {
            return $"[{DateTime.UtcNow}] Tenant: [{_tenant.Id}] - {message}";
        }
    }
}
