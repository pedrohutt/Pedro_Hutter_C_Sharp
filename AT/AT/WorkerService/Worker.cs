using Domain;
using Infraestructure;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ITeamRepository _teamReposirotie;

        public Worker(ITeamRepository repositorie)
        {
            _teamReposirotie = repositorie;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            const string confirmation = "\n Confirme os dados e escolha uma op��o." +
                                        "\n 1 - Correto" + "\n 2 - Incorreto";

            string[] AskInput ={"Digite o nome do Time a ser adicionado:",
                                "Digite o n�mero de t�tulos mundiais do Time:",
                                "\n Digite o n�mero de t�tulos brasileiros do Time: ",
                                "\n Informe a data de Cria��o do Time (formato dd/MM/aaaa):"};

            bool Menu = true;
            string? menuOption;
            do
            {
                Console.WriteLine("\n### Gerenciador de Times de Futebol ###"
                                + "\n Escolha uma das op��es..."
                                + "\n 1 - Adicionar um novo Time"
                                + "\n 2 - Pesquise por um Time (Nome)"
                                + "\n 3 - Editar as informa��es de um time"
                                + "\n 4 - Remover as informa��es de um time"
                                + "\n 5 - Fechar o programa");

                menuOption = Console.ReadLine();
                switch (menuOption)
                {
                    case "1":
                        
                        Console.WriteLine(AskInput[0]);
                        string? nome = Console.ReadLine().ToUpper();

                        Console.WriteLine(AskInput[1]);
                        if (!byte.TryParse(Console.ReadLine(), out byte titulosMundiais))
                            Console.WriteLine("Digite um n�mero. Ser� considerado 0");

                        Console.WriteLine(AskInput[2]);
                        if (!int.TryParse(Console.ReadLine(), out int titulosBR)) 
                            Console.WriteLine("Digite um n�mero. Ser� considerado 0");

                        Console.WriteLine(AskInput[3]);
                        if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly dataCriacao))
                            Console.WriteLine("Digite uma data no formato pedido.");

                        Console.WriteLine($"\n Nome {nome}  -  Titulos Mundiais {titulosMundiais}" +
                                          $"\n Titulos Brasileiros {titulosBR}" +
                                          $"\n Data de Cria��o {dataCriacao}" +
                                          $"\n {confirmation}");

                        if (!int.TryParse(Console.ReadLine(), out int Option))
                            Console.WriteLine("Op��o inv�lida!");

                        if (Option == 1)
                        {
                            Team team = new()
                            {
                                Nome = nome,
                                TitulosMundiais = titulosMundiais,
                                TitulosBrasileiros = titulosBR,
                                DataCriacao = dataCriacao,  
                            };
                            _teamReposirotie.Add(team);
                            Console.WriteLine("Usu�rio cadastrado!");
                        }
                        else
                          Console.WriteLine("Tente novamente!");
                        break;

                    case "2":
                        Console.WriteLine("\n Digite o nome do Time que deseja ter mais informa��es: ");
                        string searchString = Console.ReadLine().ToUpper();
                        var teamsFound = _teamReposirotie.Search(searchString);

                        if (teamsFound.Count > 0)
                        {
                            Console.WriteLine("\n Escolha o n�mero relacionado ao time que deseja ter mais informa��es:");
                            for (var index = 0; index < teamsFound.Count; index++)
                            {
                                Console.WriteLine($"{index} - {teamsFound[index].Nome}");
                            }
                            if (!int.TryParse(Console.ReadLine(), out var indexOption) || indexOption > teamsFound.Count)
                            {
                                Console.WriteLine("N�mero escolhido inv�lido");
                                continue;
                            }
                            if (indexOption < teamsFound.Count)
                            {
                                var teams = teamsFound[indexOption];
                                Console.WriteLine(_teamReposirotie.ShowTeamInfo(teams));
                            }
                        }
                        else
                        {
                            Console.WriteLine($"N�o foi encontrado nenhum time.");
                        }
                        break;
                        
                    case "3":

                        _teamReposirotie.ShowAllTeams();
                        Console.WriteLine(AskInput[3]);
                        if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly editData))
                            Console.WriteLine("Digite uma data no formato pedido.");
                        _teamReposirotie.Edit(0, "FLA", 1, 2, editData);
                        
                        break;
                    case "4":
                        //DeleteTeam();
                    case"5":
                        Console.WriteLine("Fechando o programa! Digite qualquer tecla para fechar o aplicativo.");
                        Menu = false;
                        break;
                    default:
                        break;
                }
            } while (Menu == true);
        }
    } 
}
