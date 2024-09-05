using Microsoft.Extensions.Logging;
using System.IO;

namespace Lagrange.Desktop.Model;

public class StringWriterLogger : ILogger
{
    private readonly string _name;
    private readonly StringWriter _stringWriter;

    public StringWriterLogger(string name, StringWriter stringWriter)
    {
        _name = name;
        _stringWriter = stringWriter;
    }

    public void Log<TState>(
        Microsoft.Extensions.Logging.LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter
    )
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var logMessage = formatter(state, exception);
        var log = $"{logLevel}: {logMessage}";

        _stringWriter.WriteLine(log);
    }

    public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel) => true;

    public IDisposable BeginScope<TState>(TState state) => null;
}
