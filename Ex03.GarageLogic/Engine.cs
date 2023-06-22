using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal abstract class Engine
    {
        private float m_CurrentEnergyAmount;
        private float m_MaxEnergyAmount;

        public Engine(float i_CurrentEnergyAmount, float i_MaxEnergyAmount)
        {
            if (i_CurrentEnergyAmount > i_MaxEnergyAmount || i_CurrentEnergyAmount < 0)
            {
                throw new ValueOutOfRangeException("current energy amount range: ", i_MaxEnergyAmount , 0);
            }

            m_CurrentEnergyAmount = i_CurrentEnergyAmount;
            m_MaxEnergyAmount = i_MaxEnergyAmount;
        }

        internal float CurrentEnergyAmount
        {
            get { return m_CurrentEnergyAmount; }
            set { m_CurrentEnergyAmount = value; }
        }

        internal float MaxEnergyAmount
        {
            get { return m_MaxEnergyAmount; }
            set { m_MaxEnergyAmount = value; }
        }

        internal virtual void FillEnergy(float i_EnergyAmountToAdd, Dictionary<string, string> i_AdditionalParams)
        {
            CurrentEnergyAmount += i_EnergyAmountToAdd;
        }
    }
}
