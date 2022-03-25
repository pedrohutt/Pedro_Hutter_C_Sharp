using WorkerService;
using Domain;
using Infraestructure;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ITeamRepository, TeamRepository1>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
