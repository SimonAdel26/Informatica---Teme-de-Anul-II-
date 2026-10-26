using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cripto_Tema_3_Lab_4
{
    internal abstract class Cryptography
    {
        #region Field

        protected char[] plainText;
        protected char[] cipherText;

        public List<char> alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ".ToList();

        #endregion


        #region Methods
        protected static char[] NormalizePlaintext(string plainText)
        {
            return plainText.
                Where(char.IsLetter).
                Select(char.ToUpper).
                ToArray();
        }
        #endregion

        #region Abstract methods 

        public abstract void Enrcypt();

        public abstract void Decrypt();

        #endregion

    }
}
