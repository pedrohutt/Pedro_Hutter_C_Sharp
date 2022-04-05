#nullable disable
using Domain;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AT_CRUD
{
    public class Worker : BackgroundService
    {
        private readonly ITeamRepository _teamRepositorie;
        private const string pressButtons = "Pressione qualquer tecla para exibir o menu principal ...";

        public Worker(ITeamRepository repositorie)
        {
            _teamRepositorie = repositorie;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            void ShowMenu()
            {
                Console.Clear();
                ShowLastFiveTeams();
                Console.WriteLine("\n### Gerenciador de Times de Futebol ###"
                                + "\n Escolha uma das opções..."
                                + "\n 1 - Adicionar um novo Time"
                                + "\n 2 - Pesquise por um Time (Nome)"
                                + "\n 3 - Editar as informações de um time"
                                + "\n 4 - Remover as informações de um time"
                                + "\n 5 - Fechar o programa"
                                + "\n --------------------------------------");
                Console.Write("\nEscolha uma das opções acima: ");
            }

            string option;
            do
            {
                ShowMenu();
                option = Console.ReadLine();
                switch (option)
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
                        Console.WriteLine("\nSaindo...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Write("\nOpcao inválida! Escolha uma opção válida. ");
                        break;
                }
                Console.WriteLine(pressButtons);
                Console.ReadKey();

            } while (option != "5");
        }

        void AddTeam()
        {
            Console.Write("\nDigite 1 para adicionar um time: ");
            var id = int.Parse(Console.ReadLine());

            Console.Write("\n Digite o nome do Time: ");
            var nome = Console.ReadLine().ToUpper();

            Console.Write("\n Digite o número de títulos mundiais do Time: ");
            if (!short.TryParse(Console.ReadLine(), out short titulosMundiais))
                Console.WriteLine("Digite um número. Será considerado 0");

            Console.Write("\n Digite o número de títulos brasileiros do Time: ");
            if (!byte.TryParse(Console.ReadLine(), out byte titulosBR))
                Console.WriteLine("Digite um número. Será considerado 0");

            Console.Write("\n Informe a data de Criação do Time (formato dd/MM/aaaa): ");
            if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly dataCriacao))
                Console.WriteLine("Digite uma data no formato pedido.");

            Console.WriteLine($"\n Nome {nome}  -  Titulos Mundiais {titulosMundiais}" +
                                 $"\n Titulos Brasileiros {titulosBR}" +
                                 $"\n Data de Criação {dataCriacao}" +
                                 $"\n Confirme os dados e escolha uma opção." +
                                  "\n 1 - Correto" + "\n 2 - Incorreto");

            int.Parse(Console.ReadLine());
            
            var team = new Team(id, nome, titulosMundiais, titulosBR, dataCriacao);
            _teamRepositorie.Insert(team);

            Console.WriteLine("\nTime adicionado com sucesso!");
           
        }

        void ShowLastFiveTeams()
        {
            var resultList = _teamRepositorie.GetFiveLast().ToList();

            if (resultList.Any())
            {
                foreach (var team in resultList)
                {
                    Console.WriteLine($"{team.GetTeamInfo()}");
                }
            }
            else Console.WriteLine("\nNão foi encontrado nenhum time!");
            
        }
        void SearchTeam()
        {
            Console.WriteLine("\n== Digite um termo presente no nome do time que deseja ter mais informações: ");
            var partialName = Console.ReadLine();

            var resultList = _teamRepositorie.GetName(partialName);

            if (!resultList.Any())
            {
                Console.WriteLine("\nNenhum time encontrado!");
                return;
            }
            Console.WriteLine("\n Times encontrados: ");

            foreach (var team in resultList)
            {
                Console.WriteLine($"\n ID = {team.Id} - Nome = {team.Name} " +
                $"\n Titulos Mundiais = {team.TitulosMundiais} " +
                $"\n Titulos Brasileiros = {team.TitulosBR} " +
                $"\n Data de fundação  do time = {team.Criacao}");

                Console.WriteLine($"O time possui {team.tempoAtivo()} anos de história!");
            }
        } 
        void EditTeam()
        {
            Console.WriteLine("Digite o nome do time que deseja alterar:");
            var partialName = Console.ReadLine();

            var resultList = _teamRepositorie.GetName(partialName);

            if (!resultList.Any())
            {
                Console.WriteLine("\nNenhum resultado encontrado");
                return;
            }
            Console.WriteLine("\nResultado da busca: ");

            foreach (var team in resultList)
            {
                Console.WriteLine($"{team.GetTeamInfo()}");
            }

            Console.WriteLine("\nEscolha o ID para correspondente:");
            int.TryParse(Console.ReadLine(), out int id);

            var teamEdit = resultList.FirstOrDefault(p => p.Id == id);

            if (teamEdit == null)
            {
                Console.WriteLine("\nNenhum time encontrado");
                return;
            }

            Console.WriteLine("Digite o nome do Time:");
            var editNome = Console.ReadLine().ToUpper();

            Console.WriteLine("Digite o número de títulos mundiais do Time:");
            if (!byte.TryParse(Console.ReadLine(), out byte editTitulosM))
                Console.WriteLine("Digite um número. Será considerado 0");

            Console.WriteLine("\n Digite o número de títulos brasileiros do Time: ");
            if (!short.TryParse(Console.ReadLine(), out short editTitulosBR))
                Console.WriteLine("Digite um número. Será considerado 0");

            Console.WriteLine("\n Informe a data de Criação do Time (formato dd/MM/aaaa):");
            if (DateOnly.TryParse(Console.ReadLine(), out DateOnly editData))
            {
                teamEdit.Name = editNome;
                teamEdit.Criacao = editData;
                teamEdit.TitulosBR = editTitulosBR;
                teamEdit.TitulosMundiais = editTitulosM;
                _teamRepositorie.Update(teamEdit);
                Console.WriteLine("\nTime alterado com sucesso!");
            }
            else
            {
                Console.WriteLine("Alguma informação está errada, tente novamente.");
                return;
            }

        }
        void DeleteTeam()
        {
            var resultList = _teamRepositorie.GetTeams();

            foreach (var team in resultList)
            {
                Console.WriteLine($"{team.Id} - {team.Name}");
            }

            Console.WriteLine("\nDigite o número que deseja excluir: ");
            int.TryParse(Console.ReadLine(), out int id);

            var resultDonation = resultList.FirstOrDefault(p => p.Id == id);
            var result = resultList.FirstOrDefault(p => p.Id == id);
            if (result == null)
            {
                Console.WriteLine("Número inválido. Tente Novamente.");
                return;
            }
            _teamRepositorie.Delete(result);
            Console.WriteLine($"Time deletado com sucesso!");
        }
    }
}

