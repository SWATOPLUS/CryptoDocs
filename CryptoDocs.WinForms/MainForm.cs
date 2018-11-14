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
using CryptoDocs.Abstractions;
using CryptoDocs.RestClient;
using CryptoDocs.Shared.Dto;
using CryptoDocs.Shared.Rsa;
using CryptoDocs.Shared.Symmetric;

namespace CryptoDocs.WinForms
{
    public partial class MainForm : Form
    {
        private readonly IDataCryptoProvider _cryptoProvider;
        private byte[] SessionKey { get; set; }

        private RsaKeyPair KeyPair { get; set; }

        private ICryptoDocsService _cryptoDocsService;
        private string CryptoAlgorithm { get; } = Abstractions.CryptoAlgorithm.SerpentCbc;

        public MainForm()
        {
            InitializeComponent();
            _cryptoProvider = new CbcCryptoProvider(new SerpentCryptoProvider());
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
                _cryptoDocsService = form.CryptoDocsService;
            }
        }

        private bool ValidateAction()
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
            if (ValidateAction())
            {
                return;
            }

            var data = Encoding.UTF8.GetBytes(MainTextBox.Text ?? string.Empty);
            var encryptedContent = _cryptoProvider.Encrypt(data, SessionKey);

            await _cryptoDocsService.SetEncryptedFileAsync(KeyPair.PublicKey.ToDto(), CryptoAlgorithm,
                FileNameTextBox.Text, encryptedContent);
        }

        private async void LoadButton_Click(object sender, EventArgs e)
        {
            if (ValidateAction())
            {
                return;
            }

            var encryptedContent = await _cryptoDocsService.GetEncryptedFileAsync(KeyPair.PublicKey.ToDto(),
                CryptoAlgorithm, FileNameTextBox.Text);

            var data = _cryptoProvider.Decrypt(encryptedContent, SessionKey);
            MainTextBox.Text = Encoding.UTF8.GetString(data);
        }
    }
}
