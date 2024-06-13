using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiagZoneToXlsConverter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void _openFile_Click(object sender, EventArgs e)
        {
            if (openDiagzoneFileDialog.ShowDialog() == DialogResult.OK)
            {
                _path.Text = string.Empty;

                foreach (string filePath in openDiagzoneFileDialog.FileNames)
                {
                    var data = ReadData(filePath);
                    var path = filePath + ".xlsx";
                    _path.Text += path + " ";
                    WriteData(path, data);
                }

                MessageBox.Show("Conversion complete", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void WriteData(string path, DataContainer data)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Diagzone Export");

                for (int i = 0; i < data.ChannelNames.Length; i++)
                {
                    worksheet.Cell(1, i + 1).Value = data.ChannelNames[i];
                }

                for (int i = 0; i < data.Data.Length; i++)
                    for (int j = 0; j < data.Data[i].Length; j++)
                    {
                        worksheet.Cell(j + 2, i + 1).Value = data.Data[i][j];
                    }

                workbook.SaveAs(path);
            }
        }

        private DataContainer ReadData(string path)
        {
            var data = new DataContainer();

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                byte[] buffer = new byte[128];

                byte var8 = 0;
                short var16 = 0;
                int var32 = 0;

                // read channel count
                fs.Seek(0x134, SeekOrigin.Begin);
                fs.Read(buffer, 0, 1);
                int columnCount = buffer[0] / 4;

                fs.Seek(0x0c, SeekOrigin.Begin); // seek to the first lenght field

                fs.Read(buffer, 0, 4);
                var32 = BitConverter.ToInt32(buffer, 0);
                fs.Seek(var32, SeekOrigin.Current);

                for (int i = 0; i < 8; i++) // skip several headers
                {
                    fs.Read(buffer, 0, 2);
                    var16 = BitConverter.ToInt16(buffer, 0);
                    fs.Seek(var16 - 2, SeekOrigin.Current);
                }

                // read point values
                List<string> pointValues = new List<string>();
                while (fs.Position != fs.Length)
                {
                    // read field lenght
                    fs.Read(buffer, 0, 2);
                    var16 = BitConverter.ToInt16(buffer, 0);

                    // read field data
                    fs.Read(buffer, 0, var16 - 2);
                    string value = Encoding.ASCII.GetString(buffer, 0, var16 - 3);
                    pointValues.Add(value);
                }

                var columnNames = new string[columnCount];
                fs.Seek(0x138, SeekOrigin.Begin);
                // read column headers
                for (int i = 0; i < columnCount; i++)
                {
                    // read column description
                    fs.Read(buffer, 0, 4);
                    var32 = BitConverter.ToInt16(buffer, 0);

                    if (var32 == 0) continue;

                    columnNames[i] = string.Format("{0}. {1}", i + 1, pointValues[var32 - 0x09]);
                }

                // read column headers
                for (int i = 0; i < columnCount; i++)
                {
                    // read column description
                    fs.Read(buffer, 0, 4);
                    var32 = BitConverter.ToInt16(buffer, 0);

                    if (var32 == 0) continue;

                    columnNames[i] = string.Format("{0} ({1})", columnNames[i], pointValues[var32 - 0x09]);
                }

                data.ChannelNames = columnNames;

                // read data 
                fs.Seek(0x11c, SeekOrigin.Begin);
                fs.Read(buffer, 0, 2);
                var16 = BitConverter.ToInt16(buffer, 0);

                // Read data lenght
                fs.Seek(var16 + 8, SeekOrigin.Begin);
                fs.Read(buffer, 0, 8);
                int recordsCountInBytes = BitConverter.ToInt32(buffer, 0);
                int totalRecordsCount = recordsCountInBytes / 4;
                int recordsPerChannel = totalRecordsCount / columnCount;

                var samplesBuffer = new double[columnCount][];

                // prepare channel buffer
                for (int i = 0; i < columnCount; i++)
                {
                    samplesBuffer[i] = new double[recordsPerChannel];
                }

                // fill channel data
                for (int i = 0; i < recordsPerChannel; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        fs.Read(buffer, 0, 4);
                        var16 = BitConverter.ToInt16(buffer, 0);
                        int index = var16 - 0x09;
                        if (0 <= index && index < pointValues.Count)
                        {
                            samplesBuffer[j][i] = double.Parse(pointValues[index]);
                        }
                        else
                        {
                            samplesBuffer[j][i] = 0;
                        }
                    }
                }

                data.Data = samplesBuffer;
            }

            return data;
        }
    }
}
