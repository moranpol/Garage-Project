using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;


namespace Ex03.ConsoleUI
{
    internal class UIManager
    {
        private Garage m_Garage = new Garage();

        internal void AddNewVehicle()
        {
            List<string> v_Answers;
            string v_LicenseNumber = GetLicenseNumberFromUser();

            if (m_Garage.IsVehicleExists(v_LicenseNumber))
            {
                m_Garage.ChangeVehicleConditionToUnderRepair(v_LicenseNumber);
            }
            else
            {
                v_Answers = getInfoFromCustomer(v_LicenseNumber, m_Garage.GetListOfRequirementsInfoForNewCustomer());
                try
                {
                    m_Garage.InsertNewCustomer(v_Answers);
                    v_Answers = getInfoFromCustomer(v_LicenseNumber, m_Garage.GetListOfRequirementsInfoForNewVehicle(v_LicenseNumber));
                    m_Garage.InsertVehicleInfo(v_Answers);
                }
                catch (Exception v_Ex)
                {
                    m_Garage.Vehicles.Remove(v_LicenseNumber);
                    Console.WriteLine(v_Ex.Message);
                }
            }
        }

        private string GetLicenseNumberFromUser()
        {
            string v_LicenseNumber;
            Console.WriteLine("Please enter your vehicle license number:");
            v_LicenseNumber = getInputFromUser();
            return v_LicenseNumber;
        }

        private List<string> getInfoFromCustomer(string i_LicenseNumber, List<string> i_ListOfRequirements)
        {
            List<string> v_AnswersList = new List<string>();
            string v_Input;
            v_AnswersList.Add(i_LicenseNumber);

            foreach (string v_Str in i_ListOfRequirements)
            {
                Console.WriteLine("Please enter " + v_Str + ":");
                v_Input = getInputFromUser();
                v_AnswersList.Add(v_Input);
            }

            return v_AnswersList;
        }

        internal void ShowLicenseNumbersWithFilter()
        {
            List<string> v_LicenseNumbersCondition = m_Garage.GetListOfOptionalConditions();
            string v_InputCondition;
            List<string> v_ListFilterdLicenseNumbers;

            Console.WriteLine("Please insert filter condition:\nAll");
            printStringList(v_LicenseNumbersCondition);
            v_InputCondition = getInputFromUser();

            try
            {
                v_ListFilterdLicenseNumbers = m_Garage.GetListOfLicenseNumbers(v_InputCondition);

                if(v_ListFilterdLicenseNumbers.Count==0)
                {
                    Console.WriteLine("No license Numbers filtered by: " + v_InputCondition + "\n");
                }
                else
                {
                    Console.WriteLine("License Numbers filtered by: " + v_InputCondition);
                    printStringList(v_ListFilterdLicenseNumbers);
                    Console.WriteLine();
                }
            }
            catch (Exception v_Ex)
            {
                Console.WriteLine(v_Ex.Message);
            }
        }

        private string getInputFromUser()
        {
            string v_Input;
            Console.Write("-->");
            v_Input = Console.ReadLine();

            while (v_Input == "")
            {
                Console.WriteLine("Empty input please try again:");
                Console.Write("-->");
                v_Input = Console.ReadLine();
            }

            Console.WriteLine();
            return v_Input;
        }

        private void printStringList(List<string> i_StringList)
        {
            foreach (string v_Str in i_StringList)
            {
                Console.WriteLine(v_Str);
            }
        }

        internal void ChangeVehicleCondition()
        {
            string v_InputCondition;
            string v_LicenseNumber = GetLicenseNumberFromUser();
            List<string> v_LicenseNumbersCondition = m_Garage.GetListOfOptionalConditions();
            Console.WriteLine("Please enter new condition:");
            printStringList(v_LicenseNumbersCondition);
            v_InputCondition = getInputFromUser();

            try
            {
                m_Garage.ChangeVehicleCondition(v_LicenseNumber, v_InputCondition);
            }
            catch (Exception v_Ex)
            {
                Console.WriteLine(v_Ex.Message);
            }
        }

        internal void FillMaxTyreAir()
        {
            string v_LicenseNumber = GetLicenseNumberFromUser();

            try
            {
                m_Garage.FillMaxTyreAir(v_LicenseNumber);
            }
            catch (Exception v_Ex)
            {
                Console.WriteLine(v_Ex.Message);
            }
        }

        internal void RefuelVehicle()
        {
            string v_LicenseNumber = GetLicenseNumberFromUser();
            string v_FuelAmount, v_FuelType;
            Console.WriteLine("Please enter fuel type:");
            printStringList(m_Garage.GetListOfFuelTypes());
            v_FuelType = getInputFromUser();
            Console.WriteLine("Please enter fuel amount in liter:");
            v_FuelAmount = getInputFromUser();

            try
            {
                m_Garage.RefuelVehicle(v_LicenseNumber, v_FuelAmount, v_FuelType);
            }
            catch (Exception v_Ex)
            {
                Console.WriteLine(v_Ex.Message);
            }
        }

        internal void ChargeVehicle()
        {
            string v_LicenseNumber = GetLicenseNumberFromUser();
            string v_ChargeMinute;
            Console.WriteLine("Please enter charge in minute:");
            v_ChargeMinute = getInputFromUser();

            try
            {
                m_Garage.ChargeVehicle(v_LicenseNumber, v_ChargeMinute);
            }
            catch (Exception v_Ex)
            {
                Console.WriteLine(v_Ex.Message);
            }
        }

        internal void PrintCustomerData()
        {
            string v_LicenseNumber = GetLicenseNumberFromUser();

            try
            {
                Console.WriteLine(m_Garage.GetCustomerData(v_LicenseNumber));
            }
            catch (Exception v_Ex)
            {
                Console.WriteLine(v_Ex.Message);
            }
        }

        internal void Menu()
        {
            string v_choice = "0";

            while(v_choice != "8")
            {
                printMenu();
                v_choice = getInputFromUser();

                switch(v_choice)
                {
                    case "1":
                        AddNewVehicle();
                        break;
                    case "2":
                        ShowLicenseNumbersWithFilter();
                        break;
                    case "3":
                        ChangeVehicleCondition();
                        break;
                    case "4":
                        FillMaxTyreAir();
                        break;
                    case "5":
                        RefuelVehicle();
                        break;
                    case "6":
                        ChargeVehicle();
                        break;
                    case "7":
                        PrintCustomerData();
                        break;
                    case "8":
                        break;
                    default:
                        Console.WriteLine("Invalid input, try again\n");
                        break;
                }
            }
        }

        private void printMenu()
        {
            Console.WriteLine("Please select an action:");
            Console.WriteLine("1 - Insert new vehicle");
            Console.WriteLine("2 - Show filterd license numbers");
            Console.WriteLine("3 - Change vehicle condition");
            Console.WriteLine("4 - Inflate tyres to the maximum");
            Console.WriteLine("5 - Refuel a vehicle that is powered by fuel");
            Console.WriteLine("6 - Charge an electric vehicle");
            Console.WriteLine("7 - Show vehicle data by license number");
            Console.WriteLine("8 - Quit");
        }
    }
}
