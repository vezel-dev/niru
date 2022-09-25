using Vezel.Niru.Driver.Commands;

var app = new CommandApp<RunCommand>();

app.Configure(cfg =>
{
    // TODO: https://github.com/dotnet/Nerdbank.GitVersioning/issues/555
#pragma warning disable CS0436
    _ = cfg
        .SetApplicationName(ThisAssembly.AssemblyName)
        .PropagateExceptions();
#pragma warning restore CS0436

    _ = cfg
        .AddCommand<InfoCommand>("info")
        .WithDescription("Print Niru runtime environment information.");

    _ = cfg
        .AddCommand<RunCommand>("run")
        .WithDescription("Run a virtual machine.");
});

return await app.RunAsync(args);
