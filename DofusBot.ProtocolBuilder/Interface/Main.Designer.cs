namespace DofusBot.ProtocolBuilder
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
            this.lblStats = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBulkOutput = new System.Windows.Forms.TextBox();
            this.txtNulkInput = new System.Windows.Forms.TextBox();
            this.pbMain = new System.Windows.Forms.ProgressBar();
            this.btnTranslat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStats
            // 
            this.lblStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStats.AutoSize = true;
            this.lblStats.Location = new System.Drawing.Point(12, 124);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(56, 13);
            this.lblStats.TabIndex = 16;
            this.lblStats.Text = "En attente";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Dossier des fichiers traduits:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Chemin du dossier \"Network\":";
            // 
            // txtBulkOutput
            // 
            this.txtBulkOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBulkOutput.Location = new System.Drawing.Point(164, 38);
            this.txtBulkOutput.Name = "txtBulkOutput";
            this.txtBulkOutput.Size = new System.Drawing.Size(386, 20);
            this.txtBulkOutput.TabIndex = 14;
            this.txtBulkOutput.Text = "C:\\Users\\yovan\\Desktop\\Protocol";
            // 
            // txtNulkInput
            // 
            this.txtNulkInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNulkInput.Location = new System.Drawing.Point(164, 12);
            this.txtNulkInput.Name = "txtNulkInput";
            this.txtNulkInput.Size = new System.Drawing.Size(386, 20);
            this.txtNulkInput.TabIndex = 15;
            this.txtNulkInput.Text = "C:\\Users\\yovan\\Desktop\\DI_Decompiled";
            // 
            // pbMain
            // 
            this.pbMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMain.Location = new System.Drawing.Point(15, 81);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(535, 27);
            this.pbMain.TabIndex = 11;
            // 
            // btnTranslat
            // 
            this.btnTranslat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTranslat.Location = new System.Drawing.Point(475, 124);
            this.btnTranslat.Name = "btnTranslat";
            this.btnTranslat.Size = new System.Drawing.Size(75, 23);
            this.btnTranslat.TabIndex = 10;
            this.btnTranslat.Text = "Traduire";
            this.btnTranslat.UseVisualStyleBackColor = true;
            this.btnTranslat.Click += new System.EventHandler(this.btnTranslat_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 374);
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBulkOutput);
            this.Controls.Add(this.txtNulkInput);
            this.Controls.Add(this.pbMain);
            this.Controls.Add(this.btnTranslat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.RightToLeftLayout = true;
            this.Text = "DofusBot ProtocolBuilder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBulkOutput;
        private System.Windows.Forms.TextBox txtNulkInput;
        private System.Windows.Forms.ProgressBar pbMain;
        private System.Windows.Forms.Button btnTranslat;
    }
}

