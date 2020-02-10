using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace ExcelToXML.Engine
{
    public delegate void PercentageFileProcessedHandler(int porcentageProcessed);
    public delegate void FileProcessingHandler(string message);

    public class ExcelToXMLConverter
    {
        private string _sSelectedExcelFile;
        private string _sSelectedOutputPath;
        private Application _ExcelApp;
        private Workbook _workbook;
        private Dictionary<string, Worksheet> _sheetsDictionary;
        private const string KEYCOL = "A";
        private const string CATEGORYCOL = "B";
        private const string TYPECOL = "C";
        private const string rootNode = "root";
        private const string resheaderNode = "resheader";
        private const string dataNode = "data";
        private const string valueNode = "value";
        private int _iPorcentageProcessed = 0;
        private int _iTotalOperations = 0;

        public event PercentageFileProcessedHandler PercentageProcessed;
        public event FileProcessingHandler FileProcessing;

        public ExcelToXMLConverter(string inputFile, string outputFolder)
        {
            _sSelectedExcelFile = inputFile;
            _sSelectedOutputPath = outputFolder;
            _ExcelApp = new Application();
            _workbook = _ExcelApp.Workbooks.Open(_sSelectedExcelFile, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true);
            _sheetsDictionary = new Dictionary<string, Worksheet>();

            foreach (Worksheet sheet in _workbook.Worksheets)
            {
                _sheetsDictionary.Add(sheet.Name, sheet);
            }
        }

        public void Convert()
        {
            foreach (KeyValuePair<string, Worksheet> kvp in _sheetsDictionary)
            {
                ProcessSheet(kvp.Value);
            }
        }

        private void ProcessSheet(Worksheet sheet)
        {
            string rootFileName = sheet.Name;
            List<XmlDocument> docs = new List<XmlDocument>();
            Dictionary<string, Range> columns = new Dictionary<string, Range>();
            Range labelsColumn = null;
            foreach (Range column in sheet.UsedRange.Columns)
            {
                string colAddress = column.Address.Replace("$", "").Split(':')[0].Substring(0, 1);
                if(!colAddress.Equals(CATEGORYCOL) && !colAddress.Equals(TYPECOL))
                {
                    string columnLabel = column.Cells[1, 1].Value2 as string;
                    if (columnLabel != null)
                    {
                        if (!colAddress.Equals(KEYCOL))
                        {
                            columnLabel = columnLabel.Split('[')[1].Split(']')[0];
                            columns.Add(columnLabel, column);
                        }
                        else
                        {
                            labelsColumn = column;
                            _iTotalOperations = column.Rows.Count - 1;
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, Range> column in columns)
            {
                string columnName = column.Key;
                string fileName = Path.Combine(_sSelectedOutputPath, $"{rootFileName}_{columnName}.resx.xml");
                FileProcessing?.Invoke($"Exporting content to file '{rootFileName}_{columnName}.resx.xml'...");
                XmlDocument doc = CreateLanguageXML(labelsColumn, column.Value);
                if (File.Exists(fileName))
                {
                    if (MessageBox.Show($"The file '{rootFileName}_{columnName}.resx.xml' already exists in the selected folder.\nDo you want to replace it?",
                            "File already exists",
                            MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        doc.Save(fileName);
                    }
                }
                else
                {
                    doc.Save(fileName);
                }
                FileProcessing?.Invoke($"File '{rootFileName}_{columnName}.resx.xml' successfully exported");
            }
        }

        private XmlDocument CreateLanguageXML(Range labels, Range translation)
        {
            XmlDocument document = new XmlDocument();
            GenerateXmlDocumentHeader(document);

            for (int i = 2; i <= labels.Rows.Count; i++)
            {
                object labelObj = labels.Cells[i, 1].Value2;
                object translationObj = translation.Cells[i, 1].Value2;
                string labelString = string.Empty;
                string translationString = string.Empty;
                if (labelObj != null)
                {
                    labelString = labelObj.ToString();
                }
                if (translationObj != null)
                {
                    translationString = translationObj.ToString();
                }
                AddResourceEntry(labelString, translationString, document);
                _iPorcentageProcessed = 100 * i / _iTotalOperations;
                if (_iPorcentageProcessed > 101)
                {
                    _iPorcentageProcessed = 101;
                }
                PercentageProcessed?.Invoke(_iPorcentageProcessed);
            }

            return document;
        }

        private void GenerateXmlDocumentHeader(XmlDocument document)
        {
            XmlDeclaration xmlDeclaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.AppendChild(xmlDeclaration);
            XmlElement root = document.CreateElement(rootNode);
            XmlElement resheaderElementMimeType = document.CreateElement(resheaderNode);
            XmlElement valueElement = document.CreateElement(valueNode);
            resheaderElementMimeType.SetAttribute("name", "resmimetype");
            valueElement.InnerText = "text/microsoft-resx";
            resheaderElementMimeType.AppendChild(valueElement);
            XmlElement resheaderElementVersion = document.CreateElement(resheaderNode);
            resheaderElementVersion.SetAttribute("name", "version");
            valueElement = document.CreateElement(valueNode);
            valueElement.InnerText = "2.0";
            resheaderElementVersion.AppendChild(valueElement);
            XmlElement resheaderElementReader = document.CreateElement(resheaderNode);
            resheaderElementReader.SetAttribute("name", "reader");
            valueElement = document.CreateElement(valueNode);
            valueElement.InnerText = "System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral,PublicKeyToken = b77a5c561934e089";
            resheaderElementReader.AppendChild(valueElement);
            XmlElement resheaderElementWriter = document.CreateElement(resheaderNode);
            resheaderElementWriter.SetAttribute("name", "reader");
            valueElement = document.CreateElement(valueNode);
            valueElement.InnerText = "System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral,PublicKeyToken = b77a5c561934e089";
            resheaderElementWriter.AppendChild(valueElement);
            root.AppendChild(resheaderElementMimeType);
            root.AppendChild(resheaderElementVersion);
            root.AppendChild(resheaderElementReader);
            root.AppendChild(resheaderElementWriter);
            document.AppendChild(root);
        }

        public void AddResourceEntry(string label, string translation, XmlDocument document)
        {
            if (translation != string.Empty)
            {
                XmlElement dataElement = document.CreateElement(dataNode);
                XmlElement valueElement = document.CreateElement(valueNode);
                dataElement.SetAttribute("name", label);
                valueElement.InnerText = translation;
                dataElement.AppendChild(valueElement);
                document.ChildNodes[1].AppendChild(dataElement); 
            }
        }
    }
}
