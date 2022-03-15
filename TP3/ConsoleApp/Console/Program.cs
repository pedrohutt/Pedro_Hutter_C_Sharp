using Domain;
using Infraestructure;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuOpcoes();
            Console.ReadKey();
        }
        public static void MenuOpcoes()
        {
            bool Menu = true;
            string opcaoMenu;
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
                        Menu = false;
                        break;
                    default:
                        break;
                }
            } while (Menu);
        }
        public static void AddTeam()
        {
            string nome, estado;
            DateTime dataCriacao;
            int titulosBR, titulosES;

            Console.WriteLine("Digite o nome do Time a ser adicionado:");
            nome = Console.ReadLine().ToUpper();
            if (string.IsNullOrEmpty(nome))
                BackToMenu();

            Console.WriteLine("Digite o Estado no qual o Time pertence:");
            estado = Console.ReadLine().ToUpper();
            if (string.IsNullOrEmpty(estado))
                BackToMenu();

            Console.WriteLine("Digite o número de títulos brasileiros do Time: ");
            if (!int.TryParse(Console.ReadLine(), out titulosBR))
                BackToMenu();

            Console.WriteLine("Digite o número de títulos estaduais do Time: ");
            if (!int.TryParse(Console.ReadLine(), out titulosES))
                BackToMenu();

            Console.WriteLine("Informe a data de Criação do Time (formato dd/MM/yyyy):");
            if (!DateTime.TryParse(Console.ReadLine(), out dataCriacao))
                BackToMenu();

            Console.WriteLine($"\n Nome {nome}  -  Estado {estado}" +
                              $"\n Titulos Brasileiros {titulosBR}  -  Titulos Estaduais {titulosES}" +
                              $"\n Data de Criação {dataCriacao}");

            Console.WriteLine("\nConfirme os dados e escolha uma opção." +
                "\n 1 - Correto" +
                "\n 2 - Incorreto");
            Console.ReadLine();

        }
        public static void SearchTeam()
        {
            List<Teams> teams;
            int index = 0; int indexOption;
            Console.WriteLine("Digite o nome do Time que deseja ter mais informações: ");
            string searchName = Console.ReadLine().ToUpper();
            if (string.IsNullOrEmpty(searchName)) BackToMenu();
            
            teams = TeamsRepositorie.SearchTeam(searchName);

            if (teams.Any())
                Console.WriteLine("Nenhum usuário encontrado.");
            else
            {
                foreach (var t in teams)
                {
                    Console.WriteLine($"{index} - {t.Nome}");
                    index++;
                }
                do Console.WriteLine("Escolha o número relacionado ao time que deseja ter mais informações:");
                while (!int.TryParse(Console.ReadLine(), out indexOption));
                if (indexOption < teams.Count) Console.WriteLine(TeamsRepositorie.ShowTeamInfo(teams[indexOption]));
                Console.WriteLine("Número escolhido inválido");

            }
        }

        public static void BackToMenu()
        {
            Console.WriteLine("Data inválida! Dados descartados! Pressione qualquer tecla para exibir o menu principal ...");
            Console.ReadKey();
            MenuOpcoes();
        }
    }
}