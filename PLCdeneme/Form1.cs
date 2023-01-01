using S7.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sharp7;
using PLCdeneme.Connection;
using System.Data.SqlClient;

namespace PLCdeneme
{
    public partial class Form1 : Form
    {
        public static string model;
        public static int tambur_sayisi;
        public static Plc myPlc;
        public static PlcToPc PlcToPc = new PlcToPc();
        public static PcToPlc PcToPlc = new PcToPlc();
        public static SqlConnections formCon = new SqlConnections();
        public static Form2 frm2;
        public static Form3 frm3;
        public static Form4 frm4;
        public Form1()
        {
            frm2 = new Form2();
            frm3 = new Form3();
            frm4 = new Form4();
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            modelText.Text = "";
        }
        private void connectButton_Click(object sender, EventArgs e)
        {
            if (tambur_slot.Text!="0")
            {
            if (modelText.Text != "" && tambur_slot.Text != "") 
            {
                model = modelText.Text.ToString().ToUpper();
                myPlc = new Plc(CpuType.S71500, "192.168.0.1", 0, 1);
                try
                {
                    myPlc.Open();
                    PcToPlc.Tambur_Slot_Sayısı = Convert.ToInt16(tambur_slot.Text);
                    tambur_sayisi= Convert.ToInt16(tambur_slot.Text);
                    Form1.myPlc.WriteClass(PcToPlc, 23);
                    myPlc.ReadClass(PlcToPc, 12);
                    label4.Text = "Bağlantı Başarılı ☑";
                    errorPicture.Visible = false;
                    succesPicture.Visible = true;
                    label4.ForeColor = Color.Green;
                    toolStripStatusLabel1.Text = "PLC Bağlantısı Başarılı ☑";
                    statusStrip1.ForeColor = Color.Green;
                    label4.Visible = true;
                    Form1.formCon.ModelRead();
                    model = modelText.Text.ToString().ToUpper().TrimEnd();
                    formCon.ModelAdd();
                    frm3.Show();
                    this.Hide();
                    
                }
                catch
                {
                    label4.Text = "Bağlantı Başarısız ❎";
                    errorPicture.Visible = true;
                    label4.ForeColor = Color.Red;
                    label4.Visible = true;
                    toolStripStatusLabel1.Text = "PLC Bağlantısı Başarısız ❎";
                    statusStrip1.ForeColor = Color.Red;
                }
            }
            else
                guna2MessageDialog1.Show("ÜRÜN MODEL KODU VEYA\nTAMBUR SLOT SAYISI BOŞ\nBIRAKILAMAZ!");
            }
            else
                guna2MessageDialog1.Show("TAMBUR SLOT SAYISI 0\nOLAMAZ!");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Form Ana_Ekran2 = new Form3();
            //Ana_Ekran2.Show();
            //this.Hide();            
            this.Close();
        }
        private void modelText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 32)
                e.Handled = true;
        }
        public static void Kapat()
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form1.frm3.Show();
        }
    }
}
