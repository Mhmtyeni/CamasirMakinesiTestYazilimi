using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using ScottPlot;

namespace PLCdeneme
{
    public partial class Form5 : Form
    {
        
        public Form5()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Excel Dosyası |*.xlsx| Excel Dosyası|*.xls";
            file.FilterIndex = 1;
            file.RestoreDirectory = true;
            file.CheckFileExists = false;
            file.Title = "Aktarım Yapılacak Excel Dosyası Seçiniz..";
            file.Multiselect = false; //Burası önemli. Multiselecti pasif yapmamız lazım. Aksi halde birden çok seçim işimize gelmeyecektir. Biz bir adet dosya seçeceğiz.
            string DosyaYolu = "";
            if (file.ShowDialog() == DialogResult.OK)
            {
                DosyaYolu = file.FileName;
            }
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DosyaYolu + "; Extended Properties='Excel 12.0 xml;HDR=YES;'");
            baglanti.Open();
            DataTable abc = baglanti.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string ExcelSheetName = abc.Rows[0]["Table_Name"].ToString();            
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [" + ExcelSheetName + "]", baglanti);
            DataTable dt = new DataTable();
            //////
            DataTable abc2 = baglanti.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string ExcelSheetName2 = abc2.Rows[1]["Table_Name"].ToString();
            OleDbDataAdapter da2 = new OleDbDataAdapter("SELECT * FROM [" + ExcelSheetName2 + "]", baglanti);
            DataTable dt2 = new DataTable();
            ////
            DataTable abc3 = baglanti.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string ExcelSheetName3 = abc3.Rows[3]["Table_Name"].ToString();
            OleDbDataAdapter da3 = new OleDbDataAdapter("SELECT * FROM [" + ExcelSheetName3 + "]", baglanti);
            DataTable dt3 = new DataTable();
            ////
            da3.Fill(dt3);
            da2.Fill(dt2);                    
            da.Fill(dt);
            dataGridView1.DataSource = "";
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = dt.DefaultView;
            dataGridView2.DataSource = "";
            dataGridView2.Columns.Clear();
            dataGridView2.DataSource = dt2.DefaultView;
            dataGridView3.DataSource = "";
            dataGridView3.Columns.Clear();
            dataGridView3.DataSource = dt3.DefaultView;
            baglanti.Close();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                dataGridView2.Rows[i].HeaderCell.Value = (i + 1).ToString();
                dataGridView3.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }       
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            DatagridviewSetting(dataGridView1);
            DatagridviewSetting(dataGridView2);
            DatagridviewSetting(dataGridView3);
        }
        public void DatagridviewSetting(DataGridView dataGridView)
        {
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView2.RowHeadersVisible = false;
            //dataGridView3.RowHeadersVisible = false;
            dataGridView.BorderStyle = BorderStyle.Fixed3D;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(139,134,78);
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(189,183,107);
            dataGridView.DefaultCellStyle.ForeColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(110,139,61);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;            
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.AutoGenerateColumns = true;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.RowHeadersWidth = 70;

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DateTime[] start = new DateTime[dataGridView1.Rows.Count];
            double[] dates = new double[dataGridView1.Rows.Count];
            double[] Enerji1Double = new double[dataGridView1.Rows.Count];
            double[] Enerji2Double = new double[dataGridView1.Rows.Count];
            double[] Enerji3Double = new double[dataGridView1.Rows.Count];
            double[] Enerji4Double = new double[dataGridView1.Rows.Count];
            double[] Enerji5Double = new double[dataGridView1.Rows.Count];
            double[] Enerji6Double = new double[dataGridView1.Rows.Count];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                start[i] = Convert.ToDateTime(dataGridView2.Rows[i].Cells[7].Value);
                Enerji1Double[i] = Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value);
                Enerji2Double[i] = Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
                Enerji3Double[i] = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
                Enerji4Double[i] = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                Enerji5Double[i] = Convert.ToDouble(dataGridView2.Rows[i].Cells[4].Value);
                Enerji6Double[i] = Convert.ToDouble(dataGridView2.Rows[i].Cells[5].Value);
                dates[i] = start[i].ToOADate();
            }
            formsPlot1.Plot.Style(ScottPlot.Style.Blue1);
            formsPlot1.Plot.XAxis.MajorGrid(true, Color.FromArgb(80, Color.Black));
            formsPlot1.Plot.YAxis.MajorGrid(true, Color.FromArgb(80, Color.Black));
            formsPlot1.Plot.YAxis.MinorLogScale(true);
            formsPlot1.Plot.YAxis.MinorGrid(true, Color.FromArgb(20, Color.Black));
            var hline = formsPlot1.Plot.AddHorizontalLine(50);
            hline.LineWidth = 3;
            hline.LineStyle = LineStyle.Dash;
            hline.PositionLabel = true;
            hline.PositionLabelBackground = hline.Color = Color.Silver;
            hline.DragEnabled = true;
            formsPlot1.Plot.XAxis.DateTimeFormat(true);
            formsPlot1.Plot.Legend(location: ScottPlot.Alignment.UpperLeft);
            var enerji1 = formsPlot1.Plot.AddScatter(dates, Enerji1Double, Color.DarkMagenta, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "FREKANS");
            var enerji2 = formsPlot1.Plot.AddScatter(dates, Enerji2Double, Color.Orange, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "POWER FACKTOR");
            var enerji3 = formsPlot1.Plot.AddScatter(dates, Enerji3Double, Color.SeaGreen, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "VOLTAJ");
            var enerji4 = formsPlot1.Plot.AddScatter(dates, Enerji4Double, Color.Blue, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "AKIM");
            var enerji5 = formsPlot1.Plot.AddScatter(dates, Enerji5Double, Color.Crimson, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "ENERJİ 5");
            var enerji6 = formsPlot1.Plot.AddScatter(dates, Enerji6Double, Color.Coral, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "TOTAL POWER");
            formsPlot1.Refresh();
        }
    }
}
