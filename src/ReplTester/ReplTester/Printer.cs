using Serilog;

using Typin;

namespace ReplTester;
public static class Printer {
    private const string NULL_TEXT = "[null]";
    private const string LOG_FOLDER = "logs";
    private const string LOG_FILE = "log.txt";
    private const string CONSOLE_OUTPUT_TEMPLATE = "[{Level:u3}] {Message:lj}{NewLine}{Exception}";
    private const string FILE_OUTPUT_TEMPLATE = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level:u3} | {Message:lj}{NewLine}{Exception}";

    static Printer() {
        string latestLogFile = Path.Join(LOG_FOLDER, LOG_FILE);
        string datedLogFile = Path.Join(LOG_FOLDER, $"log_{DateTime.Now:yyyy-MM-dd_hh-mm-ss-ffff}.txt");

        Directory.CreateDirectory(LOG_FOLDER);

        if(File.Exists(latestLogFile)) {
            File.Delete(latestLogFile);
        }

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .WriteTo.Trace()
            .WriteTo.File(latestLogFile, outputTemplate: FILE_OUTPUT_TEMPLATE)
            .WriteTo.File(datedLogFile, outputTemplate: FILE_OUTPUT_TEMPLATE)
            .CreateLogger();
    }

    public static void Print(string? s) => Log.Information(s ?? NULL_TEXT);
    public static void Print(params string[]? s) => Print(s?.JoinStrings("") ?? NULL_TEXT);

    public static void Print(string label, object? obj) => Print(label + (obj?.ToString() ?? NULL_TEXT));
    public static void Print(object? obj) => Print("", obj);

    public static void Print<T>(string label, IEnumerable<T> ie) => Print(label + ie.Select(e => e?.ToString() ?? NULL_TEXT).JoinStrings());
    public static void Print<T>(IEnumerable<T> ie) => Print("", ie.Select(e => e?.ToString() ?? NULL_TEXT).JoinStrings());
    public static void Print(IEnumerable<string> s, string separator = ", ") => Print("", s.Select(e => e?.ToString() ?? NULL_TEXT).JoinStrings(separator));

    public static void Print(Exception e, string message) => Log.Error(e, message);
    public static void Print(Exception e) => Print(e, "Exception Occurred");

    /// <summary>Creates a formatted list of all elements in an array.</summary>
    /// <param name="arr">The current array.</param>
    /// <param name="separator">The string that separates elements of the array.</param>
    private static string JoinStrings<T>(this IEnumerable<T> arr, string separator = ", ") => string.Join(separator, arr);
}
