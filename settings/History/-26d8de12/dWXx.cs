using CommandLine;
using vimrcManager;

public class Program
{
    private static ILog Log = LogManager.GetLogger();
    
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
 
        var parameters = Parser.Default.ParseArguments<Parameters>(args)
            .WithParsed(Parameters.ParsedParameters)
            .WithNotParsed(Parameters.ErrorArguments);

        var appSettings = new AppSettings();
        var t = appSettings.OrderFileName
        switch (parameters.Value.RunMode)
        {
            case RunMode.CollateToFile: 
                CollateToFile(parameters.Value, appSettings); 
                break;
        }
    }

    private static void CollateToFile(Parameters parameters, AppSettings appSettings)
    {
        CollateToFile(parameters.InputDir, parameters.OutputFile, appSettings.OrderFileName);
    }

    private static void CollateToFile(string inputDir, string outputFile, string orderFileName)
    {
        var orderedFiles = new List<string>();

        var filePaths = Directory.GetFiles(inputDir).ToList();
        var fileNames = filePaths.Select(f => f.Split("\\").Last()).ToList();
        
        if (fileNames.FirstOrDefault(f => string.Equals(f, orderFileName)) != null)
        {
            // TODO Make getOrderedFiles return (orderFile, orderedFiles) 
            orderedFiles = GetOrderedFiles(inputDir, orderFileName, filePaths, fileNames);
        }

        var allLines = new List<string>();
        
        foreach (var file in orderedFiles)
        {
            // Log.Info($"Reading file {file}");

            allLines.Add(FileStartEndStringGenerator.GetFileStart(file));
            
            if (file.EndsWith(orderFileName))
                allLines.AddRange(File.ReadAllLines(file).Select(fileName => $"\" {fileName}"));
            else
            	allLines.AddRange(File.ReadAllLines(file));
           
            allLines.Add(FileStartEndStringGenerator.GetFileEnd(file));
            // allLines.Add("\n");
            // allLines.Add(wrappedString);
            // allLines.Add(wrappedString);
            // allLines.Add($"{fileStartPrefix}({file})   {fileEnd}");
            // allLines.Add(wrappedString);
            // allLines.Add(wrappedString);
            // allLines.Add("\n");
        }

        if (File.Exists(outputFile))
            File.Delete(outputFile);
        Log.Info($"Writing {allLines.Count} to file {outputFile}");
        File.WriteAllLines(outputFile, allLines);
    }

    private static List<string> GetOrderedFiles(string inputDir, string orderFileName, 
        List<string> filePaths, List<string> fileNames)
    {
        var orderFilePath = Path.Combine(inputDir, orderFileName);
        var order = File.ReadAllLines(orderFilePath);
        
        var orderedFiles = new List<string>();
        var excludePaths = new List<string>();
        
        // TODO Add Order file wrapper
        orderedFiles.Add($"{inputDir}\\{orderFileName}");

        foreach (var orderFile in order)
        {
            if (orderFile.StartsWith("-"))
            {
                excludePaths.Add(filePaths[fileNames.IndexOf(orderFile[1..])]);
                continue;
            }

            if (fileNames.Contains(orderFile))
                orderedFiles.Add(filePaths[fileNames.IndexOf(orderFile)]);
        }

        var unOrderedFiles = filePaths.Where(fp => !orderedFiles.Contains(fp) 
                                                   && !fp.EndsWith(orderFileName)
                                                   && !excludePaths.Contains(fp)).ToList();       orderedFiles.AddRange(unOrderedFiles);
        return orderedFiles;
    }
}