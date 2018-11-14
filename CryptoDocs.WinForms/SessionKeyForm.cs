using System;
using System.Net.Http;
using System.Windows.Forms;
using CryptoDocs.Abstractions;
using CryptoDocs.RestClient;
using CryptoDocs.Shared;
using CryptoDocs.Shared.Dto;
using CryptoDocs.Shared.Rsa;

namespace CryptoDocs.WinForms
{
    public partial class SessionKeyForm : Form
    {
        private readonly string _baseUrl;
        private static readonly IBigIntegerService BigIntegerService = new BigIntegerService();
        private static readonly HttpClient HttpClient = new HttpClient();

        public ICryptoDocsService CryptoDocsService { get; private set; }


        public byte[] SessionKey { get; private set; }
        public RsaKeyPair KeyPair { get; private set; }

        public SessionKeyForm(string baseUrl)
        {
            _baseUrl = baseUrl;
            InitializeComponent();

            SessionKeyLabel.Text = $"Session key from {baseUrl}";
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            var p = BigIntegerService.GetPrime();
            var q = BigIntegerService.GetPrime();
            KeyPairTextBox.Text = RsaKeyPair.Generate(p, q).ToString();
        }

        private async void GetButton_Click(object sender, EventArgs e)
        {
            try
            {
                KeyPair = new RsaKeyPair(KeyPairTextBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid key pair", "Error");
                return;
            }

            CryptoDocsService = new CryptoDocsServiceClient(HttpClient, _baseUrl);

            var sessionKeyEncrypted = await CryptoDocsService.GetEncryptedSessionKeyAsync(KeyPair.PublicKey.ToDto());
            SessionKey = KeyPair.PrivateKey.Decrypt(sessionKeyEncrypted);
            SessionKeyTextBox.Text = Convert.ToBase64String(SessionKey);

            DialogResult = DialogResult.OK;
        }
    }
}
