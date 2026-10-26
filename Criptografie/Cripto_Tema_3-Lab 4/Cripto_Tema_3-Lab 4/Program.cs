
/*

Cerinta : Scrieti un program care scrie cifrul Playfair cipher 

 */


using Cripto_Tema_3_Lab_4;
using System;

namespace Cripto_Tema_3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Cifrul Playfair");
            Console.WriteLine("Introduceti textul care doriti sa decriptati.");
            Console.WriteLine();

            string load = Console.ReadLine();

            Console.WriteLine();

            Playfair p = new Playfair();
            p.PlaintText = load;

            Console.WriteLine("Textul criptat este :");
            p.Enrcypt();
            Console.WriteLine(p.CipherText);
            Console.WriteLine();

            Console.WriteLine("Textul decriptat este : ");
            p.Decrypt();
            Console.WriteLine(p.PlaintText);

        }
    }
}