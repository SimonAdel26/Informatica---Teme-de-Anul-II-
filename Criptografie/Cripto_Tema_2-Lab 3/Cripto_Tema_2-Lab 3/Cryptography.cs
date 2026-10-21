using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cripto_Tema_2_Lab_3
{
    // ---------------------------------------   INTERFACE   ----------------------------------------------
     interface IShuffle
    {
        void Shuffle<T>(T[] newKey);
    }
    public abstract class Cryptography : IShuffle
    {
    // -----------------------------------------     FIELD     -------------------------------------------------
        public string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        protected char[] cipherText;
        protected char[] plainText;

    // ----------------------------------------     METHODS     --------------------------------------------------
        protected void NormalizePlaintext(string plaintext)
        {
            string[] RemovingPunctuation = plaintext.Split(' ', ',', '.', '!', '?', ':', ';');
            string text = "";

            for (int i = 0; i < RemovingPunctuation.Length; i++)
                text = text + RemovingPunctuation[i].ToUpper();

            plainText = text.ToCharArray();
        }
        public void Shuffle<T>(T[] newKey)
        {
            for (int i = 0; i < newKey.Length - 2; i++)
            {
                Random rn = new Random();
                int j = rn.Next(i, newKey.Length - 1);
                T aux = newKey[i];
                newKey[i] = newKey[j];
                newKey[j] = aux;
            }
        }
        

        // ----------------------------------------   ABSTRACT METHODS   -------------------------------------------
        public abstract string Encryption();
        public abstract string Decryption();

    }
}
