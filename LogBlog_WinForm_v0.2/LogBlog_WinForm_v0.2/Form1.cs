using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogBlog_WinForm_v0._2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog.ShowDialog();
            string logAdress = openFileDialog.FileName;
            UpdateFileNameBox(logAdress);
            
            // Initiate the log reader
            LogReader newLog = new LogReader(logAdress, this);
            Console.WriteLine(result); // <-- For debugging use
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void UpdateOutput (String text)
        {
            if (this.outputBox.InvokeRequired)
            {
                //I am not on the main UI thread
                Invoke((MethodInvoker)delegate { this.UpdateOutput(text); });
            }
            else
            {
                this.outputBox.AppendText(text);
            }
        }

        public void UpdateFileNameBox(String text)
        {
            this.fileNameBox.AppendText(text);
        }
    }
}
