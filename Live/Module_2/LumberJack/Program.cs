using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace LumberJack;

internal class Program
{
    static void Main(string[] args)
    {
        var file = "appsettings.json";
        ConfigurationBuilder bld = new ConfigurationBuilder();
        bld.AddJsonFile(file, true, true);
        var app = bld.Build();

        //LoggingBuilder bld = new Microsoft.Extensions.Logging.LoggingBuilder()
        ILoggerFactory factory = LoggerFactory.Create(config=> {
            config.AddConfiguration(app.GetSection("Logging"));
            //config.AddFilter((src, lvl) => {
            //    return lvl >= LogLevel.Trace && src == "bla";
            //});
            config.AddConsole();
        });


        ILogger<Program> writer = factory.CreateLogger<Program>();
        writer.LogTrace("Treees");
        writer.LogDebug("Debug");
        writer.LogInformation("Information");
        writer.LogWarning("Warning");
        writer.LogError("Error");
        writer.LogCritical("Kritiek");

        ILogger<Dummy> writer3 = factory.CreateLogger<Dummy>();
        writer3.LogTrace("Treees");
        writer3.LogDebug("Debug");
        writer3.LogInformation("Information");
        writer3.LogWarning("Warning");
        writer3.LogError("Error");
        writer3.LogCritical("Kritiek");
    }
}
