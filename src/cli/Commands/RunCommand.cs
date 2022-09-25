namespace Vezel.Niru.Driver.Commands;

[SuppressMessage("", "CA1812")]
internal sealed class RunCommand : AsyncCommand<RunCommand.RunCommandSettings>
{
    public sealed class RunCommandSettings : CommandSettings
    {
    }

    public override Task<int> ExecuteAsync(CommandContext context, RunCommandSettings settings)
    {
        // TODO

        return Task.FromResult(0);
    }
}
