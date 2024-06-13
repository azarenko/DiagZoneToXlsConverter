namespace DiagZoneToXlsConverter
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
            this.label1 = new System.Windows.Forms.Label();
            this._path = new System.Windows.Forms.TextBox();
            this._openFile = new System.Windows.Forms.Button();
            this.openDiagzoneFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Diagzone file location:";
            // 
            // _path
            // 
            this._path.Location = new System.Drawing.Point(129, 6);
            this._path.Name = "_path";
            this._path.ReadOnly = true;
            this._path.Size = new System.Drawing.Size(480, 20);
            this._path.TabIndex = 1;
            // 
            // _openFile
            // 
            this._openFile.Location = new System.Drawing.Point(615, 4);
            this._openFile.Name = "_openFile";
            this._openFile.Size = new System.Drawing.Size(37, 23);
            this._openFile.TabIndex = 4;
            this._openFile.Text = "...";
            this._openFile.UseVisualStyleBackColor = true;
            this._openFile.Click += new System.EventHandler(this._openFile_Click);
            // 
            // openDiagzoneFileDialog
            // 
            this.openDiagzoneFileDialog.Filter = "DiagZone|*.dzx";
            this.openDiagzoneFileDialog.Multiselect = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 34);
            this.Controls.Add(this._openFile);
            this.Controls.Add(this._path);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "Diagzone to XLS converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _path;
        private System.Windows.Forms.Button _openFile;
        private System.Windows.Forms.OpenFileDialog openDiagzoneFileDialog;
    }
}

