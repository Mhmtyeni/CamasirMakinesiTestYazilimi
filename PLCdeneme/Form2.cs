using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using S7.Net;
using Guna.Charts.WinForms;
using System.Collections;
using System.Windows.Media;
using Color = System.Drawing.Color;
using MindFusion.Charting;
using Brush = MindFusion.Drawing.Brush;
using SolidBrush = MindFusion.Drawing.SolidBrush;
using ScottPlot;

namespace PLCdeneme
{
    public partial class Form2 : Form
    {
        
        public SqlDataReader FormReader;
        #region'GUNA DATASET'
        //DİĞER DEĞERLER
        public GunaLineDataset Ods1 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Ods2 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Ods3 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Ods4 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Ods5 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Ods6 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Ods7 = new Guna.Charts.WinForms.GunaLineDataset();
        //SICAKLIK
        public GunaLineDataset Sds1 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Sds2 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Sds3 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Sds4 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Sds5 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Sds6 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Sds7 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Sds8 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Sds9 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Sds10 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Sds11 = new Guna.Charts.WinForms.GunaLineDataset();
        //ENERJİ
        public GunaLineDataset Eds1 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Eds2 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Eds3 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Eds4 = new Guna.Charts.WinForms.GunaLineDataset();
        public GunaLineDataset Eds5 = new Guna.Charts.WinForms.GunaLineDataset();
        #endregion
        public bool grafik = true;
        public DateTime minTarih, maxTarih;
        public static DateTime MinDate, MaxDate;
        public static string urunModelComboBox, GrafikAdiComboBox;
        public static PlcToPc PlcToPc = new PlcToPc();
        public static PcToPlc PcToPlc = new PcToPlc();   
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Form1.formCon.TarihRead();
            minTarihPicker2.Format = DateTimePickerFormat.Custom;
            minTarihPicker2.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            minTarihPicker2.ShowUpDown = true;
            maxTarihPicker2.Format = DateTimePickerFormat.Custom;
            maxTarihPicker2.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            maxTarihPicker2.ShowUpDown = true;
            //minTarih = Convert.ToDateTime(Form1.formCon.TarihArry.ToArray().Min());
            //maxTarih = Convert.ToDateTime(Form1.formCon.TarihArry.ToArray().Max());
            //minTarihPicker2.MaxDate = maxTarih;
            //minTarihPicker2.MinDate = minTarih.AddSeconds(-1);
            //maxTarihPicker2.MaxDate = maxTarih.AddSeconds(1);
            //maxTarihPicker2.MinDate = minTarih;
            //Form1.myPlc.ReadClass(PlcToPc, 12);
            //TotalpowerText.Text = (PlcToPc.energy_send6 + Form3.EnerjiTutucu[4]).ToString();
            //rpmmaxText.Text = (PlcToPc.RPM_MAX_Result + Form3.DigerDegerlerTutucu[1]).ToString();
            //toplamgucText.Text = (PlcToPc.SUM_OF_TOTAL_POWER + Form3.DigerDegerlerTutucu[2]).ToString();
            //toplamsuakisText.Text = (PlcToPc.FlowMeter_Max + Form3.DigerDegerlerTutucu[5]).ToString();
            //toplamsubasinciText.Text = (PlcToPc.Liquid_Pressure_Max + Form3.DigerDegerlerTutucu[6]).ToString();     
            if (Form3.bitis_zamani > Convert.ToDateTime("00:00:00"))
            {
                minTarihPicker2.Value = Form3.baslangic_zamani;
                maxTarihPicker2.Value = Form3.bitis_zamani;
            }
            #region ' DİĞER DEĞERLER GRAFİĞİ'
            Ods1.DataPoints.Add(DateTime.Now.ToString(),Form3.T_OtherValues[0]);
            Ods2.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_OtherValues[1]);
            Ods3.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_OtherValues[2]);
            Ods4.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_OtherValues[3]);
            Ods5.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_OtherValues[4]);
            Ods6.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_OtherValues[5]);
            Ods7.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_OtherValues[6]);
            gunaChart1.Update();     
            if (Ods1.DataPointCount > 100)
            {
                for (int i = 0; i < 20; i++)
                {
                    Ods1.DataPoints.RemoveAt(i);
                    Ods2.DataPoints.RemoveAt(i);
                    Ods3.DataPoints.RemoveAt(i);
                    Ods4.DataPoints.RemoveAt(i);
                    Ods5.DataPoints.RemoveAt(i);
                    Ods6.DataPoints.RemoveAt(i);
                    Ods7.DataPoints.RemoveAt(i);
                }
            }
            #endregion
            #region ' ENERJİ GRAFİĞİ'
            Eds1.DataPoints.Add(DateTime.Now.ToString(), Form3.T_Enerji[0]);
            Eds2.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_Enerji[1]);
            Eds3.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_Enerji[2]);
            Eds4.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_Enerji[3]);
            Eds5.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_Enerji[4]);            
            gunaChart2.Update();
            if (Eds2.DataPointCount > 100)
            {
                for (int i = 0; i < 20; i++)
                {
                    Eds1.DataPoints.RemoveAt(i);
                    Eds2.DataPoints.RemoveAt(i);
                    Eds3.DataPoints.RemoveAt(i);
                    Eds4.DataPoints.RemoveAt(i);
                    Eds5.DataPoints.RemoveAt(i);     
                }
            }
            #endregion
            #region ' SICAKLIK GRAFİĞİ'
            Sds1.DataPoints.Add(DateTime.Now.ToString(), Form3.T_Sicaklik[0]);
            Sds2.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_Sicaklik[1]);
            Sds3.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_Sicaklik[2]);
            Sds4.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_Sicaklik[3]);
            Sds5.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_Sicaklik[4]);
            Sds6.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_Sicaklik[5]);
            Sds7.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_Sicaklik[6]);
            Sds8.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_Sicaklik[7]);
            Sds9.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_Sicaklik[8]);
            Sds10.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_Sicaklik[10]);
            Sds11.DataPoints.Add(DateTime.Now.ToString().ToString().Substring(10), Form3.T_Sicaklik[9]);
            gunaChart3.Update();
            if (Sds1.DataPointCount > 100)
            {
                for (int i = 0; i < 20; i++)
                {
                    Sds1.DataPoints.RemoveAt(i);
                    Sds2.DataPoints.RemoveAt(i);
                    Sds3.DataPoints.RemoveAt(i);
                    Sds4.DataPoints.RemoveAt(i);
                    Sds5.DataPoints.RemoveAt(i);
                    Sds6.DataPoints.RemoveAt(i);
                    Sds7.DataPoints.RemoveAt(i);
                    Sds8.DataPoints.RemoveAt(i);
                    Sds9.DataPoints.RemoveAt(i);
                    Sds10.DataPoints.RemoveAt(i);
                    Sds11.DataPoints.RemoveAt(i);
                }
            }
            #endregion
        }        
        private void OtherValuesDataSetCreate()
        {
            gunaChart1.YAxes.GridLines.Display = false;
            Form1.formCon.Read();            
            Ods1.PointRadius = 3;
            Ods1.PointStyle = PointStyle.Circle;
            Ods1.Label = "RPM";
            Ods1.BorderColor = Color.DarkMagenta;
            Ods1.FillColor = Color.DarkMagenta;
            Ods1.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.RpmSend.Count, Color.DarkMagenta);
            Ods1.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.RpmSend.Count, Color.DarkMagenta);            
            /////////////
            Ods2.PointRadius = 3;
            Ods2.PointStyle = PointStyle.Circle;
            Ods2.Label = "RPM MAX";
            Ods2.BorderColor = Color.Orange;
            Ods2.FillColor = Color.Orange;
            Ods2.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.RpmSend.Count, Color.Orange);
            Ods2.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.RpmSend.Count, Color.Orange);
            /////////////////
            Ods3.PointRadius = 3;
            Ods3.PointStyle = PointStyle.Circle;
            Ods3.Label = "TOPLAM GÜÇ";
            Ods3.BorderColor = Color.SeaGreen;
            Ods3.FillColor = Color.SeaGreen;
            Ods3.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.RpmSend.Count, Color.SeaGreen);
            Ods3.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.RpmSend.Count, Color.SeaGreen);
            ////////////////////
            Ods4.PointRadius = 3;
            Ods4.PointStyle = PointStyle.Circle;
            Ods4.Label = "SU AKIŞ HIZI";
            Ods4.BorderColor = Color.Blue;
            Ods4.FillColor = Color.Blue;
            Ods4.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.RpmSend.Count, Color.Blue);
            Ods4.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.RpmSend.Count, Color.Blue);
            ///////////////////////////
            Ods5.PointRadius = 3;
            Ods5.PointStyle = PointStyle.Circle;
            Ods5.Label = "SU BASINCI";
            Ods5.BorderColor = Color.Crimson;
            Ods5.FillColor = Color.Crimson;
            Ods5.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.RpmSend.Count, Color.Crimson);
            Ods5.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.RpmSend.Count, Color.Crimson);
            ///////////////////////////
            Ods6.PointRadius = 3;
            Ods6.PointStyle = PointStyle.Circle;
            Ods6.Label = "TOPLAM SU AKIŞ HIZI";
            Ods6.BorderColor = Color.Coral;
            Ods6.FillColor = Color.Coral;
            Ods6.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.RpmSend.Count, Color.Coral);
            Ods6.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.RpmSend.Count, Color.Coral);
            ///////////////////////////
            Ods7.PointRadius = 3;
            Ods7.PointStyle = PointStyle.Circle;
            Ods7.Label = "TOPLAM SU BASINCI";
            Ods7.BorderColor = Color.Firebrick;
            Ods7.FillColor = Color.Firebrick;
            Ods7.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.RpmSend.Count, Color.Firebrick);
            Ods7.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.RpmSend.Count, Color.Firebrick);
        }
        private void EnerjiDataSetCreate()
        {
            gunaChart2.YAxes.GridLines.Display = false;
            Form1.formCon.Read3();
            Eds1.PointRadius = 3;
            Eds1.PointStyle = PointStyle.Circle;
            Eds1.Label = "FREKANS";
            Eds1.BorderColor = Color.DarkMagenta;
            Eds1.FillColor = Color.DarkMagenta;
            Eds1.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.energy1.Count, Color.DarkMagenta);
            Eds1.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.energy1.Count, Color.DarkMagenta);
            /////////////
            Eds2.PointRadius = 3;
            Eds2.PointStyle = PointStyle.Circle;
            Eds2.Label = "POWER FACKTOR";
            Eds2.BorderColor = Color.Orange;
            Eds2.FillColor = Color.Orange;
            Eds2.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.energy1.Count, Color.Orange);
            Eds2.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.energy1.Count, Color.Orange);
            /////////////////
            Eds3.PointRadius = 3;
            Eds3.PointStyle = PointStyle.Circle;
            Eds3.Label = "VOLTAJ";
            Eds3.BorderColor = Color.SeaGreen;
            Eds3.FillColor = Color.SeaGreen;
            Eds3.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.energy1.Count, Color.SeaGreen);
            Eds3.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.energy1.Count, Color.SeaGreen);
            ///////////////////
            Eds4.PointRadius = 3;
            Eds4.PointStyle = PointStyle.Circle;
            Eds4.Label = "AKIM";
            Eds4.BorderColor = Color.Blue;
            Eds4.FillColor = Color.Blue;
            Eds4.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.energy1.Count, Color.Blue);
            Eds4.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.energy1.Count, Color.Blue);
            ///////////////////////////
            Eds5.PointRadius = 3;
            Eds5.PointStyle = PointStyle.Circle;
            Eds5.Label = "TOTAL POWER";
            Eds5.BorderColor = Color.Coral;
            Eds5.FillColor = Color.Coral;
            Eds5.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.energy1.Count, Color.Coral);
            Eds5.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.energy1.Count, Color.Coral);
            ///////////////////////////            
        }
        private void SicaklikDataSetCreate()
        {
            gunaChart3.YAxes.GridLines.Display = false;
            Form1.formCon.Read2();
            Sds1.PointRadius = 3;
            Sds1.PointStyle = PointStyle.Circle;
            Sds1.Label = "SICAKLIK 1";
            Sds1.BorderColor = Color.DarkMagenta;
            Sds1.FillColor = Color.DarkMagenta;
            Sds1.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.DarkMagenta);
            Sds1.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.DarkMagenta);
            /////////////
            Sds2.PointRadius = 3;
            Sds2.PointStyle = PointStyle.Circle;
            Sds2.Label = "SICAKLIK 2";
            Sds2.BorderColor = Color.Orange;
            Sds2.FillColor = Color.Orange;
            Sds2.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Orange);
            Sds2.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Orange);
            /////////////////
            Sds3.PointRadius = 3;
            Sds3.PointStyle = PointStyle.Circle;
            Sds3.Label = "SICAKLIK 3";
            Sds3.BorderColor = Color.SeaGreen;
            Sds3.FillColor = Color.SeaGreen;
            Sds3.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.SeaGreen);
            Sds3.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.SeaGreen);
            ////////////////////
            Sds4.PointRadius = 3;
            Sds4.PointStyle = PointStyle.Circle;
            Sds4.Label = "SICAKLIK 4";
            Sds4.BorderColor = Color.Blue;
            Sds4.FillColor = Color.Blue;
            Sds4.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Blue);
            Sds4.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Blue);
            ///////////////////////////
            Sds5.PointRadius = 3;
            Sds5.PointStyle = PointStyle.Circle;
            Sds5.Label = "SICAKLIK 5";
            Sds5.BorderColor = Color.Crimson;
            Sds5.FillColor = Color.Crimson;
            Sds5.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Crimson);
            Sds5.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Crimson);
            ///////////////////////////
            Sds6.PointRadius = 3;
            Sds6.PointStyle = PointStyle.Circle;
            Sds6.Label = "SICAKLIK 6";
            Sds6.BorderColor = Color.Coral;
            Sds6.FillColor = Color.Coral;
            Sds6.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Coral);            
            Sds6.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Coral);
            ///////////////////////////
            Sds7.PointRadius = 3;
            Sds7.PointStyle = PointStyle.Circle;
            Sds7.Label = "SICAKLIK 7";
            Sds7.BorderColor = Color.Firebrick;
            Sds7.FillColor = Color.Firebrick;
            Sds7.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Firebrick);
            Sds7.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Firebrick);
            ////////////////////
            Sds8.PointRadius = 3;
            Sds8.PointStyle = PointStyle.Circle;
            Sds8.Label = "SICAKLIK 8";
            Sds8.BorderColor = Color.Violet;
            Sds8.FillColor = Color.Violet;
            Sds8.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Violet);
            Sds8.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Violet);
            ///////////////////////////
            Sds9.PointRadius = 3;
            Sds9.PointStyle = PointStyle.Circle;
            Sds9.Label = "SICAKLIK 9";
            Sds9.BorderColor = Color.Cyan;
            Sds9.FillColor = Color.Cyan;
            Sds9.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Cyan);
            Sds9.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Cyan);
            ///////////////////////////
            Sds10.PointRadius = 3;
            Sds10.PointStyle = PointStyle.Circle;
            Sds10.Label = "SICAKLIK 10";
            Sds10.BorderColor = Color.DeepPink;
            Sds10.FillColor = Color.DeepPink;
            Sds10.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.DeepPink);
            Sds10.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.DeepPink);
            ///////////////////////////
            Sds11.PointRadius = 3;
            Sds11.PointStyle = PointStyle.Circle;
            Sds11.Label = "SUYUN SICAKLIĞI ";
            Sds11.BorderColor = Color.Chocolate;
            Sds11.FillColor = Color.Chocolate;
            Sds11.PointFillColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Chocolate);
            Sds11.PointBorderColors = Guna.Charts.WinForms.ChartUtils.Colors(Form1.formCon.sicaklik1.Count, Color.Chocolate);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            if (grafik)
            {
                OtherValuesDataSetCreate();
                gunaChart1.Datasets.Add(Ods1);
                gunaChart1.Datasets.Add(Ods2);
                gunaChart1.Datasets.Add(Ods3);
                gunaChart1.Datasets.Add(Ods4);
                gunaChart1.Datasets.Add(Ods5);
                gunaChart1.Datasets.Add(Ods6);
                gunaChart1.Datasets.Add(Ods7);
                gunaChart1.Update();
                ///////////////////
                EnerjiDataSetCreate();
                gunaChart2.Datasets.Add(Eds1);
                gunaChart2.Datasets.Add(Eds2);
                gunaChart2.Datasets.Add(Eds3);
                gunaChart2.Datasets.Add(Eds4);
                gunaChart2.Datasets.Add(Eds5);
                gunaChart2.Update();
                /////////////////////////
                SicaklikDataSetCreate();
                gunaChart3.Datasets.Add(Sds1);
                gunaChart3.Datasets.Add(Sds2);
                gunaChart3.Datasets.Add(Sds3);
                gunaChart3.Datasets.Add(Sds4);
                gunaChart3.Datasets.Add(Sds5);
                gunaChart3.Datasets.Add(Sds6);
                gunaChart3.Datasets.Add(Sds7);
                gunaChart3.Datasets.Add(Sds8);
                gunaChart3.Datasets.Add(Sds9);
                gunaChart3.Datasets.Add(Sds10);
                gunaChart3.Datasets.Add(Sds11);
                gunaChart3.Update();
                if(Form3.grafikStop == true)
                {
                    timer1.Enabled = true;
                    timer1.Stop();
                }
                else
                {
                    timer1.Enabled = true;
                    timer1.Start();
                }
            }
                     
        }       
        private void guna2ImageButton8_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;          
            timer1.Start();          
        }
        private void guna2ImageButton9_Click(object sender, EventArgs e)
        {
            timer1.Stop();           
        }
        private void Form2_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            grafik = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1.frm3.Show();
            this.Hide();
        }
        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {                         
                GrafikAdiComboBox = guna2ComboBox2.SelectedItem.ToString();
                MinDate = minTarihPicker2.Value;
                MaxDate = maxTarihPicker2.Value;
                Form1.frm4.ShowDialog();
        }
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Form1.myPlc.Close();
            //Application.Exit();
            //Application.Restart(); 
        }
        
        
    }       
}

