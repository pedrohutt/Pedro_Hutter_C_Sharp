using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora
{
    public class Sub
    {
        public static void Main()
        {
            Console.WriteLine("Digite o primeiro número: ");
            var first = Console.ReadLine();
            if (!double.TryParse(first, out var a))
                Console.WriteLine("Número inválido");

            Console.WriteLine("Digite o segundo número: ");
            var second = Console.ReadLine();
            if (!double.TryParse(second, out var b))
                Console.WriteLine("Número inválido");

            double c = a - b;
            Console.WriteLine($"O resultado da subtração de {a} menos {b} é igual a: {c}");
        }

    }
}