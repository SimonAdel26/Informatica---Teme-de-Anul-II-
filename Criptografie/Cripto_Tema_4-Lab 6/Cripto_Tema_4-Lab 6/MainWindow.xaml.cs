using System.Text;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;



namespace Cripto_Tema_4_Lab_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private byte[] plaintext;
        private byte[] ciphertext;

        private CipherMode ModOperare;
        private PaddingMode PaddingMod;

        private bool InputPath;
        public bool OutputPath;



        public MainWindow()
        {
            InitializeComponent();
            InputPath = false;
            OutputPath = false;

        }

        #region Clik Methods

        private void BtnBrowseInput_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Text files (*.txt)|*.txt";

            if (dialog.ShowDialog() == true)
            {
                txtInputFile.Text = dialog.FileName;
                InputPath = true;
            }

            ReadingFileInByte(txtInputFile.Text);

        }

        private void BtnBrowseOutput_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Text files (*.txt)|*.txt";

            if (dialog.ShowDialog() == true)
            {
                txtOutputFile.Text = dialog.FileName;
                OutputPath = true;
            }
        }

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            if (!InputPath)
            {
                MessageBox.Show("Nu ati incarcat nimic in InputFile!");
                return;
            }

            if (!OutputPath)
            {
                MessageBox.Show("Nu ati introdus fisierul de iesire !");
                return;
            }

            if (cmbAlgoritm.SelectedItem == null)
            {
                MessageBox.Show("Selectati Algoritmul !");
                return;
            }

            if(cmbOperationMode.SelectedItem == null)
            {
                MessageBox.Show("Selectati Modul de Operatie !");
                return;
            }

            if(cmbPadding.SelectedIndex ==-1)
            {
                MessageBox.Show("Selectati Modul de Padding! ");
                return;
            }

            GetOperationMode();
            GetPaddingMode();

            string selectAlforitm =((ComboBoxItem)cmbAlgoritm.SelectedItem).Content.ToString();

  

            switch(selectAlforitm)
            {
                case "AES":
                    AES_Encryption();
                    break;

                case "DES":
                    DES_Encryption();
                    break;

                case "TripleDES":
                    TripleDES_Encryption();
                    break;

                case "RC2":
                    RC2_Encryption();
                    break;

                case "Rijndeal":
                    Rijndael_Encryption();
                    break;

            }


        }

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            if(!InputPath)
            {
                MessageBox.Show("Nu ati introdus fisierul de decriptare !");
                return;
            }

            if (!OutputPath)
            {
                MessageBox.Show("Nu ati introdus fisierul de iesire !");
                return;
            }

            if (cmbAlgoritm.SelectedItem == null)
            {
                MessageBox.Show("Selectati Algoritmul !");
                return;
            }

            if (cmbOperationMode.SelectedItem == null)
            {
                MessageBox.Show("Selectati Modul de Operatie !");
                return;
            }

            if (cmbPadding.SelectedIndex == -1)
            {
                MessageBox.Show("Selectati Modul de Padding! ");
                return;
            }

            if(KeyText.Text==null)
            {
                MessageBox.Show("Nu ati introdus cheia pentru decriptare !");
                return;
            }

            if(IVText.Text==null)
            {
                MessageBox.Show("Nu ati introdus IV pentru decriptare !");
                return;
            }

            GetOperationMode();
            GetPaddingMode();

            SymmetricAlgorithm algorithm;

            string selectalgoritm = ((ComboBoxItem)cmbAlgoritm.SelectedItem).Content.ToString();

            switch(selectalgoritm)
            {
                case "AES":
                    AES_Decryption();
                    break;

                case "DES":
                    DES_Decryption();
                    break;

                case "TripleDES":
                    TripleDES_Decryption();
                    break;

                case "RC2":
                    RC2_Decryption();
                    break;

                case "Rijndael":
                    Rijndael_Decryption();
                    break;
            }

        }

        #endregion

        #region Algoritm Endryption 
        private void AES_Encryption()
        {
            Aes aes = Aes.Create();

            aes.Mode = ModOperare;
            aes.Padding = PaddingMod;

            aes.GenerateKey();
            aes.GenerateIV();

            ICryptoTransform encryptor = aes.CreateEncryptor();

            WriteFileInByteAndTransformFinalBlock(encryptor);

            KeyText.Text = Convert.ToHexString(aes.Key);
            IVText.Text = Convert.ToHexString(aes.IV);

        }

        private void DES_Encryption()
        {
            DES des = DES.Create();

            des.Mode = ModOperare;
            des.Padding = PaddingMod;

            des.GenerateKey();
            des.GenerateIV();

            ICryptoTransform encryptor =
            des.CreateEncryptor();

            WriteFileInByteAndTransformFinalBlock(encryptor);

            KeyText.Text = Convert.ToHexString(des.Key);
            IVText.Text = Convert.ToHexString(des.IV);
        }

        private void TripleDES_Encryption()
        {
            TripleDES trpleDes = TripleDES.Create();

            trpleDes.Mode = ModOperare;
            trpleDes.Padding = PaddingMod;

            trpleDes.GenerateKey();
            trpleDes.GenerateIV();

            ICryptoTransform encryptor =trpleDes.CreateEncryptor();

            WriteFileInByteAndTransformFinalBlock(encryptor);

            KeyText.Text = Convert.ToHexString(trpleDes.Key);
            IVText.Text = Convert.ToHexString(trpleDes.IV   );

        }

        private void RC2_Encryption()
        {
            RC2 rc2 = RC2.Create();

            rc2.Mode = ModOperare;
            rc2.Padding = PaddingMod;

            rc2.GenerateKey();
            rc2.GenerateIV();

            ICryptoTransform encryptor = rc2.CreateEncryptor();

            WriteFileInByteAndTransformFinalBlock(encryptor);

            KeyText.Text = Convert.ToHexString(rc2.Key);
            IVText.Text = Convert.ToHexString(rc2.IV);

        }

        private void Rijndael_Encryption()
        {
            Rijndael rij = Rijndael.Create();

            rij.Mode = ModOperare;
            rij.Padding = PaddingMod;

            rij.GenerateKey();
            rij.GenerateIV();

            ICryptoTransform encryptor = rij.CreateEncryptor();

            WriteFileInByteAndTransformFinalBlock(encryptor);

            KeyText.Text = Convert.ToHexString(rij.Key);
            IVText.Text = Convert.ToHexString(rij.IV);

        }

        #endregion

        #region Algoritm Decryption

        private void AES_Decryption()
        {
            Aes aes = Aes.Create();

            aes.Mode = ModOperare;
            aes.Padding = PaddingMod;

            aes.Key = Convert.FromHexString(KeyText.Text);
            aes.IV = Convert.FromHexString(IVText.Text);

            ICryptoTransform decryptor =aes.CreateDecryptor();

            WriteFileInByteAndTransformFinalBlock(decryptor);

        }

        private void DES_Decryption()
        {
            DES des = DES.Create();

            des.Mode = ModOperare;
            des.Padding = PaddingMod;

            des.Key = Convert.FromHexString(KeyText.Text);
            des.IV = Convert.FromHexString(IVText.Text);

            ICryptoTransform decryptor = des.CreateDecryptor();

            WriteFileInByteAndTransformFinalBlock(decryptor);

        }

        private void TripleDES_Decryption()
        {
            TripleDES tripleDES = TripleDES.Create();

            tripleDES.Mode = ModOperare;
            tripleDES.Padding = PaddingMod;

            tripleDES.Key = Convert.FromHexString(KeyText.Text);
            tripleDES.IV = Convert.FromHexString(IVText.Text);

            ICryptoTransform decryptor = tripleDES.CreateDecryptor();

            WriteFileInByteAndTransformFinalBlock(decryptor);
        }

        private void RC2_Decryption()
        {
            RC2 rc2 = RC2.Create();

            rc2.Mode = ModOperare;
            rc2.Padding = PaddingMod;

            rc2.Key = Convert.FromHexString(KeyText.Text);
            rc2.Key = Convert.FromHexString(IVText.Text);

            ICryptoTransform decryptor = rc2.CreateDecryptor();

            WriteFileInByteAndTransformFinalBlock(decryptor);

        }

        private void Rijndael_Decryption()
        {
            Rijndael rij = Rijndael.Create();

            rij.Mode = ModOperare;
            rij.Padding = PaddingMod;

            rij.Key = Convert.FromHexString(KeyText.Text);
            rij.Key = Convert.FromHexString(IVText.Text);

            ICryptoTransform decryptor = rij.CreateDecryptor();

            WriteFileInByteAndTransformFinalBlock(decryptor);

        }

        #endregion

        #region Methods

        private void ReadingFileInByte(string filename)
        {
           plaintext = File.ReadAllBytes(filename);
        }

        private void WriteFileInByteAndTransformFinalBlock(ICryptoTransform crypt)
        {
            byte[] encrypted =
               crypt.TransformFinalBlock(
                   plaintext,
                   0,
                   plaintext.Length);

            File.WriteAllBytes(txtOutputFile.Text, encrypted);
        }

        private void GetOperationMode()
        {
            string mod =((ComboBoxItem)cmbOperationMode.SelectedItem).Content.ToString();

            ModOperare = Enum.Parse<CipherMode>(mod);
        }

        private void GetPaddingMode()
        {
            string mod = ((ComboBoxItem)cmbPadding.SelectedItem).Content.ToString();

            PaddingMod = Enum.Parse<PaddingMode>(mod);
        }

        #endregion
    }
}