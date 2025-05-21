using Typin;

namespace ReplTester;
// ReSharper disable once ClassNeverInstantiated.Global
/// <summary>
/// Typin Middleware that prints the input command to the Serilog pipeline.
/// </summary>
/// <remarks>Necessary for the Serilog File sink to print user input.</remarks>
internal sealed class TypinInputPrinter : IMiddleware {
    public async Task HandleAsync(ICliContext context, CommandPipelineHandlerDelegate next, CancellationToken cancellationToken) {
        Printer.Print("> " + context.Input);
        await next();
    }
}