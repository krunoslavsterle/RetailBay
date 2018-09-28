using System;

namespace RetailBay.Core.Interfaces
{
    public interface IAppLogger<T>
    {
        /// <summary>
        /// Logs the trace message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        void LogTrace(string message, params object[] args);

        /// <summary>
        /// Logs the warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        void LogWarning(string message, params object[] args);

        /// <summary>
        /// Logs the information message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        void LogInformation(string message, params object[] args);

        /// <summary>
        /// Logs the error message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        void LogError(Exception exception, string message, params object[] args);

        /// <summary>
        /// Logs the debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        void LogDebug(string message, params object[] args);

        /// <summary>
        /// Logs the critical message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        void LogCritical(string message, params object[] args);
    }
}
