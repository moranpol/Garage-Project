using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private string m_Message;
        private float m_MaxValue;
        private float m_MinValue;

        public ValueOutOfRangeException(string i_Message, float i_MaxValue, float i_MinValue)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
            m_Message = i_Message + i_MinValue + " - " + i_MaxValue + "\n";
        }

        public override string Message
        {
            get { return m_Message; }
        }
    }
}