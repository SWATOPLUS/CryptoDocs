using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoDocs.Shared.Dto;
using CryptoDocs.Shared.Rsa;
using CryptoDocs.Shared.Symmetric;

namespace CryptoDocs.WinForms
{
    public partial class MainForm : Form
    {
        private readonly CbcCryptoProvider _cryptoProvider;
        private byte[] SessionKey { get; set; }

        private RsaKeyPair KeyPair { get; set; }

        public MainForm()
        {
            InitializeComponent();
            _cryptoProvider = new CbcCryptoProvider(new IdeaCryptoProvider());
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            var url = ServerUrlTextBox.Text;

            var form = new SessionKeyForm(url);

            var result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                SessionKey = form.SessionKey;
                KeyPair = form.KeyPair;
            }
        }

        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(FileNameTextBox.Text))
            {
                MessageBox.Show("Enter file name", "Error");
                return true;
            }

            if (SessionKey == null)
            {
                MessageBox.Show("Get session key", "Error");
                return true;
            }

            return false;
        }

        private void ServerUrlTextBox_TextChanged(object sender, EventArgs e)
        {
            SessionKey = null;
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                return;
            }

            using (var client = new HttpClient())
            {
                var data = Encoding.UTF8.GetBytes(MainTextBox.Text ?? string.Empty);

                var encryptedContent = _cryptoProvider.Encrypt(data, SessionKey);
                var encryptedContentBase64 = Convert.ToBase64String(encryptedContent);
                var url = new Uri(new Uri(ServerUrlTextBox.Text), $"api/Data/SetFile/{FileNameTextBox.Text}");
                await client.PostJsonAsync<bool>(url.AbsoluteUri, new FileRequest
                {
                    EncryptedContentBase64 = encryptedContentBase64,
                    PublicKeyDto = KeyPair.PublicKey.ToDto()
                });
            }

        }

        private async void LoadButton_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                return;
            }

            using (var client = new HttpClient())
            {
                var url = new Uri(new Uri(ServerUrlTextBox.Text), $"api/Data/GetFile/{FileNameTextBox.Text}");
                var encryptedContentBase64 = await client.PostJsonAsync<string>(url.AbsoluteUri, KeyPair.PublicKey.ToDto());
                var encryptedContent = Convert.FromBase64String(encryptedContentBase64);
                var data = _cryptoProvider.Decrypt(encryptedContent, SessionKey);
                MainTextBox.Text = Encoding.UTF8.GetString(data);
            }
        }
    }
}
