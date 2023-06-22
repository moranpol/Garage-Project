using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        private string m_Model;
        private string m_LicenseNumber;
        private float m_PrecentEnrgyLeft;
        private List<Tyre> m_Tyres = new List<Tyre>();
        private Engine m_Engine;

        internal string Modele
        {
            get { return m_Model; }
            set { m_Model = value; }
        }
        internal string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }
        internal float PrecentEnrgyLeft
        {
            get { return m_PrecentEnrgyLeft; }
            set { m_PrecentEnrgyLeft = value;}
        }
        internal List<Tyre> Tyres
        {
            get { return m_Tyres; }
        }
        internal Engine Engine
        {
            get { return m_Engine; }
            set { m_Engine = value; }
        }

        internal virtual List<string> CreateListOfRequirementsInfoForNewVehicle()
        {
            List<string> v_Info = new List<string>()
            {"model", "the current amount of air in the wheels" ,"the tyre producer name"};
            return v_Info;
        }

        internal virtual void InsertVehicleInfo(List<string> i_VehicleInfo)
        {
            m_LicenseNumber = i_VehicleInfo[0];
            m_Model = i_VehicleInfo[1];
        }

        internal void CreateTyres(int i_NumTyres, float i_MaxAirPressure, List<string> i_VehicleInfo)
        {
            for (int i = 0; i < i_NumTyres; i++)
            {
                Tyres.Add(Tyre.CreateTyre(i_VehicleInfo[3], i_MaxAirPressure, i_VehicleInfo[2]));
            }
        }

        internal void UpdatePrecentEnrgyLeft(float i_CurrentEnergyAmount, float i_MaxEnergyAmount)
        {
            m_PrecentEnrgyLeft = i_CurrentEnergyAmount / i_MaxEnergyAmount * 100;
        }

        internal void FillEnergy(float i_EnergyAmountToAdd, Dictionary<string, string> i_AdditionalParams)
        {
            m_Engine.FillEnergy(i_EnergyAmountToAdd, i_AdditionalParams);
            UpdatePrecentEnrgyLeft(m_Engine.CurrentEnergyAmount, m_Engine.MaxEnergyAmount);
        }

        internal void FillMaxTyreAir()
        {
            for(int i = 0; i < m_Tyres.Count; i++)
            {
                m_Tyres[i] = m_Tyres[i].Pump(m_Tyres[i].MaxAirPressure - m_Tyres[i].CurrentAirPressure);
            }
        }

        public override string ToString()
        {
            return String.Format("License number: {0}\nModel: {1}\nPrecent energy left: {2}\n{3} Tyres info:\n{4}\nEngine info - {5}:\n{6}\n", 
                m_LicenseNumber, m_Model, m_PrecentEnrgyLeft, m_Tyres.Count, m_Tyres[0].ToString(), m_Engine.GetType().Name, m_Engine.ToString());
        }

    }
}
