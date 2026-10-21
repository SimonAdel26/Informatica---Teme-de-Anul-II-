using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cripto_Tema_2_Lab_3
{
    internal class JeffersonDisk : Cryptography
    {
    // -----------------------------------      FIELD       ---------------------------------------------
        private int numberEncryption;
        private Dictionary<int , char[] > Discs = new Dictionary<int , char[] >();

    // -----------------------------------      CONSTRUCTOR     -------------------------------------------
        public JeffersonDisk(string plaintext, int numberEncryption)
        {
            this.numberEncryption = numberEncryption;
            NormalizePlaintext(plaintext);
            cipherText = new char[plainText.Length];
            GenerateDiscs();
            ShuffleDiscs();
        }

    // -----------------------------------      METHODS     ------------------------------------------------
        private void GenerateDiscs()
        {
            for (int i = 0; i < 36; i++)
            {
                char[] Key = alphabet.ToCharArray();
                Shuffle(Key);
                Discs.Add(i, Key);
            }
        }
        private void ShuffleDiscs()
        {
            int[] index = Discs.Keys.ToArray();
            Shuffle(index);

            Dictionary<int , char[]> TempKey = new Dictionary<int , char[] >();

            for (int i = 0; i < 36; i++)
                TempKey[i] = Discs[index[i]];

            for (int i = 0;i <36;i++)
                Discs[i] = TempKey[i];
        }
        private char GetLetter(int pos , char letter , int indexCrypt)
        {
            for(int i = 0; i < Discs[i].Length; i++) 
                if(letter == Discs[pos][i])
                    return Discs[pos][(i + indexCrypt +26)%26];

            throw new Exception("Nu s-a gasit litera dintr-un alfabet!");
        }

    // ----------------------------------       OVERRIDEN METHODS       ---------------------------------------
        public override string Encryption()
        {
            for(int i=0;i<plainText.Length;i++)
            {
                cipherText[i] = GetLetter(i%36 , plainText[i],numberEncryption);
            }

            return new string(cipherText);
        }

        public override string Decryption()
        {
            char[] DecryptText = new char[cipherText.Length];

            for(int i=0;i<cipherText.Length;i++)
            {
                DecryptText[i] = GetLetter(i % 36, cipherText[i], numberEncryption * (-1));
            }

            return new string(DecryptText);
        }
    }
}
