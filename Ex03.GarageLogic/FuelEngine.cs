using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal enum eFuelTypes
    {
        Soler,
        Octan95,
        Octan96,
        Octan98,
    }

    internal class FuelEngine : Engine
    {
        private eFuelTypes m_FuelType;

        public FuelEngine(float i_CurrentEnergyAmount, float i_MaxEnergyAmount, eFuelTypes i_FuelType) : base(i_CurrentEnergyAmount, i_MaxEnergyAmount)
        {
            m_FuelType = i_FuelType;
        }

        internal eFuelTypes FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }

        public override string ToString()
        {
            string v_Str = String.Format("Current fuel amount: {0}\nFuel type: {1} ", base.CurrentEnergyAmount, FuelType.ToString());
            return v_Str;
        }

        internal override void FillEnergy(float i_EnergyAmountToAdd, Dictionary<string, string> i_AdditionalParams)
        {
            eFuelTypes v_FuelType;

            if (!Enum.TryParse<eFuelTypes>(i_AdditionalParams["fuel"], out v_FuelType))
            {
                throw new FormatException("fuel type not exist\n");
            }
            else if(v_FuelType != m_FuelType)
            {
                throw new ArgumentException("fuel type must be: " + m_FuelType.ToString() + "\n");
            }

            if (CurrentEnergyAmount + i_EnergyAmountToAdd > MaxEnergyAmount || i_EnergyAmountToAdd < 0)
            {
                throw new ValueOutOfRangeException("fuel amount to add in liters range:", (MaxEnergyAmount - CurrentEnergyAmount), 0);
            }

            base.FillEnergy(i_EnergyAmountToAdd, i_AdditionalParams);
        }
    }
}
