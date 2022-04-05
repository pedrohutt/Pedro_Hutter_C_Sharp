using AT_CRUD;
using Domain;
using Infraestructure;

    IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ITeamRepository, TeamRepositoryInText>();
        services.AddHostedService<Worker>();

    })
    .Build();

    await host.RunAsync();

