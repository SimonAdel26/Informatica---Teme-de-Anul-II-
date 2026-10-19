using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cripto_Tema_2_Lab_3
{
    public class Vinegere : Cryptography
    {
        private char[] plainText;
        private char[] cipherText;
        private char[] Key;

        public Vinegere(string plaintext , string password)
        {
            NormalizePlaintext(plaintext);
            GenerateKey(password);
        }

        private void NormalizePlaintext(string plaintext)
        {
            string[] RemovingPunctuation = plaintext.Split(' ', ',', '.', '!', '?', ':', ';');
            string text = "";

            for (int i = 0; i < RemovingPunctuation.Length; i++)
                text = text + RemovingPunctuation[i].ToUpper();

            plainText = text.ToCharArray();
        }

        private void GenerateKey(string password)
        {
            Key = new char[plainText.Length];

            char[] word = password.ToUpper().ToCharArray();

            for (int i = 0; i < Key.Length; i++)
                Key[i] = word[i % word.Length];
        }

        public override string Encryption()
        {
            cipherText = new char[plainText.Length];
            for (int i = 0; i < plainText.Length; i++)
                cipherText[i] = (char)(((plainText[i] - 'A') + (Key[i] - 'A')) % 26 + 'A');
            
            return new string(cipherText);
        }

        public override string Decryption()
        {
            char[] Decrypt = new char[cipherText.Length];

            for (int i = 0;i < cipherText.Length; i++)
                Decrypt[i]= (char)(((cipherText[i] - 'A') - (Key[i] - 'A')+26) % 26 +'A');

            return new string(Decrypt);
        }
    }
}
