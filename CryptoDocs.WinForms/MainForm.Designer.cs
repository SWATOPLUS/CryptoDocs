namespace CryptoDocs.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SaveButton = new System.Windows.Forms.Button();
            this.FileNameTextBox = new System.Windows.Forms.TextBox();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.MainTextBox = new System.Windows.Forms.TextBox();
            this.ServerUrlTextBox = new System.Windows.Forms.TextBox();
            this.ServerUrlLabel = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(12, 415);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // FileNameTextBox
            // 
            this.FileNameTextBox.Location = new System.Drawing.Point(70, 12);
            this.FileNameTextBox.Name = "FileNameTextBox";
            this.FileNameTextBox.Size = new System.Drawing.Size(278, 20);
            this.FileNameTextBox.TabIndex = 1;
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Location = new System.Drawing.Point(12, 15);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(52, 13);
            this.FileNameLabel.TabIndex = 2;
            this.FileNameLabel.Text = "File name";
            // 
            // MainTextBox
            // 
            this.MainTextBox.Location = new System.Drawing.Point(12, 56);
            this.MainTextBox.Multiline = true;
            this.MainTextBox.Name = "MainTextBox";
            this.MainTextBox.Size = new System.Drawing.Size(776, 353);
            this.MainTextBox.TabIndex = 3;
            // 
            // ServerUrlTextBox
            // 
            this.ServerUrlTextBox.Location = new System.Drawing.Point(446, 12);
            this.ServerUrlTextBox.Name = "ServerUrlTextBox";
            this.ServerUrlTextBox.Size = new System.Drawing.Size(244, 20);
            this.ServerUrlTextBox.TabIndex = 4;
            this.ServerUrlTextBox.Text = "http://localhost:50487";
            this.ServerUrlTextBox.TextChanged += new System.EventHandler(this.ServerUrlTextBox_TextChanged);
            // 
            // ServerUrlLabel
            // 
            this.ServerUrlLabel.AutoSize = true;
            this.ServerUrlLabel.Location = new System.Drawing.Point(377, 15);
            this.ServerUrlLabel.Name = "ServerUrlLabel";
            this.ServerUrlLabel.Size = new System.Drawing.Size(63, 13);
            this.ServerUrlLabel.TabIndex = 5;
            this.ServerUrlLabel.Text = "Server URL";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(696, 10);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(92, 23);
            this.ConnectButton.TabIndex = 6;
            this.ConnectButton.Text = "Get session key";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(93, 415);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 7;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.ServerUrlLabel);
            this.Controls.Add(this.ServerUrlTextBox);
            this.Controls.Add(this.MainTextBox);
            this.Controls.Add(this.FileNameLabel);
            this.Controls.Add(this.FileNameTextBox);
            this.Controls.Add(this.SaveButton);
            this.Name = "MainForm";
            this.Text = "CryptoDocs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox FileNameTextBox;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.TextBox MainTextBox;
        private System.Windows.Forms.TextBox ServerUrlTextBox;
        private System.Windows.Forms.Label ServerUrlLabel;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button LoadButton;
    }
}

