using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal enum eVehicleConditions
    {
        UnderRepair,
        Fixed,
        Paid,
    }
    public struct CustomerInfo
    {
        private string m_OwnerName;
        private string m_OwnerPhone;
        private eVehicleConditions m_Condition;
        private Vehicle m_Vehicle;

        internal string OwnerName
        {
            get { return m_OwnerName;}
            set { m_OwnerName = value;}
        }

        internal string OwnerPhone
        {
            get { return m_OwnerPhone;}
            set { m_OwnerPhone = value; }
        }

        internal eVehicleConditions Condition
        {
            get { return m_Condition;}
            set { m_Condition = value; }
        }

        internal Vehicle Vehicle
        {
            get { return m_Vehicle;}
            set { m_Vehicle = value;}
        }

        public override string ToString()
        {
            string v_Str = String.Format("{0}'s {1}:\n" + "=====================\n" +
                "Owner name: {0}\nPhone number: {2}\nCondition: {3}\nVehicle details:\n{4}\n",
                m_OwnerName, m_Vehicle.GetType().Name, m_OwnerPhone, m_Condition.ToString(), m_Vehicle.ToString());
            return v_Str;
        }

        internal static List<string> CreateListOfRequirementsInfoForNewCustomer()
        {
            List<string> v_Info = new List<string>()
            {"vehicle type", "owner name", "owner phone - only numbers"};
            return v_Info; ;
        }
    }
}
