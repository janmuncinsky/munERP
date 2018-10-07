namespace MunCode.Core.Logging
{
    using System;

    using global::EasyNetQ.Logging;

    using Microsoft.Extensions.Logging;

    using MunCode.Core.Guards;

    public class LoggerAdapterLogProvider : ILogProvider
    {
        private readonly ILoggerFactory factory;

        public LoggerAdapterLogProvider(ILoggerFactory factory)
        {
            Guard.NotNull(factory, nameof(factory));
            this.factory = factory;
        }

        public Logger GetLogger(string name)
        {
            this.factory.CreateLogger(name);
            var logger = this.factory.CreateLogger(name);

            bool LoggerDelegate(global::EasyNetQ.Logging.LogLevel logLevel, Func<string> messageFunc, Exception exception, object[] formatParameters)
            {
                Microsoft.Extensions.Logging.LogLevel internalLogLevel;

                switch (logLevel)
                {
                    case global::EasyNetQ.Logging.LogLevel.Trace:
                        internalLogLevel = Microsoft.Extensions.Logging.LogLevel.Trace;
                        break;
                    case global::EasyNetQ.Logging.LogLevel.Debug:
                        internalLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug;
                        break;
                    case global::EasyNetQ.Logging.LogLevel.Info:
                        internalLogLevel = Microsoft.Extensions.Logging.LogLevel.Information;
                        break;
                    case global::EasyNetQ.Logging.LogLevel.Warn:
                        internalLogLevel = Microsoft.Extensions.Logging.LogLevel.Warning;
                        break;
                    case global::EasyNetQ.Logging.LogLevel.Error:
                        internalLogLevel = Microsoft.Extensions.Logging.LogLevel.Error;
                        break;
                    case global::EasyNetQ.Logging.LogLevel.Fatal:
                        internalLogLevel = Microsoft.Extensions.Logging.LogLevel.Critical;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
                }

                if (messageFunc == null)
                {
                    return logger.IsEnabled(internalLogLevel);
                }

                logger.Log(internalLogLevel, exception, messageFunc(), formatParameters);
                return true;
            }

            return LoggerDelegate;
        }

        public IDisposable OpenNestedContext(string message)
        {
            return new EmptyDisposable();
        }

        public IDisposable OpenMappedContext(string key, object value, bool destructure = false)
        {
            return new EmptyDisposable();
        }

        private class EmptyDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}