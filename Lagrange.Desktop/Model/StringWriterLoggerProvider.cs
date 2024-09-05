using Microsoft.Extensions.Logging;
using System.IO;

namespace Lagrange.Desktop.Model;

public class StringWriterLoggerProvider : ILoggerProvider
{
    private readonly StringWriter _stringWriter;

    public StringWriterLoggerProvider(StringWriter stringWriter)
    {
        _stringWriter = stringWriter;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new StringWriterLogger(categoryName, _stringWriter);
    }

    public void Dispose() { }
}