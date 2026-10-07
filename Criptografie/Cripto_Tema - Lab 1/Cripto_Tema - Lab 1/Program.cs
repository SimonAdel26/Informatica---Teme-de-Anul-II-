
/*
 Cerinte : 

1. Scrieti un program care implementeaza cifrul lui Cezar . Criptare / Descriptare / Criptanaliza 
2. Scrieti un program care implementeza cifrul "+n" . Criptare / Descriptare / Criptanaliza 
3. Scrieti un program care implementeza cifrul substituitiei monoalfabetica . Criptare / Descriptare / Criptanaliza 
(cheia este o permutare aleatorie a alfabetului ) 
4. Scrieti un program care implementeaza ROT13 . Criptare / Descriptare / Criptanaliza
 
 */

using System;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Tema_Criptografie_Lab_1
{
    public class Program
    {
        // -----------------------------------------     Main    --------------------------------------- 
        public static void Main(string[] args)
        {
            Console.WriteLine($"Acest program foloseste cifruri care transformă o propoziție inteligibilă într‑o formă codificată, folosind urmatoarele metode : Cezar , +n , substituitiei monoalfabetica si ROT13. ");
            Console.WriteLine($"Scrieti mesajul care doriti sa codificati.");
            Console.WriteLine();

            string load1 = Console.ReadLine();

            Console.WriteLine($"Alegeti un numar dintre 1 si 4 unde 1 reprezinta Cifrul lui Cezar , 2 reprezinta Cifrul +n , 3 reprezinta cifrul substituitiei monoalfabetica iar 4 este Cifrul ROT13 .");
            Console.Write($"Numarul ales este : ");
            string load2 = Console.ReadLine();
            Console.WriteLine();

            bool verif = false;
            int number = 0;
            int method = 3;

            while (!verif)
            {
                if (int.TryParse(load2, out method))
                {
                    if (method >= 1 && method <= 4)
                    {
                        if (method == 2)
                        {
                            Console.WriteLine($"Ati ales Cifrul +n : alegeti un numar oarecare . Poate sa fie si un numar negativ .");
                            load2 = Console.ReadLine();
                            number = int.Parse(load2);
                            Console.WriteLine();
                        }

                        verif = true;
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
                else
                {
                    Console.WriteLine($"Numarul introdus de dumneavoastra este incorect !");
                    Console.WriteLine($"Incercati din nou ! ");
                    Console.Write($"Numarul ales este : ");
                    load2 = Console.ReadLine();
                    Console.WriteLine();
                }
            }

            Cryptography cripto = new Cryptography(load1, number, method);

            Console.WriteLine($"Criptografia este : ");
            Console.WriteLine(cripto.Encryption());
            Console.WriteLine();

            Console.WriteLine($"Decriptarea este :");
            Console.WriteLine(cripto.Decryptor());
            Console.WriteLine();

            Console.WriteLine($"Criptanaliza este :");

            List<string> listCryptoanalysis = new List<string>();
            listCryptoanalysis = cripto.Cryptanalysis();
            foreach(var text in listCryptoanalysis)
                Console.WriteLine(text);
            Console.WriteLine();

            
        }
    }

    // -------------------------------------   Interfaces    --------------------------------------------
    interface IShuffle
    {
        public void Shuffle();
    }

    interface ILoadCommonWords
    {
        public void LoadCommonWords();
    }

    interface ILoadLetterFrequency
    {
        public void LoadLetterFrequency();
    }

    // -------------------------------------    Classes     ----------------------------------------------
    public  class Words :Letters ,  ILoadCommonWords
    {

        // ------------------------------- Reading common words -----------------------------------------
        public List<string> words = new List<string>();
        public void LoadCommonWords()
        {
            foreach (var line in File.ReadLines("../../../Common_Words.txt"))
            {
                words.Add(line.Trim().ToUpper());
            }

        }

        // ---------------------------- Verify if the word is intelligible -------------------------------
        public bool IsIntelligible(string text)
        {
            foreach (var word in words)
                if (text.Contains(word))
                    return true;
            return false;
        }
    }

    public  class Letters : ILoadLetterFrequency
    {

        // -------------------------------- Reading Letter Frequency -----------------------------------

        public List<string> LetterFrequency = new List<string>();
        public void LoadLetterFrequency()
        {
            foreach (var line in File.ReadLines("C:\\Users\\Adela\\Desktop\\Adel\\Facultate\\Anul 2\\Informatica II - Teme\\Criptografie\\Cripto_Tema - Lab 1\\Cripto_Tema - Lab 1\\Letter_Frequencytxt.txt"))
            {
                LetterFrequency.Add(line.Trim().ToUpper());
            }
        }

        // ------------------------------------ Sorting letters -----------------------------------------
        public void Sort( List<char> letter , List<int> letterFrequency)
        {
            var pairs = new List<(char Letter, int Frequency)>();

            for (int i=0;i< letter.Count;i++)
            {
                pairs.Add((letter[i], letterFrequency[i]));
            }

            pairs = pairs.OrderByDescending(p => p.Frequency).ToList();

            letter.Clear();
            letterFrequency.Clear();

            foreach( var p in pairs)
            {
                letter.Add(p.Letter);
                letterFrequency.Add(p.Frequency);
            }

        }

    }

    public class Cryptography : Words, IShuffle
    {
        private char[] Initial_Date;
        private char[] CryptoOutput;
        private int number_method;
        private int n;

        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private char[] Chiper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private Dictionary<char, char> mapping = new Dictionary<char, char>();
        private List<string> CryptAnalistIntelligible = new List<string>();
       
        // -----------------------------------    Constructor    -----------------------------------------
        public Cryptography(string date, int number = 0 , int method = 3 )
        {
            Initial_Date = date.ToUpper().ToCharArray();
            number_method = method;
            if (method == 1) n = 3;
            else if (method == 2) n = number;
            else if (method == 4) n = 13;
            CryptoOutput = new char[Initial_Date.Length];
        }


        // ----------------------------------   Interfete aplicate   ------------------------------------------
        public void Shuffle()
        {   

            for (int i = 0; i < Chiper.Length - 2; i++)
            {
                Random rn = new Random();
                int j = rn.Next(i, Chiper.Length - 1);
                char aux = Chiper[i];
                Chiper[i] = Chiper[j];
                Chiper[j] = aux;
            }

            for (int i = 0; i < Chiper.Length; i++)
            {
                mapping[alphabet[i]]= Chiper[i];
            }
        }


        // ---------------- Metode :  Encryption ,  Decryptor , Cryptanalysis ---------------------------------
        public char[] Encryption()
        {
            if (number_method ==3 )
                this.Shuffle();

            for (int i = 0; i < Initial_Date.Length; i++)
            {
                if (Initial_Date[i] == '.' || Initial_Date[i] == ',' || Initial_Date[i] == ';' ||
                    Initial_Date[i] == ':' || Initial_Date[i] == '?' || Initial_Date[i] == '!' ||
                    Initial_Date[i] == '(' || Initial_Date[i] == ')' || Initial_Date[i] == ' ')
                {
                    CryptoOutput[i] = Initial_Date[i];
                    continue;
                }

                if (number_method == 3)
                    CryptoOutput[i] = mapping[Initial_Date[i]];
                else 
                    CryptoOutput[i] = (char)('A' + ((Initial_Date[i] - 'A' + n) % 26 + 26) % 26);
            }

            
            return  CryptoOutput;
        }
    
        public string Decryptor()
        {
            char[] Old_Date = new char[CryptoOutput.Length];

            Dictionary<char, char> inverseMapping = new Dictionary<char, char>();

            foreach (var pair in mapping)
            {
                inverseMapping[pair.Value] = pair.Key;
            }

           
            for (int i = 0; i < CryptoOutput.Length; i++)
            {
                if (Initial_Date[i] == '.' || Initial_Date[i] == ',' || Initial_Date[i] == ';' ||
                    Initial_Date[i] == ':' || Initial_Date[i] == '?' || Initial_Date[i] == '!' ||
                    Initial_Date[i] == '(' || Initial_Date[i] == ')' || Initial_Date[i] == ' ')
                {
                    Old_Date[i] = CryptoOutput[i];
                    continue;
                }

                if(number_method ==3 )
                    Old_Date[i] = inverseMapping[CryptoOutput[i]];
                else
                    Old_Date[i] = (char)('A' + ((CryptoOutput[i] - 'A' - n) % 26 + 26) % 26);
            }

                return string.Join("", Old_Date);
           
        }
    
        public List<string> Cryptanalysis()
        { 

            if (number_method == 3)
            {
                LoadLetterFrequency();
                
                List<char> CopyCryptoOutput = CryptoOutput.ToList();
                List<char> letter = new List<char>();
                List<int> letterFrequency = new List<int>();

                while (CopyCryptoOutput.Count > 0)
                {
                    letter.Add(CopyCryptoOutput[0]);
                    letterFrequency.Add(CopyCryptoOutput.Count(c => c == CopyCryptoOutput[0]));
                    CopyCryptoOutput.RemoveAll(c => c == CopyCryptoOutput[0]);
                }

                Sort(letter,letterFrequency);
                
                for (int i=0; i< CryptoOutput.Length;i++)
                {
                    for (int j = 0; j< letter.Count;j++)
                    {
                        if(letter[j] ==CryptoOutput[i])
                        {
                            CryptoOutput[i] = Convert.ToChar(LetterFrequency[j]);
                        }
                    }
                }

                CryptAnalistIntelligible.Add(string.Join("", CryptoOutput));
                return CryptAnalistIntelligible;
            }

            LoadCommonWords();

            for (int i=0; i<26; i++)
            {
                char[] Cryptanalist = new char[CryptoOutput.Length];

                for (int j = 0; j < CryptoOutput.Length; j++)
                {
                    if (CryptoOutput[j] == '.' || CryptoOutput[j] == ',' || CryptoOutput[j] == ';' ||
                   CryptoOutput[j] == ':' || CryptoOutput[j] == '?' || CryptoOutput[j] == '!' ||
                   CryptoOutput[j] == '(' || CryptoOutput[j] == ')' || CryptoOutput[j] == ' ')
                    {
                        Cryptanalist[j] = CryptoOutput[j];
                        continue;
                    }

                    Cryptanalist[j] = (char)('A' + ((CryptoOutput[j] - 'A' - i+26) % 26));
                }

                string VerifyIsIntelligible = string.Join("", Cryptanalist);
                if(IsIntelligible(VerifyIsIntelligible))
                    CryptAnalistIntelligible.Add(VerifyIsIntelligible);
            }

            return CryptAnalistIntelligible;
        }

    }

}
