namespace MunCode.Core.Logging
{
    using System;
    using System.Reflection;

    using Microsoft.Extensions.Logging;

    public static class LoggerExtensions
    {
        public static void LogAppStartUp(this ILogger logger)
        {
            var assembly = Assembly.GetEntryAssembly().GetName();
            logger.LogInformation($"Application {assembly.Name} version {assembly.Version} has started.");
        }

        public static void LogAppTermination(this ILogger logger)
        {
            var assembly = Assembly.GetEntryAssembly().GetName();
            logger.LogInformation($"Application {assembly.Name} version {assembly.Version} has terminated.");
        }

        public static void LogException(this ILogger logger, Exception e)
        {
            logger.LogError("Exception has occurred.", e);
        }

        public static void LogUnhandledException(this ILogger logger, Exception e)
        {
            logger.LogCritical("Unhandled exception has occurred.", e);
        }
    }
}