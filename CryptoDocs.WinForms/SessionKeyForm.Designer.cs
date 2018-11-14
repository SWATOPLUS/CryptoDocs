namespace CryptoDocs.WinForms
{
    partial class SessionKeyForm
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
            this.KeyPairTextBox = new System.Windows.Forms.TextBox();
            this.KeyPairLabel = new System.Windows.Forms.Label();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.GetButton = new System.Windows.Forms.Button();
            this.SessionKeyTextBox = new System.Windows.Forms.TextBox();
            this.SessionKeyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // KeyPairTextBox
            // 
            this.KeyPairTextBox.Location = new System.Drawing.Point(12, 25);
            this.KeyPairTextBox.Multiline = true;
            this.KeyPairTextBox.Name = "KeyPairTextBox";
            this.KeyPairTextBox.Size = new System.Drawing.Size(329, 176);
            this.KeyPairTextBox.TabIndex = 0;
            // 
            // KeyPairLabel
            // 
            this.KeyPairLabel.AutoSize = true;
            this.KeyPairLabel.Location = new System.Drawing.Point(12, 9);
            this.KeyPairLabel.Name = "KeyPairLabel";
            this.KeyPairLabel.Size = new System.Drawing.Size(45, 13);
            this.KeyPairLabel.TabIndex = 1;
            this.KeyPairLabel.Text = "Key pair";
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(12, 207);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(329, 23);
            this.GenerateButton.TabIndex = 2;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // GetButton
            // 
            this.GetButton.Location = new System.Drawing.Point(350, 207);
            this.GetButton.Name = "GetButton";
            this.GetButton.Size = new System.Drawing.Size(264, 23);
            this.GetButton.TabIndex = 4;
            this.GetButton.Text = "Get session key";
            this.GetButton.UseVisualStyleBackColor = true;
            this.GetButton.Click += new System.EventHandler(this.GetButton_Click);
            // 
            // SessionKeyTextBox
            // 
            this.SessionKeyTextBox.Location = new System.Drawing.Point(350, 25);
            this.SessionKeyTextBox.Multiline = true;
            this.SessionKeyTextBox.Name = "SessionKeyTextBox";
            this.SessionKeyTextBox.Size = new System.Drawing.Size(264, 176);
            this.SessionKeyTextBox.TabIndex = 5;
            // 
            // SessionKeyLabel
            // 
            this.SessionKeyLabel.AutoSize = true;
            this.SessionKeyLabel.Location = new System.Drawing.Point(347, 9);
            this.SessionKeyLabel.Name = "SessionKeyLabel";
            this.SessionKeyLabel.Size = new System.Drawing.Size(64, 13);
            this.SessionKeyLabel.TabIndex = 6;
            this.SessionKeyLabel.Text = "Session key";
            // 
            // SessionKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 241);
            this.Controls.Add(this.SessionKeyLabel);
            this.Controls.Add(this.SessionKeyTextBox);
            this.Controls.Add(this.GetButton);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.KeyPairLabel);
            this.Controls.Add(this.KeyPairTextBox);
            this.Name = "SessionKeyForm";
            this.Text = "SessionKeyForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox KeyPairTextBox;
        private System.Windows.Forms.Label KeyPairLabel;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.Button GetButton;
        private System.Windows.Forms.TextBox SessionKeyTextBox;
        private System.Windows.Forms.Label SessionKeyLabel;
    }
}