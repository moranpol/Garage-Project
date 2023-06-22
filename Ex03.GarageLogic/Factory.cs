using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Factory
    {
        private static readonly List<Type> r_VehicleTypes = new List<Type>()
        {
            typeof(Motorcycle),
            typeof(Car),
            typeof(Truck)
        };

        internal static Vehicle CreateNewVehicle(string i_VehicleType)
        {
            Vehicle v_NewVehicle = null;

            foreach (Type v_Type in r_VehicleTypes) 
            {
                if(v_Type.Name.ToUpper() == i_VehicleType.ToUpper())
                {
                    v_NewVehicle = (Vehicle)Activator.CreateInstance(v_Type);
                    break;
                }
            }

            if(v_NewVehicle == null)
            {
                throw new FormatException("vehicle type not exist\n");
            }

            return v_NewVehicle;
        }
    }
}
