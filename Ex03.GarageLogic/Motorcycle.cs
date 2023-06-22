using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal enum eLicenseTypes
    {
        A1,
        A2,
        AA,
        B1,
    }

    internal class Motorcycle : Vehicle
    {
        private const int k_NumTyres = 2;
        private const float k_MaxAirPressure = 31f;
        private const eFuelTypes k_FuelType = eFuelTypes.Octan98;
        private const float k_MaxLiterFuelTank = 6.4f;
        private const float k_MaxBatteryTime = 2.6f;
        private eLicenseTypes m_LicenseType;
        private int m_EngineCapacity;

        internal eLicenseTypes LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        internal int EngineCapacity
        {
            get { return m_EngineCapacity; }
            set { m_EngineCapacity = value; }
        }

        public override string ToString()
        {
            string v_Str = base.ToString();
            v_Str += String.Format("License type: {0}\nEngine capacity: {1}", m_LicenseType.ToString(), m_EngineCapacity);
            return v_Str;
        }

        internal override List<string> CreateListOfRequirementsInfoForNewVehicle()
        {
            List<string> v_Info;
            v_Info = base.CreateListOfRequirementsInfoForNewVehicle();
            v_Info.Add("license type (A1, A2, AA, B1)");
            v_Info.Add("engine capacity");
            v_Info.Add("engine type (Electric, Fuel)");
            v_Info.Add("for electric engine - current battery time in hours, for fuel engine - current fuel amount in liters");

            return v_Info;
        }

        /// Lexicon index list of requirements:
        /// 0 - license number
        /// 1 - model
        /// 2 - current tyre air
        /// 3 - tyre producer name
        /// 4 - license type
        /// 5 - engine capacity
        /// 6 - engine type
        /// 7 - current energy amount
        internal override void InsertVehicleInfo(List<string> i_VehicleInfo)
        {
            float v_CurrentEnergyAmount;
            base.InsertVehicleInfo(i_VehicleInfo);
            CreateTyres(k_NumTyres, k_MaxAirPressure, i_VehicleInfo);
            checkAndUpdateLicenseType(i_VehicleInfo[4]);
            checkAndUpdateEngineCapacity(i_VehicleInfo[5]);

            if (!float.TryParse(i_VehicleInfo[7], out v_CurrentEnergyAmount))
            {
                throw new FormatException("current energy amount must be a number\n");
            }

            if (i_VehicleInfo[6] == "Electric")
            {
                base.Engine = new ElectricEngine(v_CurrentEnergyAmount, k_MaxBatteryTime);
            }
            else if (i_VehicleInfo[6] == "Fuel")
            {
                base.Engine = new FuelEngine(v_CurrentEnergyAmount, k_MaxLiterFuelTank, k_FuelType);
            }
            else
            {
                throw new FormatException("engine type must be: Electric or Fuel\n");
            }

            UpdatePrecentEnrgyLeft(base.Engine.CurrentEnergyAmount, base.Engine.MaxEnergyAmount);
        }
 
        private void checkAndUpdateLicenseType(string i_IcenseType)
        {
            if (!Enum.TryParse<eLicenseTypes>(i_IcenseType, out m_LicenseType))
            {
                throw new FormatException("license type must be: A1, A2, AA or B1\n");
            }
        }

        private void checkAndUpdateEngineCapacity(string i_EngineCapacity)
        {
            if (!int.TryParse(i_EngineCapacity, out m_EngineCapacity))
            {
                throw new FormatException("engine capacity must be a number\n");
            }

            if(m_EngineCapacity < 0)
            {
                throw new ValueOutOfRangeException("engine capacity range: ", float.PositiveInfinity, 0);
            }
        }
    }
}
