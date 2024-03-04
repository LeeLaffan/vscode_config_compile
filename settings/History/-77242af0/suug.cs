#region Assembly CommandLine, Version=2.9.1.0, Culture=neutral, PublicKeyToken=5a870481e358d379
// C:\Users\l\.nuget\packages\commandlineparser\2.9.1\lib\netstandard2.0\CommandLine.dll
// Decompiled with ICSharpCode.Decompiler 8.1.1.7464
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine.Core;
using CommandLine.Text;
using CSharpx;
using RailwaySharp.ErrorHandling;

namespace CommandLine;

//
// Summary:
//     Provides methods to parse command line arguments.
public class Parser : IDisposable
{
    private bool disposed;

    private readonly ParserSettings settings;

    private static readonly Lazy<Parser> DefaultParser = new Lazy<Parser>(() => new Parser(new ParserSettings
    {
        HelpWriter = Console.Error
    }));

    //
    // Summary:
    //     Gets the singleton instance created with basic defaults.
    public static Parser Default => DefaultParser.Value;

    //
    // Summary:
    //     Gets the instance that implements CommandLine.ParserSettings in use.
    public ParserSettings Settings => settings;

    //
    // Summary:
    //     Initializes a new instance of the CommandLine.Parser class.
    public Parser()
    {
        settings = new ParserSettings
        {
            Consumed = true
        };
    }

