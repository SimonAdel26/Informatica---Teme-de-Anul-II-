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

            plaintext = File.ReadAllBytes(dialog.FileName);

        }

        private void BtnBrowseOutput_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Text files (*.txt)|*.txt";
            dialog.CheckFileExists = false;

            if (dialog.ShowDialog() == true)
            {
                txtOutputFile.Text = dialog.FileName;
                OutputPath = true;
            }
        }


        private void cmbOperationMode_SelectionChanged(object sender , SelectionChangedEventArgs e)
        {
            if (cmbOperationMode.SelectedItem != null)
            {
                string mod = ((ComboBoxItem)cmbOperationMode.SelectedItem).Content.ToString();

                ModOperare = Enum.Parse<CipherMode>(mod);
            }
        }

        private void cmbPadding_SelectionChanged(object sender , SelectionChangedEventArgs e)
        {
            if (cmbPadding.SelectedItem != null)
            {
                string mod =
                    ((ComboBoxItem)cmbPadding.SelectedItem)
                    .Content.ToString();

                PaddingMod = Enum.Parse<PaddingMode>(mod);
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

            string selectAlforitm =((ComboBoxItem)cmbAlgoritm.SelectedItem).Content.ToString();

            SymmetricAlgorithm alg;


            switch(selectAlforitm)
            {
                case "AES":
                    alg = Aes.Create();
                    Encryption(alg);
                    break;

                case "DES":
                    alg = DES.Create();
                    Encryption(alg);
                    break;

                case "TripleDES":
                    alg = TripleDES.Create();
                    Encryption(alg);
                    break;

                case "RC2":
                    alg = RC2.Create();
                    Encryption(alg);
                    break;

                case "Rijndael":
                    alg = Rijndael.Create();
                    Encryption(alg);
                    break;

            }

            MessageBox.Show("S-a realizat criptarea textului .");

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

            string selectalgoritm = ((ComboBoxItem)cmbAlgoritm.SelectedItem).Content.ToString();

            SymmetricAlgorithm alg;

            switch (selectalgoritm)
            {
                case "AES":
                    alg = Aes.Create();
                    Decryption(alg);
                    break;

                case "DES":
                    alg = DES.Create();
                    Decryption(alg);
                    break;

                case "TripleDES":
                    alg = TripleDES.Create();
                    Decryption(alg);
                    break;

                case "RC2":
                    alg = RC2.Create();
                    Decryption(alg);
                    break;

                case "Rijndael":
                    alg = Rijndael.Create();
                    Decryption(alg);
                    break;
            }

            MessageBox.Show("S-a realizat decriptarea textului !");
        }

        #endregion

        #region Algoritm Endryption 
        private void Encryption(SymmetricAlgorithm algorithm)
        {
            algorithm.Mode = ModOperare;
            algorithm.Padding = PaddingMod;

            ICryptoTransform encryptor = algorithm.CreateEncryptor();

            byte[] encrypted =
               encryptor.TransformFinalBlock(
                   plaintext,
                   0,
                   plaintext.Length);

            File.WriteAllBytes(txtOutputFile.Text, encrypted);

            KeyText.Text = Convert.ToHexString(algorithm.Key);
            IVText.Text = Convert.ToHexString(algorithm.IV);
        }
        #endregion

        #region Algoritm Decryption

        private void Decryption(SymmetricAlgorithm algorithm)
        {
            algorithm.Mode = ModOperare;
            algorithm.Padding = PaddingMod;

            algorithm.Key = Convert.FromHexString(KeyText.Text);
            algorithm.IV = Convert.FromHexString(IVText.Text);

            ICryptoTransform decryptor = algorithm.CreateDecryptor();

            byte[] encrypted =
                decryptor.TransformFinalBlock(
                    plaintext,
                    0,
                    plaintext.Length);

            File.WriteAllBytes(txtOutputFile.Text, encrypted);
        }

        
        #endregion


    }
}