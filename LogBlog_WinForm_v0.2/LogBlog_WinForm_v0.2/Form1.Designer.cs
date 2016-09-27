namespace LogBlog_WinForm_v0._2
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.exitButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.fileNameBox = new System.Windows.Forms.TextBox();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Controls.Add(this.exitButton, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.browseButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.fileNameBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.outputBox, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1957, 1054);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // exitButton
            // 
            this.exitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exitButton.Location = new System.Drawing.Point(1765, 989);
            this.exitButton.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(184, 58);
            this.exitButton.TabIndex = 0;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // browseButton
            // 
            this.browseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseButton.Location = new System.Drawing.Point(1765, 7);
            this.browseButton.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(184, 58);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // fileNameBox
            // 
            this.fileNameBox.BackColor = System.Drawing.SystemColors.Window;
            this.fileNameBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileNameBox.Location = new System.Drawing.Point(32, 7);
            this.fileNameBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fileNameBox.Name = "fileNameBox";
            this.fileNameBox.ReadOnly = true;
            this.fileNameBox.Size = new System.Drawing.Size(1717, 38);
            this.fileNameBox.TabIndex = 2;
            // 
            // outputBox
            // 
            this.outputBox.BackColor = System.Drawing.SystemColors.Window;
            this.outputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputBox.Location = new System.Drawing.Point(32, 79);
            this.outputBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(1717, 896);
            this.outputBox.TabIndex = 3;
            this.outputBox.Text = "";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1957, 1054);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "MainForm";
            this.Text = "LogBlog";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox fileNameBox;
        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