    //
    // Summary:
    //     Initializes a new instance of the CommandLine.Parser class, configurable with
    //     CommandLine.ParserSettings using a delegate.
    //
    // Parameters:
    //   configuration:
    //     The System.Action`1 delegate used to configure aspects and behaviors of the parser.
    public Parser(Action<ParserSettings> configuration)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException("configuration");
        }

        settings = new ParserSettings();
        configuration(settings);
        settings.Consumed = true;
    }

    internal Parser(ParserSettings settings)
    {
        this.settings = settings;
        this.settings.Consumed = true;
    }

    //
    // Summary:
    //     Finalizes an instance of the CommandLine.Parser class.
    ~Parser()
    {
        Dispose(disposing: false);
    }
    

    //
    // Summary:
    //     Parses a string array of command line arguments constructing values in an instance
    //     of type T. Grammar rules are defined decorating public properties with appropriate
    //     attributes.
    //
    // Parameters:
    //   args:
    //     A System.String array of command line arguments, normally supplied by application
    //     entry point.
    //
    // Type parameters:
    //   T:
    //     Type of the target instance built with parsed value.
    //
    // Returns:
    //     A CommandLine.ParserResult`1 containing an instance of type T with parsed values
    //     and a sequence of CommandLine.Error.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     Thrown if one or more arguments are null.
    public ParserResult<T> ParseArguments<T>(IEnumerable<string> args)
    {
        if (args == null)
        {
            throw new ArgumentNullException("args");
        }

        return MakeParserResult(InstanceBuilder.Build(typeof(T).IsMutable() ? Maybe.Just<Func<T>>(Activator.CreateInstance<T>) : Maybe.Nothing<Func<T>>(), (IEnumerable<string> arguments, IEnumerable<OptionSpecification> optionSpecs) => Tokenize(arguments, optionSpecs, settings), args, settings.NameComparer, settings.CaseInsensitiveEnumValues, settings.ParsingCulture, settings.AutoHelp, settings.AutoVersion, settings.AllowMultiInstance, HandleUnknownArguments(settings.IgnoreUnknownArguments)), settings);
    }

    //
    // Summary:
    //     Parses a string array of command line arguments constructing values in an instance
    //     of type T. Grammar rules are defined decorating public properties with appropriate
    //     attributes.
    //
    // Parameters:
    //   factory:
    //     A System.Func`1 delegate used to initialize the target instance.
    //
    //   args:
    //     A System.String array of command line arguments, normally supplied by application
    //     entry point.
    //
    // Type parameters:
    //   T:
    //     Type of the target instance built with parsed value.
    //
    // Returns:
    //     A CommandLine.ParserResult`1 containing an instance of type T with parsed values
    //     and a sequence of CommandLine.Error.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     Thrown if one or more arguments are null.
    public ParserResult<T> ParseArguments<T>(Func<T> factory, IEnumerable<string> args)
    {
        if (factory == null)
        {
            throw new ArgumentNullException("factory");
        }

        if (!typeof(T).IsMutable())
        {
            throw new ArgumentException("factory");
        }

        if (args == null)
        {
            throw new ArgumentNullException("args");
        }

        return MakeParserResult(InstanceBuilder.Build(Maybe.Just(factory), (IEnumerable<string> arguments, IEnumerable<OptionSpecification> optionSpecs) => Tokenize(arguments, optionSpecs, settings), args, settings.NameComparer, settings.CaseInsensitiveEnumValues, settings.ParsingCulture, settings.AutoHelp, settings.AutoVersion, settings.AllowMultiInstance, HandleUnknownArguments(settings.IgnoreUnknownArguments)), settings);
    }

    //
    // Summary:
    //     Parses a string array of command line arguments for verb commands scenario, constructing
    //     the proper instance from the array of types supplied by types. Grammar rules
    //     are defined decorating public properties with appropriate attributes. The CommandLine.VerbAttribute
    //     must be applied to types in the array.
    //
    // Parameters:
    //   args:
    //     A System.String array of command line arguments, normally supplied by application
    //     entry point.
    //
    //   types:
    //     A System.Type array used to supply verb alternatives.
    //
    // Returns:
    //     A CommandLine.ParserResult`1 containing the appropriate instance with parsed
    //     values as a System.Object and a sequence of CommandLine.Error.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     Thrown if one or more arguments are null.
    //
    //   T:System.ArgumentOutOfRangeException:
    //     Thrown if types array is empty.
    //
    // Remarks:
    //     All types must expose a parameterless constructor. It's strongly recommended
    //     to use a generic overload.
    public ParserResult<object> ParseArguments(IEnumerable<string> args, params Type[] types)
    {
        if (args == null)
        {
            throw new ArgumentNullException("args");
        }

        if (types == null)
        {
            throw new ArgumentNullException("types");
        }

        if (types.Length == 0)
        {
            throw new ArgumentOutOfRangeException("types");
        }

        return MakeParserResult(InstanceChooser.Choose((IEnumerable<string> arguments, IEnumerable<OptionSpecification> optionSpecs) => Tokenize(arguments, optionSpecs, settings), types, args, settings.NameComparer, settings.CaseInsensitiveEnumValues, settings.ParsingCulture, settings.AutoHelp, settings.AutoVersion, settings.AllowMultiInstance, HandleUnknownArguments(settings.IgnoreUnknownArguments)), settings);
    }

    //
    // Summary:
    //     Frees resources owned by the instance.
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private static Result<IEnumerable<Token>, Error> Tokenize(IEnumerable<string> arguments, IEnumerable<OptionSpecification> optionSpecs, ParserSettings settings)
    {
        if (!settings.GetoptMode)
        {
            return Tokenizer.ConfigureTokenizer(settings.NameComparer, settings.IgnoreUnknownArguments, settings.EnableDashDash)(arguments, optionSpecs);
        }

        return GetoptTokenizer.ConfigureTokenizer(settings.NameComparer, settings.IgnoreUnknownArguments, settings.EnableDashDash, settings.PosixlyCorrect)(arguments, optionSpecs);
    }

    private static ParserResult<T> MakeParserResult<T>(ParserResult<T> parserResult, ParserSettings settings)
    {
        return DisplayHelp(parserResult, settings.HelpWriter, settings.MaximumDisplayWidth);
    }

    private static ParserResult<T> DisplayHelp<T>(ParserResult<T> parserResult, TextWriter helpWriter, int maxDisplayWidth)
    {
        parserResult.WithNotParsed(delegate (IEnumerable<Error> errors)
        {
            Maybe.Merge(errors.ToMaybe(), helpWriter.ToMaybe()).Do(delegate (IEnumerable<Error> _, TextWriter writer)
            {
                writer.Write(HelpText.AutoBuild(parserResult, maxDisplayWidth));
            });
        });
        return parserResult;
    }

    private static IEnumerable<ErrorType> HandleUnknownArguments(bool ignoreUnknownArguments)
    {
        if (!ignoreUnknownArguments)
        {
            return Enumerable.Empty<ErrorType>();
        }

        return Enumerable.Empty<ErrorType>().Concat(ErrorType.UnknownOptionError);
    }

    private void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            if (settings != null)
            {
                settings.Dispose();
            }

            disposed = true;
        }
    }
}
#if false // Decompilation log
'172' items in cache
------------------
Resolve: 'netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'netstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
WARN: Version mismatch. Expected: '2.0.0.0', Got: '2.1.0.0'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\netstandard.dll'
------------------
Resolve: 'System.Runtime, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Runtime.dll'
------------------
Resolve: 'System.IO.MemoryMappedFiles, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.MemoryMappedFiles, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.IO.MemoryMappedFiles.dll'
------------------
Resolve: 'System.IO.Pipes, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.Pipes, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.IO.Pipes.dll'
------------------
Resolve: 'System.Diagnostics.Process, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.Process, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Diagnostics.Process.dll'
------------------
Resolve: 'System.Security.Cryptography, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Cryptography, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Security.Cryptography.dll'
------------------
Resolve: 'System.Memory, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Memory, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Memory.dll'
------------------
Resolve: 'System.Collections, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Collections.dll'
------------------
Resolve: 'System.Collections.NonGeneric, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections.NonGeneric, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Collections.NonGeneric.dll'
------------------
Resolve: 'System.Collections.Concurrent, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections.Concurrent, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Collections.Concurrent.dll'
------------------
Resolve: 'System.ObjectModel, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ObjectModel, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.ObjectModel.dll'
------------------
Resolve: 'System.Collections.Specialized, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections.Specialized, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Collections.Specialized.dll'
------------------
Resolve: 'System.ComponentModel.TypeConverter, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ComponentModel.TypeConverter, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.ComponentModel.TypeConverter.dll'
------------------
Resolve: 'System.ComponentModel.EventBasedAsync, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ComponentModel.EventBasedAsync, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.ComponentModel.EventBasedAsync.dll'
------------------
Resolve: 'System.ComponentModel.Primitives, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ComponentModel.Primitives, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.ComponentModel.Primitives.dll'
------------------
Resolve: 'System.ComponentModel, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ComponentModel, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.ComponentModel.dll'
------------------
Resolve: 'Microsoft.Win32.Primitives, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'Microsoft.Win32.Primitives, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\Microsoft.Win32.Primitives.dll'
------------------
Resolve: 'System.Console, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Console, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Console.dll'
------------------
Resolve: 'System.Data.Common, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Data.Common, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Data.Common.dll'
------------------
Resolve: 'System.Runtime.InteropServices, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.InteropServices, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Runtime.InteropServices.dll'
------------------
Resolve: 'System.Diagnostics.TraceSource, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.TraceSource, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Diagnostics.TraceSource.dll'
------------------
Resolve: 'System.Diagnostics.Contracts, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.Contracts, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Diagnostics.Contracts.dll'
------------------
Resolve: 'System.Diagnostics.TextWriterTraceListener, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.TextWriterTraceListener, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Diagnostics.TextWriterTraceListener.dll'
------------------
Resolve: 'System.Diagnostics.FileVersionInfo, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.FileVersionInfo, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Diagnostics.FileVersionInfo.dll'
------------------
Resolve: 'System.Diagnostics.StackTrace, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.StackTrace, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Diagnostics.StackTrace.dll'
------------------
Resolve: 'System.Diagnostics.Tracing, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.Tracing, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Diagnostics.Tracing.dll'
------------------
Resolve: 'System.Drawing.Primitives, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Drawing.Primitives, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Drawing.Primitives.dll'
------------------
Resolve: 'System.Linq.Expressions, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Linq.Expressions, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Linq.Expressions.dll'
------------------
Resolve: 'System.IO.Compression.Brotli, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.IO.Compression.Brotli, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.IO.Compression.Brotli.dll'
------------------
Resolve: 'System.IO.Compression, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.IO.Compression, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.IO.Compression.dll'
------------------
Resolve: 'System.IO.Compression.ZipFile, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.IO.Compression.ZipFile, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.IO.Compression.ZipFile.dll'
------------------
Resolve: 'System.IO.FileSystem.DriveInfo, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.FileSystem.DriveInfo, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.IO.FileSystem.DriveInfo.dll'
------------------
Resolve: 'System.IO.FileSystem.Watcher, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.FileSystem.Watcher, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.IO.FileSystem.Watcher.dll'
------------------
Resolve: 'System.IO.IsolatedStorage, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.IsolatedStorage, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.IO.IsolatedStorage.dll'
------------------
Resolve: 'System.Linq, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Linq, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Linq.dll'
------------------
Resolve: 'System.Linq.Queryable, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Linq.Queryable, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Linq.Queryable.dll'
------------------
Resolve: 'System.Linq.Parallel, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Linq.Parallel, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Linq.Parallel.dll'
------------------
Resolve: 'System.Threading.Thread, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.Thread, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Threading.Thread.dll'
------------------
Resolve: 'System.Net.Requests, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Requests, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.Requests.dll'
------------------
Resolve: 'System.Net.Primitives, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Primitives, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.Primitives.dll'
------------------
Resolve: 'System.Net.HttpListener, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Net.HttpListener, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.HttpListener.dll'
------------------
Resolve: 'System.Net.ServicePoint, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Net.ServicePoint, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.ServicePoint.dll'
------------------
Resolve: 'System.Net.NameResolution, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.NameResolution, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.NameResolution.dll'
------------------
Resolve: 'System.Net.WebClient, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Net.WebClient, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.WebClient.dll'
------------------
Resolve: 'System.Net.Http, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Http, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.Http.dll'
------------------
Resolve: 'System.Net.WebHeaderCollection, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.WebHeaderCollection, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.WebHeaderCollection.dll'
------------------
Resolve: 'System.Net.WebProxy, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Net.WebProxy, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.WebProxy.dll'
------------------
Resolve: 'System.Net.Mail, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Net.Mail, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.Mail.dll'
------------------
Resolve: 'System.Net.NetworkInformation, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.NetworkInformation, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.NetworkInformation.dll'
------------------
Resolve: 'System.Net.Ping, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Ping, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.Ping.dll'
------------------
Resolve: 'System.Net.Security, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Security, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.Security.dll'
------------------
Resolve: 'System.Net.Sockets, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Sockets, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.Sockets.dll'
------------------
Resolve: 'System.Net.WebSockets.Client, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.WebSockets.Client, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.WebSockets.Client.dll'
------------------
Resolve: 'System.Net.WebSockets, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.WebSockets, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Net.WebSockets.dll'
------------------
Resolve: 'System.Runtime.Numerics, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Numerics, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Runtime.Numerics.dll'
------------------
Resolve: 'System.Numerics.Vectors, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Numerics.Vectors, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Numerics.Vectors.dll'
------------------
Resolve: 'System.Reflection.DispatchProxy, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.DispatchProxy, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Reflection.DispatchProxy.dll'
------------------
Resolve: 'System.Reflection.Emit, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Emit, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Reflection.Emit.dll'
------------------
Resolve: 'System.Reflection.Emit.ILGeneration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Emit.ILGeneration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Reflection.Emit.ILGeneration.dll'
------------------
Resolve: 'System.Reflection.Emit.Lightweight, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Emit.Lightweight, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Reflection.Emit.Lightweight.dll'
------------------
Resolve: 'System.Reflection.Primitives, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Primitives, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Reflection.Primitives.dll'
------------------
Resolve: 'System.Resources.Writer, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Resources.Writer, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Resources.Writer.dll'
------------------
Resolve: 'System.Runtime.CompilerServices.VisualC, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.CompilerServices.VisualC, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Runtime.CompilerServices.VisualC.dll'
------------------
Resolve: 'System.Runtime.Serialization.Primitives, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Serialization.Primitives, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Runtime.Serialization.Primitives.dll'
------------------
Resolve: 'System.Runtime.Serialization.Xml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Serialization.Xml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Runtime.Serialization.Xml.dll'
------------------
Resolve: 'System.Runtime.Serialization.Json, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Serialization.Json, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Runtime.Serialization.Json.dll'
------------------
Resolve: 'System.Runtime.Serialization.Formatters, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Serialization.Formatters, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Runtime.Serialization.Formatters.dll'
------------------
Resolve: 'System.Security.Claims, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Claims, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Security.Claims.dll'
------------------
Resolve: 'System.Text.Encoding.Extensions, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Text.Encoding.Extensions, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Text.Encoding.Extensions.dll'
------------------
Resolve: 'System.Text.RegularExpressions, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Text.RegularExpressions, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Text.RegularExpressions.dll'
------------------
Resolve: 'System.Threading, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Threading.dll'
------------------
Resolve: 'System.Threading.Overlapped, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.Overlapped, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Threading.Overlapped.dll'
------------------
Resolve: 'System.Threading.ThreadPool, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.ThreadPool, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Threading.ThreadPool.dll'
------------------
Resolve: 'System.Threading.Tasks.Parallel, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.Tasks.Parallel, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Threading.Tasks.Parallel.dll'
------------------
Resolve: 'System.Transactions.Local, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Transactions.Local, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Transactions.Local.dll'
------------------
Resolve: 'System.Web.HttpUtility, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Web.HttpUtility, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Web.HttpUtility.dll'
------------------
Resolve: 'System.Xml.ReaderWriter, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Xml.ReaderWriter, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Xml.ReaderWriter.dll'
------------------
Resolve: 'System.Xml.XDocument, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Xml.XDocument, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Xml.XDocument.dll'
------------------
Resolve: 'System.Xml.XmlSerializer, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Xml.XmlSerializer, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Xml.XmlSerializer.dll'
------------------
Resolve: 'System.Xml.XPath.XDocument, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Xml.XPath.XDocument, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Xml.XPath.XDocument.dll'
------------------
Resolve: 'System.Xml.XPath, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Xml.XPath, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Xml.XPath.dll'
------------------
Resolve: 'System.Runtime.CompilerServices.Unsafe, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'System.Runtime.CompilerServices.Unsafe, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '2.0.0.0', Got: '7.0.0.0'
Load from: 'C:\Users\l\.nuget\packages\microsoft.netcore.app.ref\7.0.15\ref\net7.0\System.Runtime.CompilerServices.Unsafe.dll'
#endif
