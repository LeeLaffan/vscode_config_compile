using CommandLine;

namespace AppSwitchOrOpen;

class Program
{
    private readonly ILog _log = Log.GetLogger();
    
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        
        var parameters = Parser.Default.ParseArguments<Parameters>(args)
            .WithParsed(Parameters.ParsedParameters)
            .WithNotParsed(Parameters.ErrorArguments);
    }
}
