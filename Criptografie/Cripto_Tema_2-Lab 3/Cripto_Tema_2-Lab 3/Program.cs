
/*
 
    Cerinte : Scrieti un program C# care implementeaza :
        1. Cifrul Substitutiei Polialfabetice
        2. Cifrul Vigenere 
        3. Cilindrul Jefferson 
 
 */


using Cripto_Tema_2_Lab_3;
using System;

namespace Tema_2_Criptografie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"Acest program foloseste cifruri care transformă o propoziție inteligibilă într‑o formă codificată, folosind urmatoarele metode : Substitutie Polialfabetica , Cifrul vigenere si Cilindrul Jefferson. ");
            Console.WriteLine();
            Console.WriteLine("Introduceti informatia care doriti sa o criptati .");
            
            string PlainText = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine($"Alegeti un numar dintre 1 si 3 unde 1 reprezinta Substitutia Polialfabetica , 2 reprezinta Cifrul Vigenere iar 3 reprezinta Cilindrul Jefferson.");
            Console.Write($"Numarul ales este : ");
            string load2 = Console.ReadLine();
            Console.WriteLine();

            bool CorectNumber = false;
            int n = 0;

            while(!CorectNumber)
            {
                if(int.TryParse(load2,out n))
                {
                    if(n>=1 && n<=3)
                        CorectNumber = true;
                    else
                    {
                        Console.WriteLine($"Numarul introdus de dumneavoastra este incorect !");
                        Console.WriteLine($"Incercati din nou ! ");
                        Console.Write($"Numarul ales este : ");
                        load2 = Console.ReadLine();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"Numarul introdus de dumneavoastra este incorect !");
                    Console.WriteLine($"Incercati din nou ! ");
                    Console.Write($"Numarul ales este : ");
                    load2 = Console.ReadLine();
                    Console.WriteLine();
                }
            }

            if(n==1)
            {
                Console.WriteLine("Ati ales Substitutia polialfabetica ! Alegeti numarul de alfabete care doriti sa fie criptat informatia data.");
                Console.Write("Doresc sa fie :");
                load2 = Console.ReadLine();
                n= int.Parse(load2);
                Console.WriteLine();

                Polialphabet poli = new Polialphabet(n, PlainText);
                Console.WriteLine("Criptarea informatiei este :");
                Console.WriteLine(poli.Encryption());
                Console.WriteLine();
                Console.WriteLine("Decriptarea informatiei criptate este :");
                Console.WriteLine(poli.Decryption());

            }

            if (n==2)
            {
                Console.WriteLine("Ati ales Cifrul Vinegere ! Introduceti o parola pentru a crea cheia .");
                Console.WriteLine(" ! ATENTIE ! : Parola NU poate contine cifre sau alte semne ! TOATE TREBUIE SA FIE LITERE .");
                Console.Write("Parola este : ");
                load2 = Console.ReadLine();
                Console.WriteLine();

                Vinegere vinegere = new Vinegere(PlainText, load2);
                Console.WriteLine("Criptarea informatiei este :");
                Console.WriteLine(vinegere.Encryption());
                Console.WriteLine();
                Console.WriteLine("Decriptarea informatiei criptate este :");
                Console.WriteLine(vinegere.Decryption());
            }

        }
    }
}