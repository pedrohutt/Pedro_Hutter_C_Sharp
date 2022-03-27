using WorkerService;
using Domain;
using Infraestructure;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ITeamRepository, TeamRepositorie1>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
