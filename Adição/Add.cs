using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora
{
    public class Add
    {
        public static void Main()
        {
            Console.WriteLine("Digite o primeiro número: ");
            var first = Console.ReadLine();
            if (!float.TryParse(first, out var a))
                Console.WriteLine("Número inválido");

            Console.WriteLine("Digite o segundo número: ");
            var second = Console.ReadLine();
            if (!float.TryParse(second, out var b))
                Console.WriteLine("Número inválido");

            float c = a + b;
            Console.WriteLine($"O resultado da soma de {a} mais {b} é igual a: {c}");
        }

    }
}