using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace IDMACHINE
{
    public partial class ImportList : Form
    {
        public ImportList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Application ImporttoDatagrid;
            _Workbook workbook;
            _Worksheet worksheet;
            Range importgridrange;

            try
            {
                ImporttoDatagrid = new Microsoft.Office.Interop.Excel.Application();
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                fileDialog.Title = "Import To Excel";
                if(fileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook = ImporttoDatagrid.Workbooks.Open(fileDialog.FileName);
                    worksheet = workbook.Sheets.get_Item(1);
                    importgridrange = worksheet.UsedRange;

                    for(int rows = 2; rows<importgridrange.Rows.Count; rows++)
                    {
                        Image img =Image.FromFile(importgridrange.Cells[rows, 2].value);
                        dataGridView1.Rows.Add(importgridrange.Cells[rows, 1].value);
                        dataGridView1.Rows.Add(img); 
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Image imageToByteArray(byte[] imgr)
        {
            MemoryStream ms = new MemoryStream();
            return Image.FromStream(ms);

        }
        private Image GetImage(byte[] iMG)
        {
            MemoryStream ms = new MemoryStream();
            return Image.FromStream(ms);

        }
    }
}
