using Domain;
using Infraestructure;

namespace ConsoleApp
{
    class Program
    {
        private static void Main()
        {
            OptionsMenu();
            Console.ReadKey();
        }
        private static void OptionsMenu()
        {
            bool Menu = true;
            string? opcaoMenu;
            do
            {
                Console.WriteLine("\n### Gerenciador de Times de Futebol ###"
                                + "\n Escolha uma das opções..."
                                + "\n 1 - Adicionar um novo Time"
                                + "\n 2 - Pesquise por um Time (Nome)"
                                + "\n 3 - Para fechar o Programa");

                opcaoMenu = Console.ReadLine();
                switch (opcaoMenu)
                {
                    case "1":
                        AddTeam();
                        break;
                    case "2":
                        SearchTeam();
                        break;
                    case "3":
                        Console.WriteLine("Fechando o programa! Digite qualquer tecla para fechar o aplicativo.");
                        Menu = false;
                        break;
                    default:
                        break;
                }
            } while (Menu);
        }
        private static void AddTeam()
        {
            string[] AskInput =
            {
                "Digite o nome do Time a ser adicionado:",
                "Digite o Estado no qual o Time pertence:",
                "Digite o número de títulos brasileiros do Time: ",
                "Digite o número de títulos estaduais do Time: ",
                "Informe a data de Criação do Time (formato dd/MM/yyyy):",
            };

            string? nome, estado;
            DateTime dataCriacao;
            int titulosBR, titulosES;

            Console.WriteLine(AskInput[0]);
            nome = Console.ReadLine().ToUpper();
            if (string.IsNullOrEmpty(nome))
                BackToMenu();

            Console.WriteLine(AskInput[1]);
            estado = Console.ReadLine().ToUpper();
            if (string.IsNullOrEmpty(estado))
                BackToMenu();

            Console.WriteLine(AskInput[2]);
            if (!int.TryParse(Console.ReadLine(), out titulosBR))
                BackToMenu();

            Console.WriteLine(AskInput[3]);
            if (!int.TryParse(Console.ReadLine(), out titulosES))
                BackToMenu();

            Console.WriteLine(AskInput[4]);
            if (!DateTime.TryParse(Console.ReadLine(), out dataCriacao))
                BackToMenu();

            Console.WriteLine($"\n Nome {nome}  -  Estado {estado}" +
                              $"\n Titulos Brasileiros {titulosBR}  -  Titulos Estaduais {titulosES}" +
                              $"\n Data de Criação {dataCriacao}");

            Console.WriteLine("\nConfirme os dados e escolha uma opção." +
                              "\n 1 - Correto" +
                              "\n 2 - Incorreto");

            var option = Console.ReadLine();
            if (!int.TryParse(option, out int Option))
                BackToMenu();

            if (Option == 1)
            {
                Teams teams = new Teams()
                {
                    Nome = nome,
                    Estado = estado,
                    DataCriacao = dataCriacao,
                    TitulosBrasileiros = titulosBR,
                    TitulosEstaduais = titulosES,
                };
                TeamsRepositorie.TeamRegister(teams);
                Console.WriteLine("Usuário cadastrado!");
            }
            else if (Option == 2)
            {
                BackToMenu();
            }
            else
            Console.WriteLine("Escolha uma opção válida! Tente novamente");

        }
        private static void SearchTeam()
        {
            List<Teams> teams;
            int index = 0; int indexOption;
            Console.WriteLine("Digite o nome do Time que deseja ter mais informações: ");
            string? searchName = Console.ReadLine().ToUpper();
            if (string.IsNullOrEmpty(searchName)) BackToMenu();
            
            teams = TeamsRepositorie.SearchTeam(searchName);

            if (!teams.Any())
                Console.WriteLine("Nenhum usuário encontrado.");
            else
            {
                foreach (var t in teams)
                {
                    Console.WriteLine($"{index +1} - {t.Nome}");
                    index++;
                }
                do Console.WriteLine("Escolha o número relacionado ao time que deseja ter mais informações:");
                while (!int.TryParse(Console.ReadLine(), out indexOption));
                indexOption -= 1;
                if (indexOption < teams.Count) 
                    Console.WriteLine(TeamsRepositorie.ShowTeamInfo(teams[indexOption]));
                else 
                    Console.WriteLine("Número escolhido inválido");
            }
        }
        private static void BackToMenu()
        {
            Console.WriteLine("Data inválida! Dados descartados! Pressione qualquer tecla para exibir o menu principal ...");
            Console.ReadKey();
            OptionsMenu();
        }
    }
}