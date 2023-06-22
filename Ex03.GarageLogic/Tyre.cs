using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal struct Tyre
    {
        private string m_ProducerName;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        internal string ProducerName
        {
            get { return m_ProducerName; }
            set { m_ProducerName = value; }
        }

        internal float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        internal float MaxAirPressure
        {
            get { return m_MaxAirPressure; }
            set { m_MaxAirPressure = value; }
        }

        public override string ToString()
        {
            string v_Str = String.Format("Producer name: {0}\nCurrent air pressure: {1}", m_ProducerName, m_CurrentAirPressure);
            return v_Str;
        }

        internal Tyre Pump(float i_AirToAdd)
        {
            if(i_AirToAdd + m_CurrentAirPressure > m_MaxAirPressure || i_AirToAdd < 0)
            {
                throw new ValueOutOfRangeException("air amount to add range: ", (m_MaxAirPressure - m_CurrentAirPressure), 0);
            }
  
            m_CurrentAirPressure += i_AirToAdd;
            return this;
        }

        internal static Tyre CreateTyre(string i_ProducerName, float i_MaxAirPressure, string i_CurrentAirPressure)
        {
            Tyre v_NewTyre = new Tyre();
            v_NewTyre.m_ProducerName = i_ProducerName;
            v_NewTyre.m_MaxAirPressure = i_MaxAirPressure;

            if(!float.TryParse(i_CurrentAirPressure, out v_NewTyre.m_CurrentAirPressure))
            {
                throw new FormatException("air amount must be a number\n");
            }

            if (v_NewTyre.m_CurrentAirPressure > v_NewTyre.m_MaxAirPressure || v_NewTyre.m_CurrentAirPressure < 0)
            {
                throw new ValueOutOfRangeException("air amount range: ", v_NewTyre.m_MaxAirPressure, 0);
            }

            return v_NewTyre;
        }      
    }
}
