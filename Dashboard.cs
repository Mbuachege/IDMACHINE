using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
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
using Image = iText.Layout.Element.Image;

namespace IDMACHINE
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImportList importList = new ImportList();
            importList.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = CONNECTION.Connection())
            {
                string SQL = "SELECT [Name],[REGNO] FROM [dbo].[TESTDATA]";
                SqlCommand cmd = new SqlCommand(SQL, sqlConnection);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string name = dt.Rows[i]["Name"].ToString();
                    string Idno = dt.Rows[i]["REGNO"].ToString();

                    SavetoDatabase(name, Idno);
                }
            }
        }
        private void PrintID(string Name, string RegNo)
        {

            float myWidth = 242.64f;

            var Strfile = @"E:\" + Name + ".pdf";

            using (PdfWriter writer = new PdfWriter(new FileStream(Strfile, FileMode.Create), new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0)))
            {
                PageSize pageSize = new PageSize(242.64f, 153.36f);


                var PDFdOCUMENT = new PdfDocument(writer);
                PDFdOCUMENT.SetDefaultPageSize(pageSize);


                var document = new Document(PDFdOCUMENT);

                document.SetMargins(0f, 0f, 0f, 0f);


                var NewRoman = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
                var fontnormal = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);


                string imageFilePath = @"C:\Users\ADMIN\Downloads\STOCK.jpg";
                Image jpg = new Image(iText.IO.Image.ImageDataFactory.Create(imageFilePath));

                //Image jpg = new Image(iText.IO.Image.ImageDataFactory.Create(imageFilePath); 

                jpg.ScaleToFit(242.64f, 153.36f).SetPadding(0);
                jpg.SetFixedPosition(0, 0);
                jpg.SetUnderline(242.64f, 153.36f).SetPadding(0);
                jpg.ScaleAbsolute(242.64f, 153.36f).SetPadding(0);
                document.Add(jpg);

                Paragraph paragraph3 = new Paragraph(Name).SetFirstLineIndent(70).SetFont(NewRoman);
                paragraph3.SetFontSize(12);
                paragraph3.SetPaddingTop(8).SetPaddingBottom(0).SetMarginBottom(0);
                document.Add(paragraph3);

            }
        }
    }
}
