using System.Drawing;
using System.Windows.Forms;

namespace ExcelToXML.Presentation
{
    partial class ExcelToXMLMainForm
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
            this._excelFolderPathField = new System.Windows.Forms.TextBox();
            this._openExcelFileBtn = new System.Windows.Forms.Button();
            this._openOutputFolderBtn = new System.Windows.Forms.Button();
            this._xmlFilePathField = new System.Windows.Forms.TextBox();
            this._convertToXMLBtn = new System.Windows.Forms.Button();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._outputTextArea = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _excelFolderPathField
            // 
            this._excelFolderPathField.Location = new System.Drawing.Point(27, 36);
            this._excelFolderPathField.Name = "_excelFolderPathField";
            this._excelFolderPathField.Size = new System.Drawing.Size(425, 22);
            this._excelFolderPathField.TabIndex = 0;
            this._excelFolderPathField.TextChanged += new System.EventHandler(this.OnFilesFilterTextChanged);
            // 
            // _openExcelFileBtn
            // 
            this._openExcelFileBtn.Location = new System.Drawing.Point(458, 30);
            this._openExcelFileBtn.Name = "_openExcelFileBtn";
            this._openExcelFileBtn.Size = new System.Drawing.Size(125, 34);
            this._openExcelFileBtn.TabIndex = 1;
            this._openExcelFileBtn.Text = "Search file";
            this._openExcelFileBtn.UseVisualStyleBackColor = true;
            this._openExcelFileBtn.Click += new System.EventHandler(this.OnOpenExcelFileClicked);
            // 
            // _openOutputFolderBtn
            // 
            this._openOutputFolderBtn.Location = new System.Drawing.Point(458, 81);
            this._openOutputFolderBtn.Name = "_openOutputFolderBtn";
            this._openOutputFolderBtn.Size = new System.Drawing.Size(125, 34);
            this._openOutputFolderBtn.TabIndex = 2;
            this._openOutputFolderBtn.Text = "Open folder";
            this._openOutputFolderBtn.UseVisualStyleBackColor = true;
            this._openOutputFolderBtn.Click += new System.EventHandler(this.OnOpenXMLFolderClicked);
            // 
            // _xmlFilePathField
            // 
            this._xmlFilePathField.Location = new System.Drawing.Point(27, 87);
            this._xmlFilePathField.Name = "_xmlFilePathField";
            this._xmlFilePathField.Size = new System.Drawing.Size(425, 22);
            this._xmlFilePathField.TabIndex = 3;
            this._xmlFilePathField.TextChanged += new System.EventHandler(this.OnFilesFilterTextChanged);
            // 
            // _convertToXMLBtn
            // 
            this._convertToXMLBtn.Enabled = false;
            this._convertToXMLBtn.Location = new System.Drawing.Point(212, 125);
            this._convertToXMLBtn.Name = "_convertToXMLBtn";
            this._convertToXMLBtn.Size = new System.Drawing.Size(189, 46);
            this._convertToXMLBtn.TabIndex = 4;
            this._convertToXMLBtn.Text = "Convert to XML";
            this._convertToXMLBtn.UseVisualStyleBackColor = true;
            this._convertToXMLBtn.Click += new System.EventHandler(this.OnConvertButtonClicked);
            // 
            // _progressBar
            // 
            this._progressBar.Location = new System.Drawing.Point(27, 177);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(556, 23);
            this._progressBar.TabIndex = 5;
            // 
            // _outputTextArea
            // 
            this._outputTextArea.Location = new System.Drawing.Point(27, 219);
            this._outputTextArea.Multiline = true;
            this._outputTextArea.Name = "_outputTextArea";
            this._outputTextArea.ReadOnly = true;
            this._outputTextArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._outputTextArea.Size = new System.Drawing.Size(556, 243);
            this._outputTextArea.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Resources excel file path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Output XML files path:";
            // 
            // ExcelToXMLMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 488);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._outputTextArea);
            this.Controls.Add(this._progressBar);
            this.Controls.Add(this._convertToXMLBtn);
            this.Controls.Add(this._xmlFilePathField);
            this.Controls.Add(this._openOutputFolderBtn);
            this.Controls.Add(this._openExcelFileBtn);
            this.Controls.Add(this._excelFolderPathField);
            this.Name = "ExcelToXMLMainForm";
            this.Text = "Excel To XML Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button _openExcelFileBtn;
        private Button _openOutputFolderBtn;
        private TextBox _xmlFilePathField;
        private Button _convertToXMLBtn;
        private ProgressBar _progressBar;
        private TextBox _outputTextArea;
        private Label label1;
        private Label label2;
    }
}

