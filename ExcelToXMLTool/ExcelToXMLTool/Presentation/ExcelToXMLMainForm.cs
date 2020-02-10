using System;
using System.IO;
using System.Windows.Forms;
using ExcelToXML.Engine;

namespace ExcelToXML.Presentation
{
    public partial class ExcelToXMLMainForm : Form
    {
        private string _sSelectedExcelFile;
        private string _sSelectedOutputPath;
        private ExcelToXMLConverter _converter;

        public ExcelToXMLMainForm()
        {
            InitializeComponent();
            _progressBar.Visible = true;
            _progressBar.Value = 0;
            _progressBar.Minimum = 0;
            _progressBar.Maximum = 101;
            _outputTextArea.Cursor = DefaultCursor;
        }

        private TextBox _excelFolderPathField;

        private bool EvaluateConvertButtonEnable()
        {
            return !_excelFolderPathField.Text.Equals(string.Empty) && !_xmlFilePathField.Text.Equals(string.Empty)
                                                             && File.Exists(_excelFolderPathField.Text) &&
                                                             Directory.Exists(_xmlFilePathField.Text);
        }

        private void OnOpenExcelFileClicked(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = "Excel files (*.xls;*.xlsx)|*.xls;*.xlsx"
            };
            if (File.Exists(_excelFolderPathField.Text))
            {
                dialog.FileName = _excelFolderPathField.Text;
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _excelFolderPathField.Text = dialog.FileNames[0];
                _sSelectedExcelFile = dialog.FileNames[0];
            }
        }

        private void OnOpenXMLFolderClicked(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (Directory.Exists(_openOutputFolderBtn.Text))
            {
                dialog.SelectedPath = _openOutputFolderBtn.Text;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _xmlFilePathField.Text = dialog.SelectedPath;
                _sSelectedOutputPath = dialog.SelectedPath;
            }
        }

        private void OnConvertButtonClicked(object sender, EventArgs e)
        {
            _converter = new ExcelToXMLConverter(_sSelectedExcelFile, _sSelectedOutputPath);
            _converter.PercentageProcessed += OnProgressBarUpdated;
            _converter.FileProcessing += OnFileStartedProcessing;
            _progressBar.Visible = true;
            _progressBar.Value = 0;

            _converter.Convert();
            if (MessageBox.Show("Excel file successfully exported to XML files", "Success",
                    MessageBoxButtons.OK) == DialogResult.OK)
            {
                Close();
            }
        }

        private void OnFileStartedProcessing(string message)
        {
            _outputTextArea.AppendText($"{message}\r\n");
        }

        private void OnProgressBarUpdated(int percentageProcessed)
        {
            _progressBar.Value = percentageProcessed;
            if (percentageProcessed > 0)
            {
                _progressBar.Value = percentageProcessed - 1;
            }
        }

        private void OnFilesFilterTextChanged(object sender, EventArgs e)
        {
            _convertToXMLBtn.Enabled = EvaluateConvertButtonEnable();
            if (sender.Equals(_excelFolderPathField))
            {
                _sSelectedExcelFile = _excelFolderPathField.Text;
            }
            else
            {
                _sSelectedOutputPath = _openExcelFileBtn.Text;
            }
        }
    }
}
