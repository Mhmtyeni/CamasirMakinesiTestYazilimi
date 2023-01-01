using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Guna.Charts.WinForms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Data.OleDb;
using ScottPlot.Plottable;
using ScottPlot;
using LiveCharts.Charts;
using LiveCharts.Wpf;
using LiveCharts;
using LiveCharts.Defaults;

namespace PLCdeneme
{
    public partial class Form4 : Form
    {
        public static PlcToPc PlcToPc = new PlcToPc();
        public static PcToPlc PcToPlc = new PcToPlc();
        public Form4()
        {
            InitializeComponent();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            formsPlot1.Reset();
            Form1.formCon.Temizle();
            if (Form2.GrafikAdiComboBox == "SICAKLIK GRAFİĞİ")
            {
                formsPlot1.Reset();
                Form1.formCon.OzelRead2();
                DateTime[] start = new DateTime[Form1.formCon.sicaklik1_2.Count];
                double[] dates = new double[Form1.formCon.sicaklik1_2.Count];
                double[] Sicaklik1Double = new double[Form1.formCon.sicaklik1_2.Count];
                double[] Sicaklik2Double = new double[Form1.formCon.sicaklik1_2.Count];
                double[] Sicaklik3Double = new double[Form1.formCon.sicaklik1_2.Count];
                double[] Sicaklik4Double = new double[Form1.formCon.sicaklik1_2.Count];
                double[] Sicaklik5Double = new double[Form1.formCon.sicaklik1_2.Count];
                double[] Sicaklik6Double = new double[Form1.formCon.sicaklik1_2.Count];
                double[] Sicaklik7Double = new double[Form1.formCon.sicaklik1_2.Count];
                double[] Sicaklik8Double = new double[Form1.formCon.sicaklik1_2.Count];
                double[] Sicaklik9Double = new double[Form1.formCon.sicaklik1_2.Count];
                double[] Sicaklik10Double = new double[Form1.formCon.sicaklik1_2.Count];
                double[] Sicaklik11Double = new double[Form1.formCon.sicaklik1_2.Count];
                for (int i = 0; i < Form1.formCon.sicaklik1_2.Count; i++)
                {
                    start[i] = Convert.ToDateTime(Form1.formCon.TarihArryOtherValues[i]);
                    Sicaklik1Double[i] = Convert.ToDouble(Form1.formCon.sicaklik1_2[i]);
                    Sicaklik2Double[i] = Convert.ToDouble(Form1.formCon.sicaklik2_2[i]);
                    Sicaklik3Double[i] = Convert.ToDouble(Form1.formCon.sicaklik3_2[i]);
                    Sicaklik4Double[i] = Convert.ToDouble(Form1.formCon.sicaklik4_2[i]);
                    Sicaklik5Double[i] = Convert.ToDouble(Form1.formCon.sicaklik5_2[i]);
                    Sicaklik6Double[i] = Convert.ToDouble(Form1.formCon.sicaklik6_2[i]);
                    Sicaklik7Double[i] = Convert.ToDouble(Form1.formCon.sicaklik7_2[i]);
                    Sicaklik8Double[i] = Convert.ToDouble(Form1.formCon.sicaklik8_2[i]);
                    Sicaklik9Double[i] = Convert.ToDouble(Form1.formCon.sicaklik9_2[i]);
                    Sicaklik10Double[i] = Convert.ToDouble(Form1.formCon.sicaklik10_2[i]);
                    Sicaklik11Double[i] = Convert.ToDouble(Form1.formCon.sicaklik11_2[i]);
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
                var sicaklik1 = formsPlot1.Plot.AddScatter(dates, Sicaklik1Double, Color.DarkMagenta, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 1");
                var sicaklik2 = formsPlot1.Plot.AddScatter(dates, Sicaklik2Double, Color.Orange, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 2");
                var sicaklik3 = formsPlot1.Plot.AddScatter(dates, Sicaklik3Double, Color.SeaGreen, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 3");
                var sicaklik4 = formsPlot1.Plot.AddScatter(dates, Sicaklik4Double, Color.Blue, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 4");
                var sicaklik5 = formsPlot1.Plot.AddScatter(dates, Sicaklik5Double, Color.Crimson, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 5");
                var sicaklik6 = formsPlot1.Plot.AddScatter(dates, Sicaklik6Double, Color.Coral, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 6");
                var sicaklik7 = formsPlot1.Plot.AddScatter(dates, Sicaklik7Double, Color.Firebrick, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 7");
                var sicaklik8 = formsPlot1.Plot.AddScatter(dates, Sicaklik8Double, Color.Violet, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 8");
                var sicaklik9 = formsPlot1.Plot.AddScatter(dates, Sicaklik9Double, Color.Cyan, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 9");
                var sicaklik10 = formsPlot1.Plot.AddScatter(dates, Sicaklik10Double, Color.DeepPink, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 10");
                var sicaklik11 = formsPlot1.Plot.AddScatter(dates, Sicaklik11Double, Color.Chocolate, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SUYUN SICAKLIĞI");
                formsPlot1.Refresh();

            }
            if (Form2.GrafikAdiComboBox == "ENERJİ GRAFİĞİ")
            {
                formsPlot1.Reset();
                Form1.formCon.OzelRead3();
                DateTime[] start = new DateTime[Form1.formCon.energy1_2.Count];
                double[] dates = new double[Form1.formCon.energy1_2.Count];
                double[] Enerji1Double = new double[Form1.formCon.energy1_2.Count];
                double[] Enerji2Double = new double[Form1.formCon.energy2_2.Count];
                double[] Enerji3Double = new double[Form1.formCon.energy3_2.Count];
                double[] Enerji4Double = new double[Form1.formCon.energy4_2.Count];
                double[] Enerji6Double = new double[Form1.formCon.energy6_2.Count];
                for (int i = 0; i < Form1.formCon.energy1_2.Count; i++)
                {
                    start[i] = Convert.ToDateTime(Form1.formCon.TarihArryOtherValues[i]);
                    Enerji1Double[i] = Convert.ToDouble(Form1.formCon.energy1_2[i]);
                    Enerji2Double[i] = Convert.ToDouble(Form1.formCon.energy2_2[i]);
                    Enerji3Double[i] = Convert.ToDouble(Form1.formCon.energy3_2[i]);
                    Enerji4Double[i] = Convert.ToDouble(Form1.formCon.energy4_2[i]);
                    Enerji6Double[i] = Convert.ToDouble(Form1.formCon.energy6_2[i]);
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
                var enerji5 = formsPlot1.Plot.AddScatter(dates, Enerji6Double, Color.Coral, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "TOTAL POWER");
                formsPlot1.Refresh();
            }
            if (Form2.GrafikAdiComboBox == "DİĞER DEĞERLER GRAFİĞİ")
            {
                formsPlot1.Reset();
                Form1.formCon.OzelRead();
                DateTime[] start = new DateTime[Form1.formCon.RpmSend2.Count];
                double[] dates = new double[Form1.formCon.RpmSend2.Count];
                double[] RpmDouble = new double[Form1.formCon.RpmSend2.Count];
                double[] RpmMaxDouble = new double[Form1.formCon.RpmMax2.Count];
                double[] SumOfTotalDouble = new double[Form1.formCon.SumOfTotal2.Count];
                double[] FlowMeterDouble = new double[Form1.formCon.FlowMeter2.Count];
                double[] LiquidDouble = new double[Form1.formCon.LiquidPressure2.Count];
                double[] FlowMeterMaxDouble = new double[Form1.formCon.FlowMeterMax2.Count];
                double[] LiquidMaxDouble = new double[Form1.formCon.LiquidPressureMax2.Count];
                for (int i = 0; i < Form1.formCon.RpmSend2.Count; i++)
                {
                    start[i] = Convert.ToDateTime(Form1.formCon.TarihArryOtherValues[i]);
                    RpmDouble[i] = Convert.ToDouble(Form1.formCon.RpmSend2[i]);
                    RpmMaxDouble[i] = Convert.ToDouble(Form1.formCon.RpmMax2[i]);
                    SumOfTotalDouble[i] = Convert.ToDouble(Form1.formCon.SumOfTotal2[i]);
                    FlowMeterDouble[i] = Convert.ToDouble(Form1.formCon.FlowMeter2[i]);
                    LiquidDouble[i] = Convert.ToDouble(Form1.formCon.LiquidPressure2[i]);
                    FlowMeterMaxDouble[i] = Convert.ToDouble(Form1.formCon.FlowMeterMax2[i]);
                    LiquidMaxDouble[i] = Convert.ToDouble(Form1.formCon.LiquidPressureMax2[i]);
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
                var rpm = formsPlot1.Plot.AddScatter(dates, RpmDouble, Color.DarkMagenta, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "RPM");
                var rpmmax = formsPlot1.Plot.AddScatter(dates, RpmMaxDouble, Color.Orange, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "RPM MAX");
                var sumoftotal = formsPlot1.Plot.AddScatter(dates, SumOfTotalDouble, Color.SeaGreen, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "TOPLAM GÜÇ");
                var flowmeter = formsPlot1.Plot.AddScatter(dates, FlowMeterDouble, Color.Blue, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SU AKIŞ HIZI");
                var liquid = formsPlot1.Plot.AddScatter(dates, LiquidDouble, Color.Crimson, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SU BASINCI");
                var flowmetermax = formsPlot1.Plot.AddScatter(dates, FlowMeterMaxDouble, Color.Coral, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "TOPLAM SU AKIŞ HIZI");
                var liquidmax = formsPlot1.Plot.AddScatter(dates, LiquidMaxDouble, Color.Firebrick, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "TOPLAM SU BASINCI");
                formsPlot1.Refresh();
            }
        }
        /////--------------excel
        private void guna2ImageButton5_Click(object sender, EventArgs e)
        {
            ///---------EXCEL VERİ AKTARMA
            if (Form2.GrafikAdiComboBox == "DİĞER DEĞERLER GRAFİĞİ")
            {
                Form1.formCon.RpmSend2.Clear();
                Form1.formCon.OzelRead();
                Excel.Application xlApp = new Excel.Application();
                xlApp.Visible = true;
                xlApp.DisplayAlerts = true;
                Excel.Workbook wb = xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet ws = (Excel.Worksheet)wb.Sheets[1];
                Excel.Range range;
                range = ws.get_Range("A1", "J1");
                range.get_Range("A1", "J1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                range.get_Range("A1", "J1").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                range.ColumnWidth = 25;
                range.Font.Size = 14;
                range.Font.Color = Excel.XlRgbColor.rgbRoyalBlue;
                range.EntireRow.Font.Bold = true;
                ws.Cells[1, 1] = "RPM";
                ws.Cells[1, 2] = "RPM MAX";
                ws.Cells[1, 3] = "TOPLAM GÜÇ";
                ws.Cells[1, 4] = "SU AKIŞ HIZI";
                ws.Cells[1, 5] = "SU BASINCI";
                ws.Cells[1, 6] = "TOPLAM SU AKIŞ HIZI";
                ws.Cells[1, 7] = "TOPLAM SU BASINCI";
                ws.Cells[1, 8] = "DÖNÜŞ YÖNÜ";
                ws.Cells[1, 9] = "MODEL";
                ws.Cells[1, 10] = "TARİH";
                for (int i = 0; i < Form1.formCon.RpmSend2.Count; i++)
                {
                    for (int j = 1; j < 11; j++)
                    {
                        ws.Cells[i + 2, j] = Form1.formCon.RpmSend2[i];
                        ws.Cells[i + 2, j + 1] = Form1.formCon.RpmMax2[i];
                        ws.Cells[i + 2, j + 2] = Form1.formCon.SumOfTotal2[i];
                        ws.Cells[i + 2, j + 3] = Form1.formCon.FlowMeter2[i];
                        ws.Cells[i + 2, j + 4] = Form1.formCon.LiquidPressure2[i];
                        ws.Cells[i + 2, j + 5] = Form1.formCon.FlowMeterMax2[i];
                        ws.Cells[i + 2, j + 6] = Form1.formCon.LiquidPressureMax2[i];
                        ws.Cells[i + 2, j + 7] = Form1.formCon.Direction2[i];
                        ws.Cells[i + 2, j + 8] = Form1.formCon.Model_2[1];
                        ws.Cells[i + 2, j + 9] = Form1.formCon.TarihArryOtherValues[i].ToString();
                        break;
                    }
                }
            }
            if (Form2.GrafikAdiComboBox == "SICAKLIK GRAFİĞİ")
            {
                Form1.formCon.sicaklik1_2.Clear();
                Form1.formCon.OzelRead2();
                Excel.Application xlApp = new Excel.Application();
                xlApp.Visible = true;
                xlApp.DisplayAlerts = true;
                Excel.Workbook wb = xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet ws = (Excel.Worksheet)wb.Sheets[1];
                Excel.Range range;
                range = ws.get_Range("A1", "M1");
                range.get_Range("A1", "M1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                range.get_Range("A1", "M1").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                range.ColumnWidth = 25;
                range.Font.Size = 14;
                range.Font.Color = Excel.XlRgbColor.rgbRed;
                range.EntireRow.Font.Bold = true;
                ws.Cells[1, 1] = "SICAKLIK 1";
                ws.Cells[1, 2] = "SICAKLIK 2";
                ws.Cells[1, 3] = "SICAKLIK 3";
                ws.Cells[1, 4] = "SICAKLIK 4";
                ws.Cells[1, 5] = "SICAKLIK 5";
                ws.Cells[1, 6] = "SICAKLIK 6";
                ws.Cells[1, 7] = "SICAKLIK 7";
                ws.Cells[1, 8] = "SICAKLIK 8";
                ws.Cells[1, 9] = "SICAKLIK 9";
                ws.Cells[1, 10] = "SICAKLIK 10";
                ws.Cells[1, 11] = "SUYUN SICAKLIĞI";
                ws.Cells[1, 12] = "MODEL";
                ws.Cells[1, 13] = "TARİH";
                for (int i = 0; i < Form1.formCon.sicaklik1_2.Count; i++)
                {
                    for (int j = 1; j < 14; j++)
                    {
                        ws.Cells[i + 2, j] = Form1.formCon.sicaklik1_2[i];
                        ws.Cells[i + 2, j + 1] = Form1.formCon.sicaklik2_2[i];
                        ws.Cells[i + 2, j + 2] = Form1.formCon.sicaklik3_2[i];
                        ws.Cells[i + 2, j + 3] = Form1.formCon.sicaklik4_2[i];
                        ws.Cells[i + 2, j + 4] = Form1.formCon.sicaklik5_2[i];
                        ws.Cells[i + 2, j + 5] = Form1.formCon.sicaklik6_2[i];
                        ws.Cells[i + 2, j + 6] = Form1.formCon.sicaklik7_2[i];
                        ws.Cells[i + 2, j + 7] = Form1.formCon.sicaklik8_2[i];
                        ws.Cells[i + 2, j + 8] = Form1.formCon.sicaklik9_2[i];
                        ws.Cells[i + 2, j + 9] = Form1.formCon.sicaklik11_2[i];
                        ws.Cells[i + 2, j + 10] = Form1.formCon.sicaklik10_2[i];
                        ws.Cells[i + 2, j + 11] = Form1.formCon.Model2_2[1];
                        ws.Cells[i + 2, j + 12] = Form1.formCon.TarihArryOtherValues[i].ToString();
                        break;
                    }
                }
            }
            if (Form2.GrafikAdiComboBox == "ENERJİ GRAFİĞİ")
            {
                Form1.formCon.energy1_2.Clear();
                Form1.formCon.OzelRead3();
                Excel.Application xlApp = new Excel.Application();
                xlApp.Visible = true;
                xlApp.DisplayAlerts = true;
                Excel.Workbook wb = xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet ws = (Excel.Worksheet)wb.Sheets[1];
                Excel.Range range;
                range = ws.get_Range("A1", "H1");
                range.get_Range("A1", "H1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                range.get_Range("A1", "H1").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                range.ColumnWidth = 25;
                range.Font.Size = 14;
                range.Font.Color = Excel.XlRgbColor.rgbDarkSlateGray;
                range.EntireRow.Font.Bold = true;
                ws.Cells[1, 1] = "FREKANS";
                ws.Cells[1, 2] = "POWER FACTOR";
                ws.Cells[1, 3] = "VOLTAJ";
                ws.Cells[1, 4] = "AKIM";
                ws.Cells[1, 5] = "TOTAL POWER";
                ws.Cells[1, 6] = "MODEL";
                ws.Cells[1, 7] = "TARİH";
                for (int i = 0; i < Form1.formCon.energy1_2.Count; i++)
                {
                    for (int j = 1; j < 8; j++)
                    {
                        ws.Cells[i + 2, j] = Form1.formCon.energy1_2[i];
                        ws.Cells[i + 2, j + 1] = Form1.formCon.energy2_2[i];
                        ws.Cells[i + 2, j + 2] = Form1.formCon.energy3_2[i];
                        ws.Cells[i + 2, j + 3] = Form1.formCon.energy4_2[i];
                        ws.Cells[i + 2, j + 4] = Form1.formCon.energy6_2[i];
                        ws.Cells[i + 2, j + 5] = Form1.formCon.Model3_2[1];
                        ws.Cells[i + 2, j + 6] = Form1.formCon.TarihArryOtherValues[i].ToString();
                        break;
                    }
                }
            }
        }
        private void formsPlot1_MouseMove(object sender, MouseEventArgs e)
        {
            (double mouseCoordX, double mouseCoordY) = formsPlot1.GetMouseCoordinates();
            formsPlot1.Plot.Title($"DEĞER : {mouseCoordY.ToString("F5")}");
        }
        private void enerjibutton_Click(object sender, EventArgs e)
        {
            formsPlot1.Reset();
            Form1.formCon.OzelRead3();
            DateTime[] start = new DateTime[Form1.formCon.energy1_2.Count];
            double[] dates = new double[Form1.formCon.energy1_2.Count];
            double[] Enerji1Double = new double[Form1.formCon.energy1_2.Count];
            double[] Enerji2Double = new double[Form1.formCon.energy2_2.Count];
            double[] Enerji3Double = new double[Form1.formCon.energy3_2.Count];
            double[] Enerji4Double = new double[Form1.formCon.energy4_2.Count];
            double[] Enerji6Double = new double[Form1.formCon.energy6_2.Count];
            for (int i = 0; i < Form1.formCon.energy1_2.Count; i++)
            {
                start[i] = Convert.ToDateTime(Form1.formCon.TarihArryOtherValues[i]);
                Enerji1Double[i] = Convert.ToDouble(Form1.formCon.energy1_2[i]);
                Enerji2Double[i] = Convert.ToDouble(Form1.formCon.energy2_2[i]);
                Enerji3Double[i] = Convert.ToDouble(Form1.formCon.energy3_2[i]);
                Enerji4Double[i] = Convert.ToDouble(Form1.formCon.energy4_2[i]);
                Enerji6Double[i] = Convert.ToDouble(Form1.formCon.energy6_2[i]);
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
            var enerji5 = formsPlot1.Plot.AddScatter(dates, Enerji6Double, Color.Coral, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "TOTAL POWER");
            formsPlot1.Refresh();
        }
        private void Otherbutton_Click(object sender, EventArgs e)
        {
            formsPlot1.Reset();
            Form1.formCon.OzelRead();
            DateTime[] start = new DateTime[Form1.formCon.RpmSend2.Count];
            double[] dates = new double[Form1.formCon.RpmSend2.Count];
            double[] RpmDouble = new double[Form1.formCon.RpmSend2.Count];
            double[] RpmMaxDouble = new double[Form1.formCon.RpmMax2.Count];
            double[] SumOfTotalDouble = new double[Form1.formCon.SumOfTotal2.Count];
            double[] FlowMeterDouble = new double[Form1.formCon.FlowMeter2.Count];
            double[] LiquidDouble = new double[Form1.formCon.LiquidPressure2.Count];
            double[] FlowMeterMaxDouble = new double[Form1.formCon.FlowMeterMax2.Count];
            double[] LiquidMaxDouble = new double[Form1.formCon.LiquidPressureMax2.Count];
            for (int i = 0; i < Form1.formCon.RpmSend2.Count; i++)
            {
                start[i] = Convert.ToDateTime(Form1.formCon.TarihArryOtherValues[i]);
                RpmDouble[i] = Convert.ToDouble(Form1.formCon.RpmSend2[i]);
                RpmMaxDouble[i] = Convert.ToDouble(Form1.formCon.RpmMax2[i]);
                SumOfTotalDouble[i] = Convert.ToDouble(Form1.formCon.SumOfTotal2[i]);
                FlowMeterDouble[i] = Convert.ToDouble(Form1.formCon.FlowMeter2[i]);
                LiquidDouble[i] = Convert.ToDouble(Form1.formCon.LiquidPressure2[i]);
                FlowMeterMaxDouble[i] = Convert.ToDouble(Form1.formCon.FlowMeterMax2[i]);
                LiquidMaxDouble[i] = Convert.ToDouble(Form1.formCon.LiquidPressureMax2[i]);
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
            var rpm = formsPlot1.Plot.AddScatter(dates, RpmDouble, Color.DarkMagenta, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "RPM");
            var rpmmax = formsPlot1.Plot.AddScatter(dates, RpmMaxDouble, Color.Orange, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "RPM MAX");
            var sumoftotal = formsPlot1.Plot.AddScatter(dates, SumOfTotalDouble, Color.SeaGreen, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "TOPLAM GÜÇ");
            var flowmeter = formsPlot1.Plot.AddScatter(dates, FlowMeterDouble, Color.Blue, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SU AKIŞ HIZI");
            var liquid = formsPlot1.Plot.AddScatter(dates, LiquidDouble, Color.Crimson, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SU BASINCI");
            var flowmetermax = formsPlot1.Plot.AddScatter(dates, FlowMeterMaxDouble, Color.Coral, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "TOPLAM SU AKIŞ HIZI");
            var liquidmax = formsPlot1.Plot.AddScatter(dates, LiquidMaxDouble, Color.Firebrick, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "TOPLAM SU BASINCI");
            formsPlot1.Refresh();
        }
        private void sicaklikbutton_Click(object sender, EventArgs e)
        {
            formsPlot1.Reset();
            Form1.formCon.OzelRead2();
            DateTime[] start = new DateTime[Form1.formCon.sicaklik1_2.Count];
            double[] dates = new double[Form1.formCon.sicaklik1_2.Count];
            double[] Sicaklik1Double = new double[Form1.formCon.sicaklik1_2.Count];
            double[] Sicaklik2Double = new double[Form1.formCon.sicaklik1_2.Count];
            double[] Sicaklik3Double = new double[Form1.formCon.sicaklik1_2.Count];
            double[] Sicaklik4Double = new double[Form1.formCon.sicaklik1_2.Count];
            double[] Sicaklik5Double = new double[Form1.formCon.sicaklik1_2.Count];
            double[] Sicaklik6Double = new double[Form1.formCon.sicaklik1_2.Count];
            double[] Sicaklik7Double = new double[Form1.formCon.sicaklik1_2.Count];
            double[] Sicaklik8Double = new double[Form1.formCon.sicaklik1_2.Count];
            double[] Sicaklik9Double = new double[Form1.formCon.sicaklik1_2.Count];
            double[] Sicaklik10Double = new double[Form1.formCon.sicaklik1_2.Count];
            double[] Sicaklik11Double = new double[Form1.formCon.sicaklik1_2.Count];
            for (int i = 0; i < Form1.formCon.sicaklik1_2.Count; i++)
            {
                start[i] = Convert.ToDateTime(Form1.formCon.TarihArryOtherValues[i]);
                Sicaklik1Double[i] = Convert.ToDouble(Form1.formCon.sicaklik1_2[i]);
                Sicaklik2Double[i] = Convert.ToDouble(Form1.formCon.sicaklik2_2[i]);
                Sicaklik3Double[i] = Convert.ToDouble(Form1.formCon.sicaklik3_2[i]);
                Sicaklik4Double[i] = Convert.ToDouble(Form1.formCon.sicaklik4_2[i]);
                Sicaklik5Double[i] = Convert.ToDouble(Form1.formCon.sicaklik5_2[i]);
                Sicaklik6Double[i] = Convert.ToDouble(Form1.formCon.sicaklik6_2[i]);
                Sicaklik7Double[i] = Convert.ToDouble(Form1.formCon.sicaklik7_2[i]);
                Sicaklik8Double[i] = Convert.ToDouble(Form1.formCon.sicaklik8_2[i]);
                Sicaklik9Double[i] = Convert.ToDouble(Form1.formCon.sicaklik9_2[i]);
                Sicaklik10Double[i] = Convert.ToDouble(Form1.formCon.sicaklik10_2[i]);
                Sicaklik11Double[i] = Convert.ToDouble(Form1.formCon.sicaklik11_2[i]);
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
            var sicaklik1 = formsPlot1.Plot.AddScatter(dates, Sicaklik1Double, Color.DarkMagenta, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 1");
            var sicaklik2 = formsPlot1.Plot.AddScatter(dates, Sicaklik2Double, Color.Orange, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 2");
            var sicaklik3 = formsPlot1.Plot.AddScatter(dates, Sicaklik3Double, Color.SeaGreen, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 3");
            var sicaklik4 = formsPlot1.Plot.AddScatter(dates, Sicaklik4Double, Color.Blue, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 4");
            var sicaklik5 = formsPlot1.Plot.AddScatter(dates, Sicaklik5Double, Color.Crimson, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 5");
            var sicaklik6 = formsPlot1.Plot.AddScatter(dates, Sicaklik6Double, Color.Coral, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 6");
            var sicaklik7 = formsPlot1.Plot.AddScatter(dates, Sicaklik7Double, Color.Firebrick, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 7");
            var sicaklik8 = formsPlot1.Plot.AddScatter(dates, Sicaklik8Double, Color.Violet, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 8");
            var sicaklik9 = formsPlot1.Plot.AddScatter(dates, Sicaklik9Double, Color.Cyan, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 9");
            var sicaklik10 = formsPlot1.Plot.AddScatter(dates, Sicaklik10Double, Color.DeepPink, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SICAKLIK 10");
            var sicaklik11 = formsPlot1.Plot.AddScatter(dates, Sicaklik11Double, Color.Chocolate, 5, 10, MarkerShape.filledCircle, LineStyle.Solid, "SUYUN SICAKLIĞI");
            formsPlot1.Refresh();

        }
        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            formsPlot1.Reset();
            sicaklikbutton.Visible = false;
            enerjibutton.Visible = false;
            Otherbutton.Visible = false;
        }
    }
}

