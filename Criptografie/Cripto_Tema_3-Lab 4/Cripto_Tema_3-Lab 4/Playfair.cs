using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Cripto_Tema_3_Lab_4
{
    internal class Playfair : Cryptography
    {
        char[,] TableKey = new char[5,5];

        #region Properties
        public string PlaintText
        {
            get { return new string(plainText); }
            set
            {
                plainText = NormalizePlaintext(value);
                GenerateTableKey();
                RemoveConsecutiveDuplicates();

                Console.WriteLine(plainText);

            }
        }

        public string CipherText
        {
            get { return new string(cipherText); }
            set
            {
                cipherText = value.ToCharArray();
            }
        }

        #endregion

        public Playfair() { }

        public Playfair(string plaintext)
        {
            PlaintText = plaintext;
        }

        private void GenerateTableKey()
        {
            List<char> letters = new List<char>();
            var seen  = new HashSet<char>();

            foreach (char c in plainText.Concat(alphabet))
                if (alphabet.Contains(c) && seen.Add(c))
                    letters.Add(c);

            for (int i = 0; i < letters.Count; i++)
                TableKey[i / 5, i % 5] = letters[i];
            
        }

        private void RemoveConsecutiveDuplicates()
        {
            List<char> text = new List<char>();
            for (int i = 0; i < plainText.Length - 1; i++)
                if (plainText[i] == plainText[i + 1])
                {
                    text.Add(plainText[i]);
                    text.Add('X');
                }
                else text.Add(plainText[i]);
            if (text.Count <= plainText.Length)
                text.Add(plainText[plainText.Length-1]);

            PreparePlainText(text);

            plainText = text.ToArray();

        }

        private void PreparePlainText(List<char> t)
        {
            if (t.Count % 2 == 1)
                t.Add('X');
        }


        private (int row , int col) GetPositionFromTable(char caracter)
        {
            for(int i=0;i< TableKey.GetLength(0);i++)
                for(int j=0;j< TableKey.GetLength(1);j++)
                    if (TableKey[i,j]==caracter)
                        return (i,j);

            throw new Exception("Nu s-a gasit elementul in matrice");
        }

        public override void Enrcypt()
        {
            cipherText = new char[plainText.Length];

            for (int i = 0; i < plainText.Length-1; i = i + 2)
            {
                var (r1 ,c1) = GetPositionFromTable(plainText[i]);
                var (r2, c2) = GetPositionFromTable(plainText[i + 1]);


                if (r1== r2)
                {
                    cipherText[i] = TableKey[r1, (c1 + 1) % 5];
                    cipherText[i+1]=TableKey[r2, (c2 + 1) % 5];
                }
                else if (c1 == c2)
                {
                    cipherText[i] = TableKey[(r1 + 1) % 5, c1];
                    cipherText[i+1] = TableKey[(r2 + 1) % 5, c2];
                }
                else
                {
                    cipherText[i] = TableKey[r1, c2];
                    cipherText[i+1] = TableKey[r2, c1];
                }

            }
        }

        public override void Decrypt()
        {
            char[] decryptedText = new char[cipherText.Length];

            for(int i = 0;i<cipherText.Length-1;i=i+2)
            {
                var (r1, c1) = GetPositionFromTable(cipherText[i]);
                var (r2, c2) = GetPositionFromTable(cipherText[i + 1]);


                if (r1 == r2)
                {
                    decryptedText[i] = TableKey[r1, (c1 - 1 + 5) % 5];
                    decryptedText[i + 1] = TableKey[r2, (c2 - 1 +5 ) % 5];
                }
                else if (c1 == c2)
                {
                    decryptedText[i] = TableKey[(r1 - 1 + 5 ) % 5, c1];
                    decryptedText[i + 1] = TableKey[(r2 - 1 + 5) % 5, c2];
                }
                else
                {
                    decryptedText[i] = TableKey[r1, c2];
                    decryptedText[i + 1] = TableKey[r2, c1];
                }
            }

            
            plainText = decryptedText; 

        }
    }
}
