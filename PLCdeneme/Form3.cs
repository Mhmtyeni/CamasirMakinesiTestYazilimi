using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows;
using MessageBox = System.Windows.Forms.MessageBox;
using PLCdeneme.Class;
using System.Threading;
using System.Data.SqlClient;

namespace PLCdeneme
{
    public partial class Form3 : Form
    {
        //============WebSensör
        WebSensorProcess webSensor, webSensor1;
        Thread thread;
        SqlConnection connection;
        SqlDataAdapter da;
        private string whichSensor = "SensorOne_1";
        //===============

        public static PlcToPc PlcToPc = new PlcToPc();
        public static PcToPlc PcToPlc = new PcToPlc();
        public static double[] SicaklikTutucu = new double[11];
        public static double[] EnerjiTutucu = new double[5];
        public static double[] DigerDegerlerTutucu = new double[7];
        public static bool excelYazdirma, grafikacma, excelYazdirma2, grafikStop;
        public static double[] T_OtherValues = new double[7];
        public static double[] T_Sicaklik = new double[11];
        public static double[] T_Enerji = new double[5];
        public static DateTime baslangic_zamani, bitis_zamani;
        public Form3()
        {
            InitializeComponent();
        }
        public void DataReading()
        {
            while (thread.ThreadState == ThreadState.Running)
            {
                List<Item> data = new List<Item>();
                data = webSensor.GetData();
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            zaman.Enabled = true;
            zaman.Start();
            foreach (Control item in this.Controls)
            {
                if (item is Guna.UI2.WinForms.Guna2NumericUpDown)
                {
                    Guna.UI2.WinForms.Guna2NumericUpDown n1 = (Guna.UI2.WinForms.Guna2NumericUpDown)item;
                    n1.DecimalPlaces = 2;
                }
            }
        }
        private void zaman_Tick(object sender, EventArgs e)
        {
            label42.Text = DateTime.Now.ToShortDateString();
            label41.Text = DateTime.Now.ToLongTimeString();
        }
        ////Plc Start ve  Değerleri Okumaya Başlama
        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            try
            {
                ////=======================WebSensör
                //connection = new SqlConnection("Server=10.3.25.106,1433;Database=AkustikTest;User Id=testAkustikUSer;Password=A4ksr9-z27u3r;");
                CheckForIllegalCrossThreadCalls = false;
                webSensor = new WebSensorProcess("http://192.168.0.213/values.xml", "SensorOne", 23, 3, 50, 20);
                thread = new Thread(DataReading);
                thread.Start();
                ////==============================
                PcToPlc.Tambur_Slot_Sayısı = Form1.tambur_sayisi;
                PcToPlc.System_Start = true;
                PcToPlc.System_Stop = false;
                Form1.myPlc.WriteClass(PcToPlc, 23);
                guna2TextBox1.Text = DateTime.Now.ToLongTimeString();
                timer1.Enabled = true;
                timer1.Start();
                excelYazdirma = false;
                excelYazdirma2 = true;
                grafikacma = true;
            }
            catch
            {
                guna2MessageDialog1.Show("LÜTFEN PLC BAĞLANTINIZI \nKONTROL EDİNİZ!");
            }
        }
        ////Testi Sonlandır
        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {
            try
            {
                grafikStop = true;
                PcToPlc.System_Start = false;
                PcToPlc.System_Stop = true;
                Form1.myPlc.WriteClass(PcToPlc, 23);
                guna2TextBox2.Text = DateTime.Now.ToLongTimeString();
                DateTime trh1 = Convert.ToDateTime(guna2TextBox1.Text);
                DateTime trh2 = Convert.ToDateTime(guna2TextBox2.Text);
                TimeSpan sonuc = trh2 - trh1;
                guna2TextBox3.Text = sonuc.ToString();
                timer1.Stop();
                Form1.formCon.Disconnect();
                excelYazdirma = true;
                ////yeni eklendi///
                baslangic_zamani = Convert.ToDateTime(guna2TextBox1.Text);
                bitis_zamani = Convert.ToDateTime(guna2TextBox2.Text);
                //thread.Abort();
                //webSensor.ThreadAbord();

            }
            catch
            {
                guna2MessageDialog1.Show("LÜTFEN PLC BAĞLANTINIZI \nKONTROL EDİNİZ!");
            }

        }
        ////Plcden Alınan ve Kalibrasyonlu Verilerin Anlık TextBoxa Yazdırılması
        private void timer1_Tick(object sender, EventArgs e)
        {
            #region 'İLK DEĞERLER'
            Form1.myPlc.ReadClass(PlcToPc, 12);
            sicaklik1_text.Text = PlcToPc.termo_send1.ToString();
            sicaklik2_text.Text = PlcToPc.termo_send2.ToString();
            sicaklik3_text.Text = PlcToPc.termo_send3.ToString();
            sicaklik4_text.Text = PlcToPc.termo_send4.ToString();
            sicaklik5_text.Text = PlcToPc.termo_send5.ToString();
            sicaklik6_text.Text = PlcToPc.termo_send6.ToString();
            sicaklik7_text.Text = PlcToPc.termo_send7.ToString();
            sicaklik8_text.Text = PlcToPc.termo_send8.ToString();
            sicaklik9_text.Text = PlcToPc.termo_send9.ToString();
            SuSicaklik_text.Text = PlcToPc.termo_send10.ToString();
            sicaklik10_text.Text = PlcToPc.termo_send11.ToString();
            /////////////////////////////////////////////////
            frekans_text.Text = PlcToPc.energy_send1.ToString();
            PowerFactor_text.Text = PlcToPc.energy_send2.ToString();
            voltaj_text.Text = PlcToPc.energy_send3.ToString();
            akim_text.Text = PlcToPc.energy_send4.ToString();
            TotalPower_text.Text = PlcToPc.energy_send6.ToString();
            //////////////////////////////////////////////////////
            rpm_text.Text = (60 * PlcToPc.RPM_Send2s).ToString();
            RpmMax_text.Text = (60 * PlcToPc.RPM_MAX_Result).ToString();
            ToplamGuc_text.Text = PlcToPc.SUM_OF_TOTAL_POWER.ToString();
            SuAkisHizi_text.Text = PlcToPc.FlowMeter.ToString();
            SuBasinci_text.Text = PlcToPc.Liquid_Pressure.ToString();
            ToplamSuAkisHizi_text.Text = PlcToPc.FlowMeter_Max.ToString();
            ToplamSuBasinci_text.Text = PlcToPc.Liquid_Pressure_Max.ToString();
            #endregion
            #region 'KALİBRASYONLU DEĞERLER'
            K_sicaklik1_text.Text = (PlcToPc.termo_send1 + SicaklikTutucu[0]).ToString();
            K_sicaklik2_text.Text = (PlcToPc.termo_send2 + SicaklikTutucu[1]).ToString();
            K_sicaklik3_text.Text = (PlcToPc.termo_send3 + SicaklikTutucu[2]).ToString();
            K_sicaklik4_text.Text = (PlcToPc.termo_send4 + SicaklikTutucu[3]).ToString();
            K_sicaklik5_text.Text = (PlcToPc.termo_send5 + SicaklikTutucu[4]).ToString();
            K_sicaklik6_text.Text = (PlcToPc.termo_send6 + SicaklikTutucu[5]).ToString();
            K_sicaklik7_text.Text = (PlcToPc.termo_send7 + SicaklikTutucu[6]).ToString();
            K_sicaklik8_text.Text = (PlcToPc.termo_send8 + SicaklikTutucu[7]).ToString();
            K_sicaklik9_text.Text = (PlcToPc.termo_send9 + SicaklikTutucu[8]).ToString();
            K_SuSicaklik_text.Text = (PlcToPc.termo_send10 + SicaklikTutucu[9]).ToString();
            K_sicaklik10_text.Text = (PlcToPc.termo_send11 + SicaklikTutucu[10]).ToString();
            T_Sicaklik[0] = Convert.ToDouble(K_sicaklik1_text.Text);
            T_Sicaklik[1] = Convert.ToDouble(K_sicaklik2_text.Text);
            T_Sicaklik[2] = Convert.ToDouble(K_sicaklik3_text.Text);
            T_Sicaklik[3] = Convert.ToDouble(K_sicaklik4_text.Text);
            T_Sicaklik[4] = Convert.ToDouble(K_sicaklik5_text.Text);
            T_Sicaklik[5] = Convert.ToDouble(K_sicaklik6_text.Text);
            T_Sicaklik[6] = Convert.ToDouble(K_sicaklik7_text.Text);
            T_Sicaklik[7] = Convert.ToDouble(K_sicaklik8_text.Text);
            T_Sicaklik[8] = Convert.ToDouble(K_sicaklik9_text.Text);
            T_Sicaklik[9] = Convert.ToDouble(K_SuSicaklik_text.Text);
            T_Sicaklik[10] = Convert.ToDouble(K_sicaklik10_text.Text);
            /////////////////////////////////////////////////////////      
            K_frekans_text.Text = (PlcToPc.energy_send1 + EnerjiTutucu[0]).ToString();
            K_PowerFactor_text.Text = (PlcToPc.energy_send2 + EnerjiTutucu[1]).ToString();
            K_voltaj_text.Text = (PlcToPc.energy_send3 + EnerjiTutucu[2]).ToString();
            K_akim_text.Text = (PlcToPc.energy_send4 + EnerjiTutucu[3]).ToString();
            K_TotalPower_text.Text = (PlcToPc.energy_send6 + EnerjiTutucu[4]).ToString();
            T_Enerji[0] = Convert.ToDouble(K_frekans_text.Text);
            T_Enerji[1] = Convert.ToDouble(K_PowerFactor_text.Text);
            T_Enerji[2] = Convert.ToDouble(K_voltaj_text.Text);
            T_Enerji[3] = Convert.ToDouble(K_akim_text.Text);
            T_Enerji[4] = Convert.ToDouble(K_TotalPower_text.Text);
            /////////////////////////////////////////////////////////////            
            K_rpm_text.Text = ((60 * PlcToPc.RPM_Send2s) + DigerDegerlerTutucu[0]).ToString();
            K_RpmMax_text.Text = ((60 * PlcToPc.RPM_MAX_Result) + DigerDegerlerTutucu[1]).ToString();
            K_ToplamGuc_text.Text = (PlcToPc.SUM_OF_TOTAL_POWER + DigerDegerlerTutucu[2]).ToString();
            K_SuAkisHizi_text.Text = (PlcToPc.FlowMeter + DigerDegerlerTutucu[3]).ToString();
            K_SuBasinci_text.Text = (PlcToPc.Liquid_Pressure + DigerDegerlerTutucu[4]).ToString();
            K_ToplamSuAkisHizi_text.Text = (PlcToPc.FlowMeter_Max + DigerDegerlerTutucu[5]).ToString();
            K_ToplamSuBasinci_text.Text = (PlcToPc.Liquid_Pressure_Max + DigerDegerlerTutucu[6]).ToString();
            T_OtherValues[0] = Convert.ToDouble(K_rpm_text.Text);
            T_OtherValues[1] = Convert.ToDouble(K_RpmMax_text.Text);
            T_OtherValues[2] = Convert.ToDouble(K_ToplamGuc_text.Text);
            T_OtherValues[3] = Convert.ToDouble(K_SuAkisHizi_text.Text);
            T_OtherValues[4] = Convert.ToDouble(K_SuBasinci_text.Text);
            T_OtherValues[5] = Convert.ToDouble(K_ToplamSuAkisHizi_text.Text);
            T_OtherValues[6] = Convert.ToDouble(K_ToplamSuBasinci_text.Text);
            #endregion
            if (PlcToPc.Direction == 2)
            {
                pictureBox2.Visible = false;
                pictureBox3.Visible = true;
            }
            else
            {
                pictureBox3.Visible = false;
                pictureBox2.Visible = true;
            }
            Form1.formCon.Add();
            Form1.formCon.Add2();
            Form1.formCon.Add3();
            Form1.formCon.ReadWebSensor();
            sensorSicaklikTxt.Text = Form1.formCon.temp;
            sensorNemTxt.Text = Form1.formCon.humid;
            sensorBasincTxt.Text = Form1.formCon.pressure;
        }
        ////Grafik Formun Açılması
        private void guna2ImageButton4_Click(object sender, EventArgs e)
        {
            if (grafikacma)
            {
                Form1.frm2.Show();
                //this.Hide();
            }
            else
                guna2MessageDialog1.Show("LÜTFEN ÖNCE TESTE \nBAŞLAYINIZ");
        }
        ////Dark ve Light Mode
        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch1.Checked == true)
            {
                darkmode.Visible = true;
                lightmode.Visible = false;
                foreach (Control item in this.Controls)
                {
                    if (item is Guna.UI2.WinForms.Guna2TextBox)
                    {
                        Guna.UI2.WinForms.Guna2TextBox tbox = (Guna.UI2.WinForms.Guna2TextBox)item;
                        tbox.ForeColor = Color.White;
                        tbox.FillColor = Color.Black;
                    }
                    if (item is System.Windows.Forms.Label)
                    {
                        System.Windows.Forms.Label tlabel = (System.Windows.Forms.Label)item;
                        tlabel.ForeColor = Color.White;
                    }
                    if (item is Guna.UI2.WinForms.Guna2NumericUpDown)
                    {
                        Guna.UI2.WinForms.Guna2NumericUpDown numericBox = (Guna.UI2.WinForms.Guna2NumericUpDown)item;
                        numericBox.ForeColor = Color.White;
                        numericBox.FillColor = Color.Black;
                    }
                    Form3.ActiveForm.BackColor = Color.FromArgb(18, 22, 27);
                }
            }
            else
            {
                darkmode.Visible = false;
                lightmode.Visible = true;
                foreach (Control item in this.Controls)
                {
                    if (item is Guna.UI2.WinForms.Guna2TextBox)
                    {
                        Guna.UI2.WinForms.Guna2TextBox tbox = (Guna.UI2.WinForms.Guna2TextBox)item;
                        tbox.ForeColor = Color.Black;
                        tbox.FillColor = Color.White;
                    }
                    if (item is Guna.UI2.WinForms.Guna2NumericUpDown)
                    {
                        Guna.UI2.WinForms.Guna2NumericUpDown numericBox = (Guna.UI2.WinForms.Guna2NumericUpDown)item;
                        numericBox.ForeColor = Color.Black;
                        numericBox.FillColor = Color.White;
                    }
                    if (item is System.Windows.Forms.Label)
                    {
                        System.Windows.Forms.Label tlabel = (System.Windows.Forms.Label)item;
                        tlabel.ForeColor = Color.Black;
                    }
                    Form3.ActiveForm.BackColor = Color.LavenderBlush;
                    label42.ForeColor = Color.Firebrick;
                    label41.ForeColor = Color.Firebrick;
                }
            }
        }
        ////Excel KAYIT
        private void guna2ImageButton5_Click(object sender, EventArgs e)
        {
            if (excelYazdirma)
            {
                Form1.formCon.RpmSend.Clear();
                Form1.formCon.Read();
                Form1.formCon.Read2();
                Form1.formCon.Read3();
                Excel.Application xlApp = new Excel.Application();
                xlApp.Visible = true;
                xlApp.DisplayAlerts = true;
                Excel.Workbook wb = xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet ws = (Excel.Worksheet)wb.Sheets[1];
                Excel.Range range;
                range = ws.get_Range("A1", "AA1");
                range.get_Range("A1", "AA1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                range.get_Range("A1", "AA1").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                range.ColumnWidth = 25;
                range.Font.Size = 14;
                range.EntireRow.Font.Bold = true;
                range = ws.get_Range("A1", "K1");
                range.Font.Color = Excel.XlRgbColor.rgbRed;
                range = ws.get_Range("L1", "Q1");
                range.Font.Color = Excel.XlRgbColor.rgbDarkSlateGray;
                range = ws.get_Range("R1", "Y1");
                range.Font.Color = Excel.XlRgbColor.rgbRoyalBlue;
                range = ws.get_Range("Z1", "Z1");
                range.Font.Color = Excel.XlRgbColor.rgbCrimson;
                range = ws.get_Range("AA1", "AA1");
                range.Font.Color = Excel.XlRgbColor.rgbBlack;
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
                ws.Cells[1, 12] = "FREKANS";
                ws.Cells[1, 13] = "POWER FACTOR";
                ws.Cells[1, 14] = "VOLTAJ";
                ws.Cells[1, 15] = "AKIM";
                ws.Cells[1, 16] = "TOTAL POWER";
                ws.Cells[1, 17] = "RPM";
                ws.Cells[1, 18] = "RPM MAX";
                ws.Cells[1, 19] = "TOPLAM GÜÇ";
                ws.Cells[1, 20] = "SU AKIŞ HIZI";
                ws.Cells[1, 21] = "SU BASINCI";
                ws.Cells[1, 22] = "TOPLAM SU AKIŞ HIZI";
                ws.Cells[1, 23] = "TOPLAM SU BASINCI";
                ws.Cells[1, 24] = "DÖNÜŞ YÖNÜ";
                ws.Cells[1, 25] = "MODEL";
                ws.Cells[1, 26] = "TARİH";
                for (int i = 0; i < Form1.formCon.RpmSend.Count; i++)
                {
                    for (int j = 1; j < 27; j++)
                    {
                        ws.Cells[i + 2, j] = Form1.formCon.sicaklik1[i];
                        ws.Cells[i + 2, j + 1] = Form1.formCon.sicaklik2[i];
                        ws.Cells[i + 2, j + 2] = Form1.formCon.sicaklik3[i];
                        ws.Cells[i + 2, j + 3] = Form1.formCon.sicaklik4[i];
                        ws.Cells[i + 2, j + 4] = Form1.formCon.sicaklik5[i];
                        ws.Cells[i + 2, j + 5] = Form1.formCon.sicaklik6[i];
                        ws.Cells[i + 2, j + 6] = Form1.formCon.sicaklik7[i];
                        ws.Cells[i + 2, j + 7] = Form1.formCon.sicaklik8[i];
                        ws.Cells[i + 2, j + 8] = Form1.formCon.sicaklik9[i];
                        ws.Cells[i + 2, j + 9] = Form1.formCon.sicaklik11[i];
                        ws.Cells[i + 2, j + 10] = Form1.formCon.sicaklik10[i];
                        ws.Cells[i + 2, j + 11] = Form1.formCon.energy1[i];
                        ws.Cells[i + 2, j + 12] = Form1.formCon.energy2[i];
                        ws.Cells[i + 2, j + 13] = Form1.formCon.energy3[i];
                        ws.Cells[i + 2, j + 14] = Form1.formCon.energy4[i];
                        ws.Cells[i + 2, j + 15] = Form1.formCon.energy6[i];
                        ws.Cells[i + 2, j + 16] = Form1.formCon.RpmSend[i];
                        ws.Cells[i + 2, j + 17] = Form1.formCon.RpmMax[i];
                        ws.Cells[i + 2, j + 18] = Form1.formCon.SumOfTotal[i];
                        ws.Cells[i + 2, j + 19] = Form1.formCon.FlowMeter[i];
                        ws.Cells[i + 2, j + 20] = Form1.formCon.LiquidPressure[i];
                        ws.Cells[i + 2, j + 21] = Form1.formCon.FlowMeterMax[i];
                        ws.Cells[i + 2, j + 22] = Form1.formCon.LiquidPressureMax[i];
                        ws.Cells[i + 2, j + 23] = Form1.formCon.Direction[i];
                        ws.Cells[i + 2, j + 24] = Form1.model.ToString();
                        ws.Cells[i + 2, j + 25] = Form1.formCon.TarihArry[i].ToString();
                        break;
                    }
                }
            }
            else
                guna2MessageDialog1.Show("TESTİ BİTİRMEDEN EXCELLE \nKAYIT EDEMEZSİNİZ.");
        }
        #region 'SICAKLIK KALİBRASYON AYARI'
        private void btn1_Click(object sender, EventArgs e)
        {
            SicaklikTutucu[0] = Convert.ToSingle(Numeric_Sicaklik1.Value);
            Numeric_Sicaklik1.Value = 0;
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            SicaklikTutucu[1] = Convert.ToSingle(Numeric_Sicaklik2.Value);
            Numeric_Sicaklik2.Value = 0;
        }
        private void btn3_Click(object sender, EventArgs e)
        {
            SicaklikTutucu[2] = Convert.ToSingle(Numeric_Sicaklik3.Value);
            Numeric_Sicaklik3.Value = 0;
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            SicaklikTutucu[3] = Convert.ToSingle(Numeric_Sicaklik4.Value);
            Numeric_Sicaklik4.Value = 0;
        }
        private void btn5_Click(object sender, EventArgs e)
        {
            SicaklikTutucu[4] = Convert.ToSingle(Numeric_Sicaklik5.Value);
            Numeric_Sicaklik5.Value = 0;
        }
        private void btn6_Click(object sender, EventArgs e)
        {
            SicaklikTutucu[5] = Convert.ToSingle(Numeric_Sicaklik6.Value);
            Numeric_Sicaklik6.Value = 0;
        }
        private void btn7_Click(object sender, EventArgs e)
        {
            SicaklikTutucu[6] = Convert.ToSingle(Numeric_Sicaklik7.Value);
            Numeric_Sicaklik7.Value = 0;
        }
        private void btn8_Click(object sender, EventArgs e)
        {
            SicaklikTutucu[7] = Convert.ToSingle(Numeric_Sicaklik8.Value);
            Numeric_Sicaklik8.Value = 0;
        }
        private void btn9_Click(object sender, EventArgs e)
        {
            SicaklikTutucu[8] = Convert.ToSingle(Numeric_Sicaklik9.Value);
            Numeric_Sicaklik9.Value = 0;
        }
        private void btn10_Click(object sender, EventArgs e)
        {
            SicaklikTutucu[9] = Convert.ToSingle(Numeric_Sicaklik10.Value);
            Numeric_Sicaklik10.Value = 0;
        }
        private void btn11_Click(object sender, EventArgs e)
        {
            SicaklikTutucu[10] = Convert.ToSingle(Numeric_SuyunSicakligi.Value);
            Numeric_SuyunSicakligi.Value = 0;
        }
        #endregion
        #region 'ENERJİ KALİBRASYON AYARI'
        private void btn12_Click(object sender, EventArgs e)
        {
            EnerjiTutucu[0] = Convert.ToSingle(Numeric_Frekans.Value);
            Numeric_Frekans.Value = 0;
        }
        private void btn13_Click(object sender, EventArgs e)
        {
            EnerjiTutucu[1] = Convert.ToSingle(Numeric_PowerFacktor.Value);
            Numeric_PowerFacktor.Value = 0;
        }
        private void btn14_Click(object sender, EventArgs e)
        {
            EnerjiTutucu[2] = Convert.ToSingle(Numeric_Voltaj.Value);
            Numeric_Voltaj.Value = 0;
        }
        private void btn15_Click(object sender, EventArgs e)
        {
            EnerjiTutucu[3] = Convert.ToSingle(Numeric_Akim.Value);
            Numeric_Akim.Value = 0;
        }
        private void btn16_Click(object sender, EventArgs e)
        {
            EnerjiTutucu[4] = Convert.ToSingle(Numeric_Enerji5.Value);
            Numeric_Enerji5.Value = 0;
        }
        private void btn17_Click(object sender, EventArgs e)
        {
            EnerjiTutucu[4] = Convert.ToSingle(Numeric_TotalPower.Value);
            Numeric_TotalPower.Value = 0;
        }
        #endregion
        #region 'DİĞER VERİLER KALİBRASYON AYARI'
        private void btn18_Click(object sender, EventArgs e)
        {
            DigerDegerlerTutucu[0] = Convert.ToSingle(Numeric_rpm.Value);
            Numeric_rpm.Value = 0;
        }
        private void btn19_Click(object sender, EventArgs e)
        {
            DigerDegerlerTutucu[1] = Convert.ToSingle(Numeric_Rpm_Max.Value);
            Numeric_Rpm_Max.Value = 0;
        }
        private void btn20_Click(object sender, EventArgs e)
        {
            DigerDegerlerTutucu[2] = Convert.ToSingle(Numeric_ToplamGuc.Value);
            Numeric_ToplamGuc.Value = 0;
        }
        private void btn21_Click(object sender, EventArgs e)
        {
            DigerDegerlerTutucu[3] = Convert.ToSingle(Numeric_SuAkisHizi.Value);
            Numeric_SuAkisHizi.Value = 0;
        }

        //WEB SENSÖR EXCELL
        //{
        //    Excel.Application xlApp = new Excel.Application();
        //    if (xlApp == null)
        //    {
        //        guna2MessageDialog1.Show("Excel Bilgisayarınızda\n Yüklü Değil");
        //        return;
        //    }
        //    xlApp.Visible = true;
        //    xlApp.DisplayAlerts = true;
        //    Excel.Workbook wb = xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
        //    Excel.Sheets xlSayfa = wb.Worksheets;
        //    Excel.Range range;
        //    Form1.formCon.Temperature.Clear();
        //    Form1.formCon.ReadWebSensor();
        //    var xlYeniSayfa4 = (Excel.Worksheet)xlSayfa.Add(xlSayfa[1], Type.Missing, Type.Missing, Type.Missing);
        //    xlYeniSayfa4.Name = "SENSÖR ÖLÇÜM";
        //    range = xlYeniSayfa4.get_Range("A1", "D1");
        //    range.get_Range("A1", "D1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
        //    range.get_Range("A1", "D1").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
        //    range.ColumnWidth = 25;
        //    range.Font.Size = 14;
        //    range.Font.Color = Excel.XlRgbColor.rgbCornflowerBlue;
        //    range.EntireRow.Font.Bold = true;
        //    xlYeniSayfa4.Cells[1, 1] = "SICAKLIK";
        //    xlYeniSayfa4.Cells[1, 2] = "NEM";
        //    xlYeniSayfa4.Cells[1, 3] = "BASINÇ";
        //    xlYeniSayfa4.Cells[1, 4] = "TARİH";
        //    for (int i = 0; i < Form1.formCon.Temperature.Count; i++)
        //    {
        //        for (int j = 1; j < 5; j++)
        //        {
        //            xlYeniSayfa4.Cells[i + 2, j] = Form1.formCon.Temperature[i];
        //            xlYeniSayfa4.Cells[i + 2, j + 1] = Form1.formCon.Humidity[i];
        //            xlYeniSayfa4.Cells[i + 2, j + 2] = Form1.formCon.Pressure[i];
        //            xlYeniSayfa4.Cells[i + 2, j + 3] = Form1.formCon.SensorTarih[i].ToString();
        //            break;
        //        }
        //    }
        //    Form1.formCon.sqlTemizle4();

        //}

        private void btn22_Click(object sender, EventArgs e)
        {
            DigerDegerlerTutucu[4] = Convert.ToSingle(Numeric_SuBasinci.Value);
            Numeric_SuBasinci.Value = 0;
        }
        private void btn23_Click(object sender, EventArgs e)
        {
            DigerDegerlerTutucu[5] = Convert.ToSingle(Numeric_ToplamSuAkis.Value);
            Numeric_ToplamSuAkis.Value = 0;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void btn24_Click(object sender, EventArgs e)
        {
            DigerDegerlerTutucu[6] = Convert.ToSingle(Numeric_ToplamSuBasinci.Value);
            Numeric_ToplamSuBasinci.Value = 0;
        }
        #endregion         
        //////Yeni Teste Geçiş ve Sqldeki Verileri Sıfırlayıp Excele Kaydetme
        private void button1_Click(object sender, EventArgs e)
        {
            Form1.formCon.TarihRead();
            if (excelYazdirma2)
            {
                if (excelYazdirma)
                {
                    DialogResult dialog = new DialogResult();
                    dialog = MessageBox.Show("YENİ TESTE GEÇMEK İSTEDİĞİNİZE EMİN MİSİNİZ ?", "BİLGİLENDİRME", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    MessageBox.Show("VERİLER SİLİNECEK EXCEL DOSYASINI KAYDETMEYİ UNUTMAYIN!!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.Yes)
                    {
                        Form1.formCon.RpmSend.Clear();
                        Form1.formCon.Read();
                        Excel.Application xlApp = new Excel.Application();
                        if (xlApp == null)
                        {
                            guna2MessageDialog1.Show("Excel Bilgisayarınızda\n Yüklü Değil");
                            return;
                        }
                        xlApp.Visible = true;
                        xlApp.DisplayAlerts = true;
                        Excel.Workbook wb = xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                        Excel.Sheets xlSayfa = wb.Worksheets;
                        var xlYeniSayfa = (Excel.Worksheet)xlSayfa.Add(xlSayfa[1], Type.Missing, Type.Missing, Type.Missing);
                        xlYeniSayfa.Name = "DİĞER DEĞERLER GRAFİĞİ";
                        Excel.Range range;
                        range = xlYeniSayfa.get_Range("A1", "J1");
                        range.get_Range("A1", "J1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        range.get_Range("A1", "J1").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        range.ColumnWidth = 25;
                        range.Font.Size = 14;
                        range.Font.Color = Excel.XlRgbColor.rgbRoyalBlue;
                        range.EntireRow.Font.Bold = true;
                        xlYeniSayfa.Cells[1, 1] = "RPM";
                        xlYeniSayfa.Cells[1, 2] = "RPM MAX";
                        xlYeniSayfa.Cells[1, 3] = "TOPLAM GÜÇ";
                        xlYeniSayfa.Cells[1, 4] = "SU AKIŞ HIZI";
                        xlYeniSayfa.Cells[1, 5] = "SU BASINCI";
                        xlYeniSayfa.Cells[1, 6] = "TOPLAM SU AKIŞ HIZI";
                        xlYeniSayfa.Cells[1, 7] = "TOPLAM SU BASINCI";
                        xlYeniSayfa.Cells[1, 8] = "DÖNÜŞ YÖNÜ";
                        xlYeniSayfa.Cells[1, 9] = "MODEL";
                        xlYeniSayfa.Cells[1, 10] = "TARİH";
                        for (int i = 0; i < Form1.formCon.RpmSend.Count; i++)
                        {
                            for (int j = 1; j < 11; j++)
                            {
                                xlYeniSayfa.Cells[i + 2, j] = Form1.formCon.RpmSend[i];
                                xlYeniSayfa.Cells[i + 2, j + 1] = Form1.formCon.RpmMax[i];
                                xlYeniSayfa.Cells[i + 2, j + 2] = Form1.formCon.SumOfTotal[i];
                                xlYeniSayfa.Cells[i + 2, j + 3] = Form1.formCon.FlowMeter[i];
                                xlYeniSayfa.Cells[i + 2, j + 4] = Form1.formCon.LiquidPressure[i];
                                xlYeniSayfa.Cells[i + 2, j + 5] = Form1.formCon.FlowMeterMax[i];
                                xlYeniSayfa.Cells[i + 2, j + 6] = Form1.formCon.LiquidPressureMax[i];
                                xlYeniSayfa.Cells[i + 2, j + 7] = Form1.formCon.Direction[i];
                                xlYeniSayfa.Cells[i + 2, j + 8] = Form1.formCon.Model[i];
                                xlYeniSayfa.Cells[i + 2, j + 9] = Form1.formCon.TarihArry[i].ToString();
                                break;
                            }
                        }
                        ////////////////////////////////////////////////////
                        Form1.formCon.energy1.Clear();
                        Form1.formCon.Read3();
                        var xlYeniSayfa2 = (Excel.Worksheet)xlSayfa.Add(xlSayfa[1], Type.Missing, Type.Missing, Type.Missing);
                        xlYeniSayfa2.Name = "ENERJİ GRAFİĞİ";
                        range = xlYeniSayfa2.get_Range("A1", "H1");
                        range.get_Range("A1", "H1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        range.get_Range("A1", "H1").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        range.ColumnWidth = 25;
                        range.Font.Size = 14;
                        range.Font.Color = Excel.XlRgbColor.rgbDarkSlateGray;
                        range.EntireRow.Font.Bold = true;
                        xlYeniSayfa2.Cells[1, 1] = "FREKANS";
                        xlYeniSayfa2.Cells[1, 2] = "POWER FACTOR";
                        xlYeniSayfa2.Cells[1, 3] = "VOLTAJ";
                        xlYeniSayfa2.Cells[1, 4] = "AKIM";
                        xlYeniSayfa2.Cells[1, 5] = "TOTAL POWER";
                        xlYeniSayfa2.Cells[1, 6] = "MODEL";
                        xlYeniSayfa2.Cells[1, 7] = "TARİH";
                        for (int i = 0; i < Form1.formCon.energy1.Count; i++)
                        {
                            for (int j = 1; j < 8; j++)
                            {
                                xlYeniSayfa2.Cells[i + 2, j] = Form1.formCon.energy1[i];
                                xlYeniSayfa2.Cells[i + 2, j + 1] = Form1.formCon.energy2[i];
                                xlYeniSayfa2.Cells[i + 2, j + 2] = Form1.formCon.energy3[i];
                                xlYeniSayfa2.Cells[i + 2, j + 3] = Form1.formCon.energy4[i];
                                xlYeniSayfa2.Cells[i + 2, j + 4] = Form1.formCon.energy6[i];
                                xlYeniSayfa2.Cells[i + 2, j + 5] = Form1.formCon.Model3[i];
                                xlYeniSayfa2.Cells[i + 2, j + 6] = Form1.formCon.TarihArry[i].ToString();
                                break;
                            }
                        }
                        ///////////////////////
                        Form1.formCon.sicaklik1.Clear();
                        Form1.formCon.Read2();
                        var xlYeniSayfa3 = (Excel.Worksheet)xlSayfa.Add(xlSayfa[1], Type.Missing, Type.Missing, Type.Missing);
                        xlYeniSayfa3.Name = "SICAKLIK GRAFİĞİ";
                        range = xlYeniSayfa3.get_Range("A1", "M1");
                        range.get_Range("A1", "M1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        range.get_Range("A1", "M1").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        range.ColumnWidth = 25;
                        range.Font.Size = 14;
                        range.Font.Color = Excel.XlRgbColor.rgbRed;
                        range.EntireRow.Font.Bold = true;
                        xlYeniSayfa3.Cells[1, 1] = "SICAKLIK 1";
                        xlYeniSayfa3.Cells[1, 2] = "SICAKLIK 2";
                        xlYeniSayfa3.Cells[1, 3] = "SICAKLIK 3";
                        xlYeniSayfa3.Cells[1, 4] = "SICAKLIK 4";
                        xlYeniSayfa3.Cells[1, 5] = "SICAKLIK 5";
                        xlYeniSayfa3.Cells[1, 6] = "SICAKLIK 6";
                        xlYeniSayfa3.Cells[1, 7] = "SICAKLIK 7";
                        xlYeniSayfa3.Cells[1, 8] = "SICAKLIK 8";
                        xlYeniSayfa3.Cells[1, 9] = "SICAKLIK 9";
                        xlYeniSayfa3.Cells[1, 10] = "SICAKLIK 10";
                        xlYeniSayfa3.Cells[1, 11] = "SUYUN SICAKLIĞI";
                        xlYeniSayfa3.Cells[1, 12] = "MODEL";
                        xlYeniSayfa3.Cells[1, 13] = "TARİH";
                        for (int i = 0; i < Form1.formCon.sicaklik1.Count; i++)
                        {
                            for (int j = 1; j < 14; j++)
                            {
                                xlYeniSayfa3.Cells[i + 2, j] = Form1.formCon.sicaklik1[i];
                                xlYeniSayfa3.Cells[i + 2, j + 1] = Form1.formCon.sicaklik2[i];
                                xlYeniSayfa3.Cells[i + 2, j + 2] = Form1.formCon.sicaklik3[i];
                                xlYeniSayfa3.Cells[i + 2, j + 3] = Form1.formCon.sicaklik4[i];
                                xlYeniSayfa3.Cells[i + 2, j + 4] = Form1.formCon.sicaklik5[i];
                                xlYeniSayfa3.Cells[i + 2, j + 5] = Form1.formCon.sicaklik6[i];
                                xlYeniSayfa3.Cells[i + 2, j + 6] = Form1.formCon.sicaklik7[i];
                                xlYeniSayfa3.Cells[i + 2, j + 7] = Form1.formCon.sicaklik8[i];
                                xlYeniSayfa3.Cells[i + 2, j + 8] = Form1.formCon.sicaklik9[i];
                                xlYeniSayfa3.Cells[i + 2, j + 9] = Form1.formCon.sicaklik11[i];
                                xlYeniSayfa3.Cells[i + 2, j + 10] = Form1.formCon.sicaklik10[i];
                                xlYeniSayfa3.Cells[i + 2, j + 11] = Form1.formCon.Model2[i];
                                xlYeniSayfa3.Cells[i + 2, j + 12] = Form1.formCon.TarihArry[i].ToString();
                                break;
                            }
                        }
                        ///////////////////////////////////////////////////////////////////
                        Form1.formCon.Temperature.Clear();
                        Form1.formCon.ReadWebSensor();
                        var xlYeniSayfa4 = (Excel.Worksheet)xlSayfa.Add(xlSayfa[1], Type.Missing, Type.Missing, Type.Missing);
                        xlYeniSayfa4.Name = "SENSÖR ÖLÇÜM";
                        range = xlYeniSayfa4.get_Range("A1", "D1");
                        range.get_Range("A1", "D1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        range.get_Range("A1", "D1").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        range.ColumnWidth = 25;
                        range.Font.Size = 14;
                        range.Font.Color = Excel.XlRgbColor.rgbDarkSlateGray;
                        range.EntireRow.Font.Bold = true;
                        xlYeniSayfa4.Cells[1, 1] = "SICAKLIK";
                        xlYeniSayfa4.Cells[1, 2] = "NEM";
                        xlYeniSayfa4.Cells[1, 3] = "BASINÇ";
                        xlYeniSayfa4.Cells[1, 4] = "TARİH";
                        for (int i = 0; i < Form1.formCon.Temperature.Count; i++)
                        {
                            for (int j = 1; j < 5; j++)
                            {
                                xlYeniSayfa4.Cells[i + 2, j] = Form1.formCon.Temperature[i];
                                xlYeniSayfa4.Cells[i + 2, j + 1] = Form1.formCon.Humidity[i];
                                xlYeniSayfa4.Cells[i + 2, j + 2] = Form1.formCon.Pressure[i];
                                xlYeniSayfa4.Cells[i + 2, j + 3] = Form1.formCon.TarihArry[i].ToString();
                                break;
                            }
                        }
                        Form1.formCon.sqlTemizle();
                        Form1.formCon.sqlTemizle2();
                        Form1.formCon.sqlTemizle3();
                        Form1.formCon.sqlTemizle4();
                        Form1.Kapat();
                    }
                }
                else
                    guna2MessageDialog1.Show("TESTİ BİTİRMEDEN YENİ \nTESTE GEÇEMEZSİNİZ.");
            }
            else
                guna2MessageDialog1.Show("BİR TESTE BAŞLAYAYIP \nBİTİRMEDEN BAŞKA \nBİR TESTE GEÇEMEZSİNİZ.");
        }
    }
}
