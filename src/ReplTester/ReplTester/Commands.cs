using Typin;
using Typin.Attributes;
using Typin.Console;

using static ReplTester.Printer;

namespace ReplTester;
public static class Commands {
    [Command("ping", Description = "Tests if the CLI is working.")]
    public class PingCommand : ICommand {
        public ValueTask ExecuteAsync(IConsole console) {
            Print("Pong!");
            return default;
        }
    }

    [Command("countdown", Description = "Begins a countdown to 0.")]
    public class CountdownCommand : ICommand {
        [CommandParameter(0, Name = "Delay", Description = "The number of seconds to wait. (Must be a positive integer.)")]
        public int Delay { get; set; }

        public async ValueTask ExecuteAsync(IConsole console) {
            if(Delay < 0) {
                Print("Delay must be a positive number");
                return;
            }

            while(Delay > 0) {
                Print($"{Delay}...");
                Delay--;
                await Task.Delay(1000);
            }

            Print("0!");
        }
    }

    [Command("add", Description = "Adds two numbers.")]
    public class AddCommand : ICommand {
        [CommandParameter(0, Name = "left", Description = "The left-hand side of the operation.")]
        public decimal Left { get; set; }

        [CommandParameter(1, Name = "right", Description = "The right-hand side of the operation.")]
        public decimal Right { get; set; }

        public async ValueTask ExecuteAsync(IConsole console) {
            Print($"{Left} + {Right} = {Left + Right}");
            await ValueTask.CompletedTask;
        }
    }


    [Command("sum", Description = "Begins a countdown to 0.")]
    public class SumCommand : ICommand {
        [CommandParameter(0, Name = "values", Description = "The values to sum.")]
        public decimal[] Values { get; set; } = null!;

        public async ValueTask ExecuteAsync(IConsole console) {
            Print($"{string.Join(" + ", Values)} = {Values.Sum()}");
            await ValueTask.CompletedTask;
        }
    }
}
