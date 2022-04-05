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
                                + "\n Escolha uma das op��es..."
                                + "\n 1 - Adicionar um novo Time"
                                + "\n 2 - Pesquise por um Time (Nome)"
                                + "\n 3 - Editar as informa��es de um time"
                                + "\n 4 - Remover as informa��es de um time"
                                + "\n 5 - Fechar o programa");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("\nEscolha uma das op��es acima: ");
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
                        Console.Write("\nOpcao inv�lida! Escolha uma op��o v�lida. ");
                        break;
                }
                Console.WriteLine(pressButtons);
                Console.ReadKey();
            }
            while (option != "5");
        }

        void AddTeam()
        {
            Console.WriteLine("id: ");
            var id = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o nome do Time:");
            var nome = Console.ReadLine().ToUpper();

            Console.WriteLine("Digite o n�mero de t�tulos mundiais do Time:");
            if (!short.TryParse(Console.ReadLine(), out short titulosMundiais))
                Console.WriteLine("Digite um n�mero. Ser� considerado 0");

            Console.WriteLine("\n Digite o n�mero de t�tulos brasileiros do Time:");
            if (!byte.TryParse(Console.ReadLine(), out byte titulosBR))
                Console.WriteLine("Digite um n�mero. Ser� considerado 0");

            Console.Write("\n Informe a data de Cria��o do Time (formato dd/MM/aaaa):");
            if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly dataCriacao))
                Console.WriteLine("Digite uma data no formato pedido.");

            Console.WriteLine($"\n Nome {nome}  -  Titulos Mundiais {titulosMundiais}" +
                                 $"\n Titulos Brasileiros {titulosBR}" +
                                 $"\n Data de Cria��o {dataCriacao}" +
                                 $"\n Confirme os dados e escolha uma op��o." +
                                  "\n 1 - Correto" + "\n 2 - Incorreto");

            if (!int.TryParse(Console.ReadLine(), out int Option)) return;
            
            var donation = new Team(id, nome, titulosMundiais, titulosBR, dataCriacao);
            _teamRepositorie.Insert(donation);

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
            else
            {
                Console.WriteLine("\nN�o foi encontrado nenhum time!");

            }
        }
        void SearchTeam()
        {
            Console.WriteLine("\n== Digite o nome do time que deseja ter mais informa��es: ");
            var partialName = Console.ReadLine();

            var resultList = _teamRepositorie.GetName(partialName);

            if (!resultList.Any())
            {
                Console.WriteLine("==========");
                Console.WriteLine("\nNenhum time encontrado!");
                return;
            }

            Console.Write("==========");
            Console.WriteLine("\nResultado da busca: ");

            foreach (var team in resultList)
            {
                Console.WriteLine($"\t {team}");
                Console.WriteLine($"o time possui {team.tempoAtivo()} anos de hist�ria!");
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

            var resultDonation = resultList.FirstOrDefault(p => p.Id == id);

            if (resultDonation == null)
            {
                Console.WriteLine("\nNenhum time encontrado");
                return;
            }

            Console.WriteLine("Digite o nome do Time:");
            var editNome = Console.ReadLine().ToUpper();

            Console.WriteLine("Digite o n�mero de t�tulos mundiais do Time:");
            if (!byte.TryParse(Console.ReadLine(), out byte editTitulosM))
                Console.WriteLine("Digite um n�mero. Ser� considerado 0");

            Console.WriteLine("\n Digite o n�mero de t�tulos brasileiros do Time: ");
            if (!short.TryParse(Console.ReadLine(), out short editTitulosBR))
                Console.WriteLine("Digite um n�mero. Ser� considerado 0");

            Console.WriteLine("\n Informe a data de Cria��o do Time (formato dd/MM/aaaa):");
            if (DateOnly.TryParse(Console.ReadLine(), out DateOnly editData))
            {
                resultDonation.Name = editNome;
                resultDonation.Criacao = editData;
                resultDonation.TitulosBR = editTitulosBR;
                resultDonation.TitulosMundiais = editTitulosM;
                _teamRepositorie.Update(resultDonation);
                Console.WriteLine("\nTime alterado com sucesso!");
            }
            else
            {
                Console.WriteLine("Alguma informa��o est� errada, tente novamente.");
                return;
            }

        }
        void DeleteTeam()
        {

            var resultList = _teamRepositorie.GetTeams();
            Console.WriteLine("\nDigite o n�mero que deseja excluir: ");

            int.TryParse(Console.ReadLine(), out int id);

            var resultDonation = resultList.FirstOrDefault(p => p.Id == id);

            var result = resultList.FirstOrDefault(p => p.Id == id);
            if (result == null)
            {
                Console.WriteLine("N�mero inv�lido. Tente Novamente.");
                return;
            }
            _teamRepositorie.Delete(result);
            Console.WriteLine($"Time deletado com sucesso!");
        }
    }
}

