using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCdeneme
{
    public class PlcToPc
    {
        // thermo send
        //public float termo_send0 { get; set; }
        public float termo_send1 { get; set; }
        public float termo_send2 { get; set; }
        public float termo_send3 { get; set; }
        public float termo_send4 { get; set; }
        public float termo_send5 { get; set; }
        public float termo_send6 { get; set; }
        public float termo_send7 { get; set; }
        public float termo_send8 { get; set; }
        public float termo_send9 { get; set; }
        public float termo_send10 { get; set; }
        public float termo_send11 { get; set; }
        //public float termo_send12 { get; set; }
        
        // energy send
        //public float energy_send0 { get; set; }
        public float energy_send1 { get; set; }
        public float energy_send2 { get; set; }
        public float energy_send3 { get; set; }
        public float energy_send4 { get; set; }
        public float energy_send5 { get; set; }
        public float energy_send6 { get; set; }
        //public float energy_send7 { get; set; }
        //public float energy_send8 { get; set; }
        //public float energy_send9 { get; set; }
        //public float energy_send10 { get; set; }
        //public float energy_send11 { get; set; }
        //public float energy_send12 { get; set; }

        // other values
        public float RPM_Send { get; set; }
        public float RPM_Send2s { get; set; }
        public short Direction { get; set; }
        public float RPM_MAX_Result { get; set; }
        public float SUM_OF_TOTAL_POWER { get; set; }
        public double FlowMeter { get; set; }
        public double Liquid_Pressure { get; set; }
        public double FlowMeter_Max { get; set; }
        public double Liquid_Pressure_Max { get; set; }
    }
}
