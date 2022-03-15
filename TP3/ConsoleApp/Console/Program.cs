using TeamDomain;
using System;
using System.Globalization;
using Infraestructure;

namespace ConsoleApp
{
    class Program
    {
        const string pressioneQualquerTecla = "Pressione qualquer tecla para exibir o menu principal ...";
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

            Console.WriteLine("Digite o Estado no qual o Time pertence:");
            estado = Console.ReadLine().ToUpper();

            Console.WriteLine("Digite o número de títulos brasileiros do Time: ");
            if (!int.TryParse(Console.ReadLine(), out titulosBR))
                Console.WriteLine("Número inválido");

            Console.WriteLine("Digite o número de títulos estaduais do Time: ");
            if (!int.TryParse(Console.ReadLine(), out titulosES))
                Console.WriteLine("Número inválido");

            Console.WriteLine("Informe a data de Criação do Time (formato dd/MM/yyyy):");
            if (!DateTime.TryParse(Console.ReadLine(), out dataCriacao))
            {
                Console.WriteLine($"Data inválida! Dados descartados! {pressioneQualquerTecla}");
                Console.ReadKey();
            }

            Console.WriteLine($"\n Nome {nome}  -  Estado {estado}" +
                              $"\n Titulos Brasileiros {titulosBR}  -  Titulos Estaduais {titulosES}" +
                              $"\n Data de Criação {dataCriacao}");

            Console.WriteLine("\nConfirme os dados registrados e escolha uma opção." +
                "\n 1 - Corretos" +
                "\n 2 - Incorretos");
            Console.ReadLine();

        }
        public static void SearchTeam()
        {

        }
    }
}