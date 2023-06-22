using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, CustomerInfo> m_Vehicles = new Dictionary<string, CustomerInfo>();

        public Dictionary<string, CustomerInfo> Vehicles
        {
            get { return  m_Vehicles; }
        }

        public List<string> GetListOfRequirementsInfoForNewCustomer()
        {
            return CustomerInfo.CreateListOfRequirementsInfoForNewCustomer();
        }

        public void InsertNewCustomer(List<string> i_CustomerInfo)
        {
            Vehicle v_NewVehicle = Factory.CreateNewVehicle(i_CustomerInfo[1]);
            CustomerInfo v_CustomerInfo = new CustomerInfo();
            v_CustomerInfo.OwnerName = i_CustomerInfo[2];
            CheckValidPhone(i_CustomerInfo[3]);
            v_CustomerInfo.OwnerPhone = i_CustomerInfo[3];
            v_CustomerInfo.Vehicle = v_NewVehicle;
            m_Vehicles.Add(i_CustomerInfo[0], v_CustomerInfo);
        }

        private void CheckValidPhone(string i_Phone)
        {
            if(i_Phone.Count() != 10)
            {
                throw new ArgumentException("Phone number must have 10 digit\n");
            }

            foreach(char v_digit in i_Phone)
            {
                if (!char.IsDigit(v_digit))
                {
                    throw new FormatException("Phone number with numbers only\n");
                }
            }
        }

        public List<string> GetListOfRequirementsInfoForNewVehicle(string i_LicenseNumber)
        {
            return m_Vehicles[i_LicenseNumber].Vehicle.CreateListOfRequirementsInfoForNewVehicle();
        }

        public void InsertVehicleInfo(List<string> i_VehicleInfo)
        {
            m_Vehicles[i_VehicleInfo[0]].Vehicle.InsertVehicleInfo(i_VehicleInfo);
        }

        public bool IsVehicleExists(string i_LicenseNumber)
        {
            return m_Vehicles.ContainsKey(i_LicenseNumber);
        }

        public void ChangeVehicleConditionToUnderRepair(string i_LicenseNumber)
        {
            CustomerInfo v_VehicleInfoCopy = m_Vehicles[i_LicenseNumber];
            v_VehicleInfoCopy.Condition = eVehicleConditions.UnderRepair;
            m_Vehicles[i_LicenseNumber] = v_VehicleInfoCopy;
        }

        public void ChargeVehicle(string i_LicenseNumber, string i_ChargeMinute)
        {
            float v_ChargeMinute;
            Dictionary<string, string> i_AdditionalParams = new Dictionary<string, string>();

            if (!float.TryParse(i_ChargeMinute, out v_ChargeMinute))
            {
                throw new FormatException("energy amount volume must be a number\n");
            }

            if (!IsVehicleExists(i_LicenseNumber))
            {
                throw new ArgumentException("License number not found\n");
            }

            if (m_Vehicles[i_LicenseNumber].Vehicle.Engine.GetType() != typeof(ElectricEngine))
            {
                throw new ArgumentException("Vehicle not electric\n");
            }

            m_Vehicles[i_LicenseNumber].Vehicle.FillEnergy(v_ChargeMinute / 60, i_AdditionalParams);
        }

        public void RefuelVehicle(string i_LicenseNumber, string i_FuelAmount, string i_FuelType)
        {
            float v_FuelAmount;
            Dictionary<string, string> i_AdditionalParams = new Dictionary<string, string>();

            if (!float.TryParse(i_FuelAmount, out v_FuelAmount))
            {
                throw new FormatException("energy amount volume must be a number\n");
            }

            if (!IsVehicleExists(i_LicenseNumber))
            {
                throw new ArgumentException("License number not found\n");
            }

            if (m_Vehicles[i_LicenseNumber].Vehicle.Engine.GetType() != typeof(FuelEngine))
            {
                throw new ArgumentException("Vehicle not on fuel\n");
            }

            i_AdditionalParams.Add("fuel", i_FuelType);
            m_Vehicles[i_LicenseNumber].Vehicle.FillEnergy(v_FuelAmount, i_AdditionalParams);
        }


        public List<string> GetListOfOptionalConditions()
        {
            List<string> v_Conditions = new List<string>();

            foreach(string v_Condition in Enum.GetNames(typeof(eVehicleConditions)))
            {
                v_Conditions.Add(v_Condition);
            }

            return v_Conditions;
        }

        public List<string> GetListOfLicenseNumbers(string i_ConditionFilter)
        {
            List<string> v_LicenseNumbers = new List<string>();
            eVehicleConditions v_Condition;

            if(i_ConditionFilter == "All")
            {
                foreach(string v_LicenseNum in m_Vehicles.Keys)
                {
                    v_LicenseNumbers.Add(v_LicenseNum);
                }
            }
            else if(!Enum.TryParse<eVehicleConditions>(i_ConditionFilter, out v_Condition))
            {
                throw new FormatException("vehicle condition must be: UnderRepair, Fixed, Paid or All\n");
            }
            else
            {
                foreach (string v_LicenseNum in m_Vehicles.Keys)
                {
                    if (m_Vehicles[v_LicenseNum].Condition == v_Condition)
                    {
                        v_LicenseNumbers.Add(v_LicenseNum);
                    }
                }
            }

            return v_LicenseNumbers;
        }

        public void ChangeVehicleCondition(string i_LicenseNumber, string i_StrNewCondition)
        {
            eVehicleConditions v_NewCondition;
            CustomerInfo v_VehicleInfoCopy; 

            if (!IsVehicleExists(i_LicenseNumber))
            {
                throw new ArgumentException("License number not found\n");
            }

            if(!Enum.TryParse<eVehicleConditions>(i_StrNewCondition, out v_NewCondition))
            {
                throw new FormatException("vehicle condition must be: UnderRepair, Fixed or Paid\n");
            }

            v_VehicleInfoCopy = m_Vehicles[i_LicenseNumber];
            v_VehicleInfoCopy.Condition = v_NewCondition;
            m_Vehicles[i_LicenseNumber] = v_VehicleInfoCopy;
        }

        public void FillMaxTyreAir(string i_LicenseNumber)
        {
            if (!IsVehicleExists(i_LicenseNumber))
            {
                throw new ArgumentException("License number not found\n");
            }

            m_Vehicles[i_LicenseNumber].Vehicle.FillMaxTyreAir();
        }

        public List<string> GetListOfFuelTypes()
        {
            List<string> v_FuelTypes = new List<string>();

            foreach (string v_Fuel in Enum.GetNames(typeof(eFuelTypes)))
            {
                v_FuelTypes.Add(v_Fuel);
            }

            return v_FuelTypes;
        }

        public string GetCustomerData(string i_LicenseNumber)
        {
            if (!IsVehicleExists(i_LicenseNumber))
            {
                throw new ArgumentException("License number not found\n");
            }

            return m_Vehicles[i_LicenseNumber].ToString();
        }
    }
}
