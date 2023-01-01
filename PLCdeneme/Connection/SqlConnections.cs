using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using PLCdeneme.Class;


namespace PLCdeneme.Connection
{
    public class SqlConnections
    {
        #region 'DEGISKENLER'
        public static PlcToPc PlcToPc = new PlcToPc();
        public SqlConnection con = new SqlConnection();
        public SqlCommand command;
        public SqlDataReader reader, readerS, readerE, ModelReader, TarihReader, OzelReader, SensorReader;
        public ArrayList Temperature = new ArrayList();
        public ArrayList Humidity = new ArrayList();
        public ArrayList Pressure = new ArrayList();
        public ArrayList SensorTarih = new ArrayList();
        public string temp, humid, pressure;
        public ArrayList RpmSend = new ArrayList();
        public ArrayList RpmMax = new ArrayList();
        public ArrayList SumOfTotal = new ArrayList();
        public ArrayList FlowMeter = new ArrayList();
        public ArrayList LiquidPressure = new ArrayList();
        public ArrayList FlowMeterMax = new ArrayList();
        public ArrayList LiquidPressureMax = new ArrayList();
        public ArrayList Direction = new ArrayList();
        public ArrayList sicaklik1 = new ArrayList();
        public ArrayList sicaklik2 = new ArrayList();
        public ArrayList sicaklik3 = new ArrayList();
        public ArrayList sicaklik4 = new ArrayList();
        public ArrayList sicaklik5 = new ArrayList();
        public ArrayList sicaklik6 = new ArrayList();
        public ArrayList sicaklik7 = new ArrayList();
        public ArrayList sicaklik8 = new ArrayList();
        public ArrayList sicaklik9 = new ArrayList();
        public ArrayList sicaklik10 = new ArrayList();
        public ArrayList sicaklik11 = new ArrayList();
        public ArrayList energy1 = new ArrayList();
        public ArrayList energy2 = new ArrayList();
        public ArrayList energy3 = new ArrayList();
        public ArrayList energy4 = new ArrayList();
        public ArrayList energy5 = new ArrayList();
        public ArrayList energy6 = new ArrayList();
        public ArrayList ModelDeger = new ArrayList();
        public ArrayList TarihArry = new ArrayList();
        public ArrayList TarihArryOtherValues = new ArrayList();
        public ArrayList RpmSend2 = new ArrayList();
        public ArrayList RpmMax2 = new ArrayList();
        public ArrayList SumOfTotal2 = new ArrayList();
        public ArrayList FlowMeter2 = new ArrayList();
        public ArrayList LiquidPressure2 = new ArrayList();
        public ArrayList FlowMeterMax2 = new ArrayList();
        public ArrayList LiquidPressureMax2 = new ArrayList();
        public ArrayList Direction2 = new ArrayList();
        public ArrayList sicaklik1_2 = new ArrayList();
        public ArrayList sicaklik2_2 = new ArrayList();
        public ArrayList sicaklik3_2 = new ArrayList();
        public ArrayList sicaklik4_2 = new ArrayList();
        public ArrayList sicaklik5_2 = new ArrayList();
        public ArrayList sicaklik6_2 = new ArrayList();
        public ArrayList sicaklik7_2 = new ArrayList();
        public ArrayList sicaklik8_2 = new ArrayList();
        public ArrayList sicaklik9_2 = new ArrayList();
        public ArrayList sicaklik10_2 = new ArrayList();
        public ArrayList sicaklik11_2 = new ArrayList();
        public ArrayList energy1_2 = new ArrayList();
        public ArrayList energy2_2 = new ArrayList();
        public ArrayList energy3_2 = new ArrayList();
        public ArrayList energy4_2 = new ArrayList();
        public ArrayList energy5_2 = new ArrayList();
        public ArrayList energy6_2 = new ArrayList();
        public ArrayList Model = new ArrayList();
        public ArrayList Model2 = new ArrayList();
        public ArrayList Model3 = new ArrayList();
        public ArrayList Model_2 = new ArrayList();
        public ArrayList Model2_2 = new ArrayList();
        public ArrayList Model3_2 = new ArrayList();
        public int id, sayac;
        #endregion        
        public void Connect()
        {
            try
            {
                con.ConnectionString = "Data Source=10.3.25.106,1433;Initial Catalog=AkustikTest;User Id=testAkustikUSer;password=A4ksr9-z27u3r;";
                //con.ConnectionString = "Data Source=10.108.206.245,1433;Initial Catalog=AkustikTest;User Id=sa;password=123456;";
                con.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Bağlantı Başarısız", ex.ToString()); ;
            }
        }
        public void Disconnect()
        {
            con.Close();
        }
        ///Sıcaklık Grafiği İçin Veri Yazma
        public void Add()
        {
            Form1.myPlc.ReadClass(PlcToPc, 12);
            string SicaklikSql = "Insert Into Sicaklik (termo_send1,termo_send2,termo_send3,termo_send4,termo_send5,termo_send6,termo_send7,termo_send8,termo_send9,termo_send10,termo_send11,Date,Model) Values (@termo_send1,@termo_send2,@termo_send3,@termo_send4,@termo_send5,@termo_send6,@termo_send7,@termo_send8,@termo_send9,@termo_send10,@termo_send11,@Date,@Model)";
            command = new SqlCommand(SicaklikSql, con);
            command.Parameters.AddWithValue("@termo_send1", PlcToPc.termo_send1 + Form3.SicaklikTutucu[0]);
            command.Parameters.AddWithValue("@termo_send2", PlcToPc.termo_send2 + Form3.SicaklikTutucu[1]);
            command.Parameters.AddWithValue("@termo_send3", PlcToPc.termo_send3 + Form3.SicaklikTutucu[2]);
            command.Parameters.AddWithValue("@termo_send4", PlcToPc.termo_send4 + Form3.SicaklikTutucu[3]);
            command.Parameters.AddWithValue("@termo_send5", PlcToPc.termo_send5 + Form3.SicaklikTutucu[4]);
            command.Parameters.AddWithValue("@termo_send6", PlcToPc.termo_send6 + Form3.SicaklikTutucu[5]);
            command.Parameters.AddWithValue("@termo_send7", PlcToPc.termo_send7 + Form3.SicaklikTutucu[6]);
            command.Parameters.AddWithValue("@termo_send8", PlcToPc.termo_send8 + Form3.SicaklikTutucu[7]);
            command.Parameters.AddWithValue("@termo_send9", PlcToPc.termo_send9 + Form3.SicaklikTutucu[8]);
            command.Parameters.AddWithValue("@termo_send10", PlcToPc.termo_send10 + Form3.SicaklikTutucu[9]);
            command.Parameters.AddWithValue("@termo_send11", PlcToPc.termo_send11 + Form3.SicaklikTutucu[10]);
            command.Parameters.AddWithValue("@Date", DateTime.Now);
            command.Parameters.AddWithValue("@Model", Form1.model);
            Connect();
            command.ExecuteNonQuery();
            Disconnect();
        }
        ///Enerji Grafiği İçin Veri Yazma
        public void Add2()
        {
            Form1.myPlc.ReadClass(PlcToPc, 12);
            string energySql = "Insert Into Energy (energy_send1,energy_send2,energy_send3,energy_send4,energy_send6,Date,Model) Values (@energy_send1,@energy_send2,@energy_send3,@energy_send4,@energy_send6,@Date,@Model)";
            command = new SqlCommand(energySql, con);
            command.Parameters.AddWithValue("@energy_send1", PlcToPc.energy_send1 + Form3.EnerjiTutucu[0]);
            command.Parameters.AddWithValue("@energy_send2", PlcToPc.energy_send2 + Form3.EnerjiTutucu[1]);
            command.Parameters.AddWithValue("@energy_send3", PlcToPc.energy_send3 + Form3.EnerjiTutucu[2]);
            command.Parameters.AddWithValue("@energy_send4", PlcToPc.energy_send4 + Form3.EnerjiTutucu[3]);
            command.Parameters.AddWithValue("@energy_send6", PlcToPc.energy_send6 + Form3.EnerjiTutucu[4]);
            command.Parameters.AddWithValue("@Date", DateTime.Now);
            command.Parameters.AddWithValue("@Model", Form1.model);
            Connect();
            command.ExecuteNonQuery();
            Disconnect();
        }
        ///Diğer Değerler Grafiği İçin Veri Yazma
        public void Add3()
        {
            Form1.myPlc.ReadClass(PlcToPc, 12);
            string othervaluesSql = "Insert Into OtherValues (RPM_Send,Direction,RPM_MAX_Result,SUM_OF_TOTAL_POWER,FlowMeter,Liquid_Pressure,FlowMeter_Max,Liquid_Pressure_Max,Date,Model) Values (@RPM_Send,@Direction,@RPM_MAX_Result,@SUM_OF_TOTAL_POWER,@FlowMeter,@Liquid_Pressure,@FlowMeter_Max,@Liquid_Pressure_Max,@Date,@Model)";
            command = new SqlCommand(othervaluesSql, con);
            command.Parameters.AddWithValue("@RPM_Send", (60 * PlcToPc.RPM_Send2s) + Form3.DigerDegerlerTutucu[0]);
            command.Parameters.AddWithValue("@Direction", PlcToPc.Direction);
            command.Parameters.AddWithValue("@RPM_MAX_Result", (60 * PlcToPc.RPM_MAX_Result) + Form3.DigerDegerlerTutucu[1]);
            command.Parameters.AddWithValue("@SUM_OF_TOTAL_POWER", PlcToPc.SUM_OF_TOTAL_POWER + Form3.DigerDegerlerTutucu[2]);
            command.Parameters.AddWithValue("@FlowMeter", PlcToPc.FlowMeter + Form3.DigerDegerlerTutucu[3]);
            command.Parameters.AddWithValue("@Liquid_Pressure", PlcToPc.Liquid_Pressure + Form3.DigerDegerlerTutucu[4]);
            command.Parameters.AddWithValue("@FlowMeter_Max", PlcToPc.FlowMeter_Max + Form3.DigerDegerlerTutucu[5]);
            command.Parameters.AddWithValue("@Liquid_Pressure_Max", PlcToPc.Liquid_Pressure_Max + Form3.DigerDegerlerTutucu[6]);
            command.Parameters.AddWithValue("@Date", DateTime.Now);
            command.Parameters.AddWithValue("@Model", Form1.model);
            Connect();
            command.ExecuteNonQuery();
            Disconnect();
        }
        ///Model Ekleme
        public void ModelAdd()
        {
            for (int i = 0; i < ModelDeger.Count; i++)
            {
                if (ModelDeger[i].ToString().TrimEnd() != Form1.model)
                    sayac++;
            }
            string Modelsql = "IF '" + sayac + "'='" + ModelDeger.Count + "' BEGIN Insert Into MODEL (Model,Date) Values (@Model,@Date) END";
            command = new SqlCommand(Modelsql, con);
            command.Parameters.AddWithValue("@Model", Form1.model);
            command.Parameters.AddWithValue("@Date", DateTime.Now);
            Connect();
            command.ExecuteNonQuery();
            Disconnect();
            sayac = 0;
        }
        ///Diğer Değerler Grafiği Okuma
        public void Read()
        {
            string OtherValuesRead = "(Select RPM_Send,RPM_MAX_Result,SUM_OF_TOTAL_POWER,FlowMeter,Liquid_Pressure,FlowMeter_Max,Liquid_Pressure_Max,Direction,Model From OtherValues)";
            command = new SqlCommand(OtherValuesRead, con);
            Connect();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                RpmSend.Add(reader["RPM_Send"]);
                RpmMax.Add(reader["RPM_MAX_Result"]);
                SumOfTotal.Add(reader["SUM_OF_TOTAL_POWER"]);
                FlowMeter.Add(reader["FlowMeter"]);
                LiquidPressure.Add(reader["Liquid_Pressure"]);
                FlowMeterMax.Add(reader["FlowMeter_Max"]);
                LiquidPressureMax.Add(reader["Liquid_Pressure_Max"]);
                Direction.Add(reader["Direction"]);
                Model.Add(reader["Model"]);
            }
            Disconnect();
        }
        ///Sıcaklık Grafiği İçin Veri Okuma
        public void Read2()
        {
            string OtherValuesReadId = "(Select termo_send1,termo_send2,termo_send3,termo_send4,termo_send5,termo_send6,termo_send7,termo_send8,termo_send9,termo_send10,termo_send11,Model From Sicaklik)";
            command = new SqlCommand(OtherValuesReadId, con);
            Connect();
            readerS = command.ExecuteReader();
            while (readerS.Read())
            {
                sicaklik1.Add(readerS["termo_send1"]);
                sicaklik2.Add(readerS["termo_send2"]);
                sicaklik3.Add(readerS["termo_send3"]);
                sicaklik4.Add(readerS["termo_send4"]);
                sicaklik5.Add(readerS["termo_send5"]);
                sicaklik6.Add(readerS["termo_send6"]);
                sicaklik7.Add(readerS["termo_send7"]);
                sicaklik8.Add(readerS["termo_send8"]);
                sicaklik9.Add(readerS["termo_send9"]);
                sicaklik10.Add(readerS["termo_send10"]);
                sicaklik11.Add(readerS["termo_send11"]);
                Model2.Add(readerS["Model"]);
            }
            Disconnect();
        }
        ///Enerji Grafiği İçin Veri Okuma
        public void Read3()
        {
            string OtherValuesRead = "(Select energy_send1,energy_send2,energy_send3,energy_send4,energy_send6,Model From Energy)";
            command = new SqlCommand(OtherValuesRead, con);
            Connect();
            readerE = command.ExecuteReader();
            while (readerE.Read())
            {
                energy1.Add(readerE["energy_send1"]);
                energy2.Add(readerE["energy_send2"]);
                energy3.Add(readerE["energy_send3"]);
                energy4.Add(readerE["energy_send4"]);
                energy6.Add(readerE["energy_send6"]);
                Model3.Add(readerE["Model"]);
            }
            Disconnect();
        }
        public void ModelRead()
        {
            string ModelRead = "(Select Model From MODEL)";
            command = new SqlCommand(ModelRead, con);
            Connect();
            ModelReader = command.ExecuteReader();
            while (ModelReader.Read())
            {
                ModelDeger.Add(ModelReader["Model"]);
            }
            Disconnect();
        }
        public void TarihRead()
        {
            string TarihRead = "(Select Date From OtherValues)";
            command = new SqlCommand(TarihRead, con);
            Connect();
            TarihReader = command.ExecuteReader();
            while (TarihReader.Read())
            {
                TarihArry.Add(TarihReader["Date"]);
            }
            Disconnect();
        }
        ///Diger Değerler Grafiği Filitreleme İçin Okuma
        public void OzelRead()
        {
            //string OzelVeriRead = "(Select RPM_Send,RPM_MAX_Result,SUM_OF_TOTAL_POWER,FlowMeter,Liquid_Pressure,FlowMeter_Max,Liquid_Pressure_Max,Date From OtherValues Where '2022-02-09 00:00:00'>= Date AND  Date>='2022-02-07 00:00:00' OR '2022-02-09 00:00:00'<= Date AND  Date>='2022-02-07 00:00:00'  AND Model='"+Form2.urunModelComboBox+"')";
            string OzelVeriRead = "(Select RPM_Send,RPM_MAX_Result,SUM_OF_TOTAL_POWER,FlowMeter,Liquid_Pressure,FlowMeter_Max,Liquid_Pressure_Max,Direction,Date,Model From OtherValues Where Date BETWEEN '" + Form2.MinDate.ToString("yyyy/MM/dd HH:mm:ss") + "' AND '" + Form2.MaxDate.ToString("yyyy/MM/dd HH:mm:ss") + "')";
            //string OzelVeriRead = "(Select RPM_Send,RPM_MAX_Result,SUM_OF_TOTAL_POWER,FlowMeter,Liquid_Pressure,FlowMeter_Max,Liquid_Pressure_Max,Date From OtherValues Where Date<='" + Form2.MaxDate + "' AND Date>='" + Form2.MinDate + "'AND Model='" + Form2.urunModelComboBox + "')";
            command = new SqlCommand(OzelVeriRead, con);
            Connect();
            OzelReader = command.ExecuteReader();
            while (OzelReader.Read())
            {
                RpmSend2.Add(OzelReader["RPM_Send"]);
                RpmMax2.Add(OzelReader["RPM_MAX_Result"]);
                SumOfTotal2.Add(OzelReader["SUM_OF_TOTAL_POWER"]);
                FlowMeter2.Add(OzelReader["FlowMeter"]);
                LiquidPressure2.Add(OzelReader["Liquid_Pressure"]);
                FlowMeterMax2.Add(OzelReader["FlowMeter_Max"]);
                LiquidPressureMax2.Add(OzelReader["Liquid_Pressure_Max"]);
                Direction2.Add(OzelReader["Direction"]);
                TarihArryOtherValues.Add(OzelReader["Date"]);
                Model_2.Add(OzelReader["Model"]);
            }
            Disconnect();
        }
        ///Sıcaklık Grafiği Filitreleme İçin Okuma
        public void OzelRead2()
        {
            string OzelVeriRead = "(Select termo_send1,termo_send2,termo_send3,termo_send4,termo_send5,termo_send6,termo_send7,termo_send8,termo_send9,termo_send10,termo_send11,Date,Model From Sicaklik Where Date BETWEEN '" + Form2.MinDate.ToString("yyyy/MM/dd HH:mm:ss") + "' AND '" + Form2.MaxDate.ToString("yyyy/MM/dd HH:mm:ss") + "')";
            command = new SqlCommand(OzelVeriRead, con);
            Connect();
            OzelReader = command.ExecuteReader();
            while (OzelReader.Read())
            {
                sicaklik1_2.Add(OzelReader["termo_send1"]);
                sicaklik2_2.Add(OzelReader["termo_send2"]);
                sicaklik3_2.Add(OzelReader["termo_send3"]);
                sicaklik4_2.Add(OzelReader["termo_send4"]);
                sicaklik5_2.Add(OzelReader["termo_send5"]);
                sicaklik6_2.Add(OzelReader["termo_send6"]);
                sicaklik7_2.Add(OzelReader["termo_send7"]);
                sicaklik8_2.Add(OzelReader["termo_send8"]);
                sicaklik9_2.Add(OzelReader["termo_send9"]);
                sicaklik10_2.Add(OzelReader["termo_send10"]);
                sicaklik11_2.Add(OzelReader["termo_send11"]);
                TarihArryOtherValues.Add(OzelReader["Date"]);
                Model2_2.Add(OzelReader["Model"]);
            }
            Disconnect();
        }
        ///Enerji Grafiği Filitreleme İçin Okuma
        public void OzelRead3()
        {
            string OzelVeriRead = "(Select energy_send1,energy_send2,energy_send3,energy_send4,energy_send5,energy_send6,Date,Model From Energy Where Date BETWEEN '" + Form2.MinDate.ToString("yyyy/MM/dd HH:mm:ss") + "' AND '" + Form2.MaxDate.ToString("yyyy/MM/dd HH:mm:ss") + "')";
            command = new SqlCommand(OzelVeriRead, con);
            Connect();
            OzelReader = command.ExecuteReader();
            while (OzelReader.Read())
            {
                energy1_2.Add(OzelReader["energy_send1"]);
                energy2_2.Add(OzelReader["energy_send2"]);
                energy3_2.Add(OzelReader["energy_send3"]);
                energy4_2.Add(OzelReader["energy_send4"]);
                energy5_2.Add(OzelReader["energy_send5"]);
                energy6_2.Add(OzelReader["energy_send6"]);
                TarihArryOtherValues.Add(OzelReader["Date"]);
                Model3_2.Add(OzelReader["Model"]);
            }
            Disconnect();
        }
        public void Temizle()
        {
            RpmSend2.Clear();
            RpmMax2.Clear();
            SumOfTotal2.Clear();
            FlowMeter2.Clear();
            LiquidPressure2.Clear();
            FlowMeterMax2.Clear();
            LiquidPressureMax2.Clear();
            Direction2.Clear();
            sicaklik1_2.Clear();
            sicaklik2_2.Clear();
            sicaklik3_2.Clear();
            sicaklik4_2.Clear();
            sicaklik5_2.Clear();
            sicaklik6_2.Clear();
            sicaklik7_2.Clear();
            sicaklik8_2.Clear();
            sicaklik9_2.Clear();
            sicaklik10_2.Clear();
            sicaklik11_2.Clear();
            energy1_2.Clear();
            energy2_2.Clear();
            energy3_2.Clear();
            energy4_2.Clear();
            energy5_2.Clear();
            energy6_2.Clear();
            TarihArryOtherValues.Clear();
        }
        public void sqlTemizle()
        {
            string sqlTemizle = "Delete From Sicaklik";
            command = new SqlCommand(sqlTemizle, con);
            Connect();
            command.ExecuteNonQuery();
            Disconnect();
        }
        public void sqlTemizle2()
        {
            string sqlTemizle2 = "Delete From Energy";
            command = new SqlCommand(sqlTemizle2, con);
            Connect();
            command.ExecuteNonQuery();
            Disconnect();
        }
        public void sqlTemizle3()
        {
            string sqlTemizle3 = "Delete From OtherValues";
            command = new SqlCommand(sqlTemizle3, con);
            Connect();
            command.ExecuteNonQuery();
            Disconnect();
        }
        public void sqlTemizle4()
        {
            string sqlTemizle4 = "Delete From SensorOne";
            command = new SqlCommand(sqlTemizle4, con);
            Connect();
            command.ExecuteNonQuery();
            Disconnect();

        }
        #region TestSuresi
        //public void TestSuresi()
        //{
        //    string Test_Suresi = "Insert Into ModelTestSuresi (Model,Test_Baslangic,Test_Bitis,Toplam_Test_Suresi) Values (@Model,@Test_Baslangic,@Test_Bitis,@Toplam_Test_Suresi)";
        //    command = new SqlCommand(Test_Suresi, con);
        //    command.Parameters.AddWithValue("@Model", Form1.model);
        //    command.Parameters.AddWithValue("@Test_Baslangic", Form3.baslangic_zamani);
        //    command.Parameters.AddWithValue("@Test_Bitis", Form3.bitis_zamani);
        //    command.Parameters.AddWithValue("@Toplam_Test_Suresi", Convert.ToInt32(Form3.bitis_zamani.ToShortTimeString()) - Convert.ToInt32(Form3.baslangic_zamani.ToShortTimeString()));
        //    Connect();
        //    command.ExecuteNonQuery();
        //    Disconnect();
        //}
        #endregion

        public void ReadWebSensor() ///WebSensorRead
        {
            string SensorRead = "(Select Temperature,Humidity,Pressure,Date From SensorOne)";
            command = new SqlCommand(SensorRead, con);
            Connect();
            SensorReader = command.ExecuteReader();
            while (SensorReader.Read())
            {                
                Temperature.Add(SensorReader["Temperature"]);
                Humidity.Add(SensorReader["Humidity"]);
                Pressure.Add(SensorReader["Pressure"]);
                SensorTarih.Add(SensorReader["Date"]);
                temp = SensorReader["Temperature"].ToString();
                humid = SensorReader["Humidity"].ToString();
                pressure = SensorReader["Pressure"].ToString();
            }
            Disconnect();
        }

    }
}
