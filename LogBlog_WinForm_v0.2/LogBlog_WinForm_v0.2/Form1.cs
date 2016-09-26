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
    public partial class Output : Form
    {
        public Output()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog.ShowDialog();
            string logAdress = openFileDialog.FileName;
            this.fileNameBox.Text = logAdress;
            // Initiate the log reader
            LogReader newLog = new LogReader(@logAdress);
            Console.WriteLine(result); // <-- For debugging use
        }
    }
}
