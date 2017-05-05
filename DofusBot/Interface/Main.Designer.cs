namespace DofusBot.Interface
{
    partial class Main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.accountPasswordLabel = new MaterialSkin.Controls.MaterialLabel();
            this.accountNameLabel = new MaterialSkin.Controls.MaterialLabel();
            this.autoConnectCheckBox = new MaterialSkin.Controls.MaterialCheckBox();
            this.accountPasswordTextField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.accountNameTextField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.podsLabel = new MaterialSkin.Controls.MaterialLabel();
            this.connectionButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.kamasShowLabel = new MaterialSkin.Controls.MaterialLabel();
            this.kamasLabel = new MaterialSkin.Controls.MaterialLabel();
            this.currentMapLabel = new MaterialSkin.Controls.MaterialLabel();
            this.currentMapIdLabel = new MaterialSkin.Controls.MaterialLabel();
            this.PodsProgress = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PodsProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.logTextBox.Location = new System.Drawing.Point(12, 132);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(889, 544);
            this.logTextBox.TabIndex = 22;
            this.logTextBox.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.accountPasswordLabel);
            this.groupBox1.Controls.Add(this.accountNameLabel);
            this.groupBox1.Controls.Add(this.autoConnectCheckBox);
            this.groupBox1.Controls.Add(this.accountPasswordTextField);
            this.groupBox1.Controls.Add(this.accountNameTextField);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(726, 54);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Identifiants";
            // 
            // accountPasswordLabel
            // 
            this.accountPasswordLabel.AutoSize = true;
            this.accountPasswordLabel.Depth = 0;
            this.accountPasswordLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.accountPasswordLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.accountPasswordLabel.Location = new System.Drawing.Point(309, 18);
            this.accountPasswordLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.accountPasswordLabel.Name = "accountPasswordLabel";
            this.accountPasswordLabel.Size = new System.Drawing.Size(100, 19);
            this.accountPasswordLabel.TabIndex = 19;
            this.accountPasswordLabel.Text = "Mot de passe";
            // 
            // accountNameLabel
            // 
            this.accountNameLabel.AutoSize = true;
            this.accountNameLabel.Depth = 0;
            this.accountNameLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.accountNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.accountNameLabel.Location = new System.Drawing.Point(6, 18);
            this.accountNameLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.accountNameLabel.Name = "accountNameLabel";
            this.accountNameLabel.Size = new System.Drawing.Size(119, 19);
            this.accountNameLabel.TabIndex = 18;
            this.accountNameLabel.Text = "Nom de Compte";
            // 
            // autoConnectCheckBox
            // 
            this.autoConnectCheckBox.AutoSize = true;
            this.autoConnectCheckBox.Depth = 0;
            this.autoConnectCheckBox.Font = new System.Drawing.Font("Roboto", 10F);
            this.autoConnectCheckBox.Location = new System.Drawing.Point(592, 14);
            this.autoConnectCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.autoConnectCheckBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.autoConnectCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.autoConnectCheckBox.Name = "autoConnectCheckBox";
            this.autoConnectCheckBox.Ripple = true;
            this.autoConnectCheckBox.Size = new System.Drawing.Size(110, 30);
            this.autoConnectCheckBox.TabIndex = 17;
            this.autoConnectCheckBox.Text = "AutoConnect";
            this.autoConnectCheckBox.UseVisualStyleBackColor = true;
            // 
            // accountPasswordTextField
            // 
            this.accountPasswordTextField.Depth = 0;
            this.accountPasswordTextField.Hint = "";
            this.accountPasswordTextField.Location = new System.Drawing.Point(415, 18);
            this.accountPasswordTextField.MouseState = MaterialSkin.MouseState.HOVER;
            this.accountPasswordTextField.Name = "accountPasswordTextField";
            this.accountPasswordTextField.PasswordChar = '*';
            this.accountPasswordTextField.SelectedText = "";
            this.accountPasswordTextField.SelectionLength = 0;
            this.accountPasswordTextField.SelectionStart = 0;
            this.accountPasswordTextField.Size = new System.Drawing.Size(174, 23);
            this.accountPasswordTextField.TabIndex = 5;
            this.accountPasswordTextField.UseSystemPasswordChar = false;
            // 
            // accountNameTextField
            // 
            this.accountNameTextField.Depth = 0;
            this.accountNameTextField.Hint = "";
            this.accountNameTextField.Location = new System.Drawing.Point(144, 18);
            this.accountNameTextField.MouseState = MaterialSkin.MouseState.HOVER;
            this.accountNameTextField.Name = "accountNameTextField";
            this.accountNameTextField.PasswordChar = '\0';
            this.accountNameTextField.SelectedText = "";
            this.accountNameTextField.SelectionLength = 0;
            this.accountNameTextField.SelectionStart = 0;
            this.accountNameTextField.Size = new System.Drawing.Size(159, 23);
            this.accountNameTextField.TabIndex = 4;
            this.accountNameTextField.UseSystemPasswordChar = false;
            // 
            // podsLabel
            // 
            this.podsLabel.AutoSize = true;
            this.podsLabel.Depth = 0;
            this.podsLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.podsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.podsLabel.Location = new System.Drawing.Point(857, 686);
            this.podsLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.podsLabel.Name = "podsLabel";
            this.podsLabel.Size = new System.Drawing.Size(43, 19);
            this.podsLabel.TabIndex = 25;
            this.podsLabel.Text = "Pods";
            // 
            // connectionButton
            // 
            this.connectionButton.Depth = 0;
            this.connectionButton.Location = new System.Drawing.Point(776, 72);
            this.connectionButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.connectionButton.Name = "connectionButton";
            this.connectionButton.Primary = true;
            this.connectionButton.Size = new System.Drawing.Size(125, 54);
            this.connectionButton.TabIndex = 24;
            this.connectionButton.Text = "Connexion";
            this.connectionButton.UseVisualStyleBackColor = true;
            this.connectionButton.Click += new System.EventHandler(this.connectionButton_Click);
            // 
            // kamasShowLabel
            // 
            this.kamasShowLabel.AutoSize = true;
            this.kamasShowLabel.Depth = 0;
            this.kamasShowLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.kamasShowLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.kamasShowLabel.Location = new System.Drawing.Point(643, 686);
            this.kamasShowLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.kamasShowLabel.Name = "kamasShowLabel";
            this.kamasShowLabel.Size = new System.Drawing.Size(55, 19);
            this.kamasShowLabel.TabIndex = 26;
            this.kamasShowLabel.Text = "Kamas";
            // 
            // kamasLabel
            // 
            this.kamasLabel.AutoSize = true;
            this.kamasLabel.Depth = 0;
            this.kamasLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.kamasLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.kamasLabel.Location = new System.Drawing.Point(620, 686);
            this.kamasLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.kamasLabel.Name = "kamasLabel";
            this.kamasLabel.Size = new System.Drawing.Size(17, 19);
            this.kamasLabel.TabIndex = 27;
            this.kamasLabel.Text = "0";
            // 
            // currentMapLabel
            // 
            this.currentMapLabel.AutoSize = true;
            this.currentMapLabel.Depth = 0;
            this.currentMapLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.currentMapLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.currentMapLabel.Location = new System.Drawing.Point(18, 686);
            this.currentMapLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.currentMapLabel.Name = "currentMapLabel";
            this.currentMapLabel.Size = new System.Drawing.Size(97, 19);
            this.currentMapLabel.TabIndex = 28;
            this.currentMapLabel.Text = "Map Actuelle";
            // 
            // currentMapIdLabel
            // 
            this.currentMapIdLabel.AutoSize = true;
            this.currentMapIdLabel.Depth = 0;
            this.currentMapIdLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.currentMapIdLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.currentMapIdLabel.Location = new System.Drawing.Point(132, 686);
            this.currentMapIdLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.currentMapIdLabel.Name = "currentMapIdLabel";
            this.currentMapIdLabel.Size = new System.Drawing.Size(17, 19);
            this.currentMapIdLabel.TabIndex = 29;
            this.currentMapIdLabel.Text = "0";
            // 
            // PodsProgress
            // 
            this.PodsProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.PodsProgress.Location = new System.Drawing.Point(707, 686);
            this.PodsProgress.Name = "PodsProgress";
            this.PodsProgress.Size = new System.Drawing.Size(144, 19);
            this.PodsProgress.TabIndex = 30;
            this.PodsProgress.TabStop = false;
            this.PodsProgress.Paint += new System.Windows.Forms.PaintEventHandler(this.PodsProgress_Paint);
            this.PodsProgress.MouseHover += new System.EventHandler(this.MouseHover_PodsProgress);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(912, 716);
            this.Controls.Add(this.PodsProgress);
            this.Controls.Add(this.currentMapIdLabel);
            this.Controls.Add(this.currentMapLabel);
            this.Controls.Add(this.kamasLabel);
            this.Controls.Add(this.kamasShowLabel);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.podsLabel);
            this.Controls.Add(this.connectionButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "DofusBot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PodsProgress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private MaterialSkin.Controls.MaterialLabel accountPasswordLabel;
        private MaterialSkin.Controls.MaterialLabel accountNameLabel;
        private MaterialSkin.Controls.MaterialCheckBox autoConnectCheckBox;
        private MaterialSkin.Controls.MaterialSingleLineTextField accountPasswordTextField;
        private MaterialSkin.Controls.MaterialSingleLineTextField accountNameTextField;
        private MaterialSkin.Controls.MaterialLabel podsLabel;
        private MaterialSkin.Controls.MaterialRaisedButton connectionButton;
        private MaterialSkin.Controls.MaterialLabel kamasShowLabel;
        private MaterialSkin.Controls.MaterialLabel kamasLabel;
        private MaterialSkin.Controls.MaterialLabel currentMapLabel;
        private MaterialSkin.Controls.MaterialLabel currentMapIdLabel;
        private System.Windows.Forms.PictureBox PodsProgress;
    }
}

