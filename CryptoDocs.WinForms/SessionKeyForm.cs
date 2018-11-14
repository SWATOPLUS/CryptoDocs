using System;
using System.Net.Http;
using System.Windows.Forms;
using CryptoDocs.Shared;
using CryptoDocs.Shared.Dto;
using CryptoDocs.Shared.Rsa;

namespace CryptoDocs.WinForms
{
    public partial class SessionKeyForm : Form
    {
        private static readonly IBigIntegerService BigIntegerService = new BigIntegerService();

        private readonly Uri _uri;
        public byte[] SessionKey { get; private set; }
        public RsaKeyPair KeyPair { get; private set; }

        public SessionKeyForm(string url)
        {
            _uri = new Uri(url);
            InitializeComponent();

            SessionKeyLabel.Text = $"Session key from {url}";
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


            using (var client = new HttpClient())
            {
                var url = new Uri(_uri, "api/Data/GetEncryptedSessionKey");

                var sessionKeyEncryptedBase64 = await client.PostJsonAsync<string>(url.AbsoluteUri, KeyPair.PublicKey.ToDto());

                var sessionKeyEncrypted = Convert.FromBase64String(sessionKeyEncryptedBase64);
                SessionKey = KeyPair.PrivateKey.Decrypt(sessionKeyEncrypted);
                SessionKeyTextBox.Text = Convert.ToBase64String(SessionKey);
            }

            DialogResult = DialogResult.OK;
        }
    }
}
