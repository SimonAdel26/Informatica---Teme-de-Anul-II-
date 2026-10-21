using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cripto_Tema_2_Lab_3
{
   
    public class Polialphabet : Cryptography
    {
    // -----------------------------------------    FIELD   ---------------------------------------------------
        private int n;
        private Dictionary<int, Dictionary<char, char>> Keys = new Dictionary<int, Dictionary<char, char>>();

        // -----------------------------------------    CONSTRUCTOR     -------------------------------------------------

        public Polialphabet(int n, string plaintext )
        {
            this.n = n;
            NormalizePlaintext( plaintext );
            GenerateNKeys();
        }

        // -------------------------------------------     METHODS     ---------------------------------------------------
        private void GenerateNKeys()
        {
            for (int indexKey = 0; indexKey < n; indexKey++)
            {
                char[] newKey = alphabet.ToCharArray();
                Dictionary<char, char> PartKey = new Dictionary<char, char>();

                if (Keys.Count == 0)
                {
                    Shuffle(newKey);
                    for (int i = 0; i < newKey.Length; i++)
                        PartKey[alphabet[i]] = newKey[i];
                }
                else
                {
                    bool DoubleDictionary = false;

                    while (!DoubleDictionary)
                    {
                        Shuffle(newKey);

                        PartKey = new Dictionary<char, char>();
                        for (int i = 0; i < newKey.Length; i++)
                            PartKey[alphabet[i]] = newKey[i];

                        for (int i = 0; i < Keys.Count; i++)
                            if (DictionarsEquality(Keys[i], PartKey) == false)
                                DoubleDictionary = true;
                    }
                }
                Keys[indexKey] = PartKey;
            }
        }

        protected bool DictionarsEquality(Dictionary<char, char> d1, Dictionary<char, char> d2)
        {
            int nr = 0;
            foreach (var k1 in d1)
                if (k1.Value == d2[k1.Key])
                    nr++;

            if (nr == 26)
                return true;
            else
                return false;
        }

        private char GetKeyByValue(Dictionary<char,char> d , char value)
        {
            foreach( var  k in d)
                if (k.Value == value)
                    return k.Key;

            throw new Exception("Nu s-a gasit valoarea din dictionar !");
        }
       
    // ------------------------------------------   OVERRIDEN METHODS   --------------------------------------------
        public override string Encryption()
        {
            cipherText = new char[plainText.Length];

            for (int i = 0; i < plainText.Length; i++)
                 cipherText[i] = Keys[i % n][plainText[i]];

            return new string(cipherText);
        }

        public override string Decryption()
        {
            char[] DecryptText = new char[cipherText.Length];

            for(int i = 0;i<cipherText.Length;i++)
            {
                DecryptText[i] = GetKeyByValue(Keys[i%n], cipherText[i]);
            }

            return new string(DecryptText);
        }
    }
}
