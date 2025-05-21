using Typin;
using Typin.Modes;
using static ReplTester.Printer;

namespace ReplTester;

internal static class Program {
    public static async Task<int> Main() {
        Print("Initializing REPL");

        await TestCode();

        return await new CliApplicationBuilder()
            .AddCommandsFromThisAssembly()
            .UseMiddleware<TypinInputPrinter>()
            .UseInteractiveMode(true)
            .Build()
            .RunAsync();
    }

    /// <summary>
    /// Test code to be executed before initializing the READ-EVAL-PRINT Loop
    /// </summary>
    private static async Task TestCode() {
        await Task.CompletedTask;
    }
}