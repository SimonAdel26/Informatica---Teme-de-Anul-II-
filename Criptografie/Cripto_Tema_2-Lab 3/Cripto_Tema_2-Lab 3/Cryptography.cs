using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cripto_Tema_2_Lab_3
{
   public abstract class Cryptography
    {
    // -----------------------------------------     FIELD     -------------------------------------------------
        public string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
       
    // ----------------------------------------   ABSTRACT METHODS   -------------------------------------------
        public abstract string Encryption();
        public abstract string Decryption();

    }
}
