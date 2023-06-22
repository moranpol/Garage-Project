using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal enum eColorType
    {
        White,
        Black,
        Red,
        Yellow,
    }

    internal enum eNumOfDoors
    {
        Two,
        Three,
        Four,
        Five,
    }

    internal class Car : Vehicle
    {
        private const int k_NumTyres = 5;
        private const float k_MaxAirPressure = 33f;
        private const eFuelTypes k_FuelType = eFuelTypes.Octan95;
        private const float k_MaxLiterFuelTank = 46f;
        private const float k_MaxBatteryTime = 5.2f;
        private eColorType m_ColorType;
        private eNumOfDoors m_NumOfDoors;
        
        internal eColorType ColorType
        {
            get { return m_ColorType; }
            set { m_ColorType = value; }
        }

        internal eNumOfDoors numOfDoors
        { 
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value;}
        
        }

        public override string ToString()
        {
            string v_Str = base.ToString();
            v_Str += String.Format("Color: {0}\nDoors number: {1}", m_ColorType.ToString(), m_NumOfDoors.ToString());
            return v_Str;
        }

        internal override List<string> CreateListOfRequirementsInfoForNewVehicle()
        {
            List<string> v_Info;
            v_Info = base.CreateListOfRequirementsInfoForNewVehicle();
            v_Info.Add("car color (White, Black, Red, Yellow)"); 
            v_Info.Add("number of doors (Two, Three, Four, Five)");
            v_Info.Add("engine type (Electric, Fuel)");
            v_Info.Add("for electric engine - current battery time in hours, for fuel engine - current fuel amount in liters");
            return v_Info;
        }

        /// Lexicon index list of requirements:
        /// 0 - license number
        /// 1 - model
        /// 2 - current tyre air
        /// 3 - tyre producer name
        /// 4 - car color
        /// 5 - door numbers
        /// 6 - engine type
        /// 7 - current energy amount
        internal override void InsertVehicleInfo(List<string> i_VehicleInfo)
        {
            float v_CurrentEnergyAmount;
            base.InsertVehicleInfo(i_VehicleInfo);
            CreateTyres(k_NumTyres, k_MaxAirPressure, i_VehicleInfo);
            checkAndUpdateCarColor(i_VehicleInfo[4]);
            checkAndUpdateCarNumDoors(i_VehicleInfo[5]);

            if(!float.TryParse(i_VehicleInfo[7], out v_CurrentEnergyAmount))
            {
                throw new FormatException("current energy amount must be a number\n");
            }

            if(i_VehicleInfo[6] == "Electric")
            {
                base.Engine = new ElectricEngine(v_CurrentEnergyAmount, k_MaxBatteryTime);
            }
            else if(i_VehicleInfo[6] == "Fuel")
            {
                base.Engine = new FuelEngine(v_CurrentEnergyAmount, k_MaxLiterFuelTank, k_FuelType);
            }
            else
            { 
                throw new FormatException("engine type must be: Electric or Fuel\n"); 
            }

            UpdatePrecentEnrgyLeft(base.Engine.CurrentEnergyAmount, base.Engine.MaxEnergyAmount);
        }

        private void checkAndUpdateCarColor(string i_Color)
        {
            if (!Enum.TryParse<eColorType>(i_Color, out m_ColorType))
            {
                throw new FormatException("car color must be: White, Black, Red or Yellow\n");
            }
        }

        private void checkAndUpdateCarNumDoors(string i_NumDoors)
        {
            if (!Enum.TryParse<eNumOfDoors>(i_NumDoors, out m_NumOfDoors))
            {
                throw new FormatException("number of doors must be: Two, Three, Four or Five\n");
            }
        }
    }
}
