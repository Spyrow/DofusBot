using DofusBot.ProtocolBuilder.CodeTraductor.BulkGenerator;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace DofusBot.ProtocolBuilder
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        #region Interface

        private void btnTranslat_Click(object sender, EventArgs e)
        {
            new Thread(BulkTranslat).Start();
        }

        #endregion

        #region Methode

        private void BulkTranslat()
        {
            if (Directory.Exists(txtNulkInput.Text) && Directory.Exists(txtBulkOutput.Text))
            {
                BulkGenerator.FileTranslated += gen_FileTranslated;
                BulkGenerator.LoadInfo += gen_LoadInfo;
                BulkGenerator.StatsChang += BulkGenerator_StatsChang;
                BulkGenerator.GenerateDirectory(txtNulkInput.Text, txtBulkOutput.Text);
            }
            else
                MessageBox.Show("no valid path");
        }

        void BulkGenerator_StatsChang(string obj)
        {
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                lblStats.Text = obj;
            });
        }

        private void gen_FileTranslated(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                pbMain.Value += 1;
            });
        }

        void gen_LoadInfo(object sender, LoadInfoEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                pbMain.Maximum = e.FilesCount;
                pbMain.Value = 0;
            });
        }


        #endregion
    }
}
