using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const int k_NumTyres = 14;
        private const float k_MaxAirPressure = 26f;
        private const eFuelTypes k_FuelType = eFuelTypes.Soler;
        private const float k_MaxLiterFuelTank = 135f;
        private bool m_IsCarringDangerMaterial;
        private float m_CargoVolume;

        internal bool IsCarringDangerMaterial
        {
            get { return m_IsCarringDangerMaterial; }
            set { m_IsCarringDangerMaterial = value; }
        }

        internal float CargoVolume
        {
            get { return m_CargoVolume; }
            set { m_CargoVolume = value; }
        }

        public override string ToString()
        {
            string v_Str = base.ToString();
            v_Str += String.Format("Is carring danger material: {0}\nCargo volume: {1}", IsCarringDangerMaterial.ToString(), m_CargoVolume);
            return v_Str;
        }

        internal override List<string> CreateListOfRequirementsInfoForNewVehicle()
        {
            List<string> v_Info;
            v_Info = base.CreateListOfRequirementsInfoForNewVehicle();
            v_Info.Add("is carring danger material (yes, no)");
            v_Info.Add("cargo volume");
            v_Info.Add("current fuel amount in liters");
            return v_Info;
        }

        /// Lexicon index list of requirements:
        /// 0 - license number
        /// 1 - model
        /// 2 - current tyre air
        /// 3 - tyre producer name
        /// 4 - is carring danger material (yes, no)
        /// 5 - cargo volume
        /// 6 - current energy amount
        internal override void InsertVehicleInfo(List<string> i_VehicleInfo)
        {
            float v_CurrentEnergyAmount;
            base.InsertVehicleInfo(i_VehicleInfo);
            CreateTyres(k_NumTyres, k_MaxAirPressure, i_VehicleInfo);
            checkAndUpdateIsCarringDangerMaterial(i_VehicleInfo[4]);
            checkAndUpdateCargoVolume(i_VehicleInfo[5]);

            if (!float.TryParse(i_VehicleInfo[6], out v_CurrentEnergyAmount))
            {
                throw new FormatException("energy amount capacity must be a number\n");
            }

            base.Engine = new FuelEngine(v_CurrentEnergyAmount, k_MaxLiterFuelTank, k_FuelType);
            UpdatePrecentEnrgyLeft(base.Engine.CurrentEnergyAmount, base.Engine.MaxEnergyAmount);
        }
               
        internal void checkAndUpdateIsCarringDangerMaterial(string i_IsCarringDangerMaterial)
        {
            if (i_IsCarringDangerMaterial == "yes")
            {
                m_IsCarringDangerMaterial = true;
            }
            else if(i_IsCarringDangerMaterial == "no")
            {
                m_IsCarringDangerMaterial = false;
            }
            else
            {
                throw new FormatException("is carring danger material must be: yes or no\n");
            }
        }

        internal void checkAndUpdateCargoVolume(string i_CargoVolume)
        {
            if (!float.TryParse(i_CargoVolume, out m_CargoVolume))
            {
                throw new FormatException("cargo volume must be a number\n");
            }

            if (m_CargoVolume < 0)
            {
                throw new ValueOutOfRangeException("cargo volume range: ", float.PositiveInfinity, 0);
            }
        }
    }
}
