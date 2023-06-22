using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricEngine : Engine
    {
        public ElectricEngine(float i_CurrentEnergyAmount, float i_MaxEnergyAmount) : base(i_CurrentEnergyAmount, i_MaxEnergyAmount){}

        internal override void FillEnergy(float i_EnergyAmountToAdd, Dictionary<string, string> i_AdditionalParams)
        {
            if (CurrentEnergyAmount + i_EnergyAmountToAdd > MaxEnergyAmount || i_EnergyAmountToAdd < 0)
            {
                throw new ValueOutOfRangeException("battery amount to add in hours range: ", (MaxEnergyAmount - CurrentEnergyAmount), 0);
            }

            base.FillEnergy(i_EnergyAmountToAdd, i_AdditionalParams);
        }

        public override string ToString()
        {
            string v_Str = String.Format("Current battery amount in houres: {0}", base.CurrentEnergyAmount);
            return v_Str;
        }
    }
}
