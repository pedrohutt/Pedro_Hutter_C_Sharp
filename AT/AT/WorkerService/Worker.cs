#nullable disable
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
        private readonly ITeamRepository _teamRepositorie;

        public Worker(ITeamRepository repositorie, IConfiguration configuration)
        {
            _teamRepositorie = repositorie;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            const string confirmation = "\n Confirme os dados e escolha uma opção." +
                                        "\n 1 - Correto" + "\n 2 - Incorreto";

            string[] AskInput ={"Digite o nome do Time:",
                                "Digite o número de títulos mundiais do Time:",
                                "\n Digite o número de títulos brasileiros do Time: ",
                                "\n Informe a data de Criação do Time (formato dd/MM/aaaa):"};

            _teamRepositorie.GetFiveLast();
            string menuOption;
            do
            {
                //Console.Clear();
                Console.WriteLine("\n### Gerenciador de Times de Futebol ###"
                                + "\n Escolha uma das opções..."
                                + "\n 1 - Adicionar um novo Time"
                                + "\n 2 - Pesquise por um Time (Nome)"
                                + "\n 3 - Editar as informações de um time"
                                + "\n 4 - Remover as informações de um time"
                                + "\n 5 - Fechar o programa");

                menuOption = Console.ReadLine();
                

                switch (menuOption)
                {   
                    case "1":
                        AddTeam();
                        break;

                    case "2":

                        SearchTeam();
                        break;
                        
                    case "3":

                        EditTeam();
                        break;

                    case "4":

                        DeleteTeam();
                        break;

                    case "5":

                        Console.WriteLine("Fechando o programa! Digite qualquer tecla para fechar o aplicativo.");
                        break;

                    default: break;                    
                }
            } while (menuOption != "5");

            void AddTeam()
            {       
                Console.WriteLine("Digite um Id");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Digite um número válido");
                    BackToMenu();
                    return;
                }
                    
                Console.WriteLine(AskInput[0]);
                string nome = Console.ReadLine().ToUpper();

                Console.WriteLine(AskInput[1]);
                if (!byte.TryParse(Console.ReadLine(), out byte titulosMundiais))
                    Console.WriteLine("Digite um número. Será considerado 0");

                Console.WriteLine(AskInput[2]);
                if (!short.TryParse(Console.ReadLine(), out short titulosBR))
                    Console.WriteLine("Digite um número. Será considerado 0");

                Console.WriteLine(AskInput[3]);
                if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly dataCriacao))
                    Console.WriteLine("Digite uma data no formato pedido.");

                Console.WriteLine($"\n Nome {nome}  -  Titulos Mundiais {titulosMundiais}" +
                                  $"\n Titulos Brasileiros {titulosBR}" +
                                  $"\n Data de Criação {dataCriacao}" +
                                  $"\n {confirmation}");

                if (!int.TryParse(Console.ReadLine(), out int Option))
                {
                    BackToMenu();
                    return ;
                }
                  

                if (Option == 1)
                {
                    Team team = new(id, nome, titulosMundiais, titulosBR, dataCriacao);
                    _teamRepositorie.Insert(team);
                    Console.WriteLine("Usuário cadastrado!");
                }
                else
                    Console.WriteLine("Tente novamente!");
            }

            void EditTeam()
            {
                Console.WriteLine("Digite o nome do time que deseja alterar:");
                var partialName = Console.ReadLine().ToUpper();

                var resultList = _teamRepositorie.Search(partialName);

                if (!resultList.Any())
                {
                    Console.WriteLine("\nNenhum resultado encontrado");
                    return;
                }

                Console.WriteLine("\nDigite o id que deseja alterar:");
                int.TryParse(Console.ReadLine(), out int id);

                var resultDonation = resultList.FirstOrDefault(p => p.Id == id);

                Console.WriteLine(AskInput[0]);
                string editNome = Console.ReadLine().ToUpper();

                Console.WriteLine(AskInput[1]);
                if (!byte.TryParse(Console.ReadLine(), out byte editTitulosM))
                    Console.WriteLine("Digite um número. Será considerado 0");

                Console.WriteLine(AskInput[2]);
                if (!short.TryParse(Console.ReadLine(), out short editTitulosBR))
                    Console.WriteLine("Digite um número. Será considerado 0");

                Console.WriteLine(AskInput[3]);
                if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly editData))
                    Console.WriteLine("Digite uma data no formato pedido.");


                Team editTeam = new(id, editNome, editTitulosM, editTitulosBR, editData);
                _teamRepositorie.Update(editTeam);
            }

            void SearchTeam()
            {
                Console.WriteLine("\n Digite o nome do Time que deseja ter mais informações: ");
                string searchString = Console.ReadLine().ToUpper();
                var teamsFound = _teamRepositorie.Search(searchString);

                if (!teamsFound.Any())
                {
                    Console.WriteLine($"Não foi encontrado nenhum time.");
                    return;
                }
                foreach (var team in teamsFound)
                {
                    Console.WriteLine($"{team.ShowTeamInfo()}");
                }               
            }
            void DeleteTeam()
            {
                var resultList = _teamRepositorie.GetAllTeams();
                Console.WriteLine("\n Digite o número do Time que deseja deletar: ");
                int.TryParse(Console.ReadLine(), out int id);
                var result = resultList.FirstOrDefault(p => p.Id == id);
                if (result == null)
                    Console.WriteLine("Número inválido. Tente Novamente.");

                _teamRepositorie.Delete(result);
                Console.WriteLine($"Time deletado com sucesso!");
            }

            static void BackToMenu()
            {
                Console.WriteLine("Data inválida! Dados descartados! Pressione qualquer tecla para exibir o menu principal ...");
                Console.ReadKey();
            }

        }
    } 
}
