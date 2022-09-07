namespace Ex03
{
    public class ConsoleUI
    {
        public static void Start()
        {
            bool loopRunning = true;
            string userAnswer;
            GarageLogic garage = new GarageLogic();
            bool validAnswer;

            while (loopRunning)
            {
                validAnswer = true;
                Console.WriteLine("available actions: ");
                Console.WriteLine("(1) - Add a new vehicle");
                Console.WriteLine("(2) - List vehicles in the garage");
                Console.WriteLine("(3) - Change vehicle status");
                Console.WriteLine("(4) - Fill air in vehicle tires");
                Console.WriteLine("(5) - Fuel Vehicle");
                Console.WriteLine("(6) - Charge Vehicle");
                Console.WriteLine("(7) - Show vehicle details");
                Console.WriteLine("(0) - Exit");
                Console.WriteLine("");
                Console.Write("Which action would you like to perform?: ");
                userAnswer = Console.ReadLine();
                try
                {
                    Console.Clear();
                    switch (userAnswer)
                    {
                        case "1":
                            AddNewVehicle(garage);
                            break;
                        case "2":
                            ShowVehicleList(garage);
                            break;
                        case "3":
                            SetVehicleStatus(garage);
                            break;
                        case "4":
                            FillAirPreasure(garage);
                            break;
                        case "5":
                            FuelVehicle(garage);
                            break;
                        case "6":
                            ChargeVehicle(garage);
                            break;
                        case "7":
                            ShowVehicleSpecs(garage);
                            break;
                        case "0":
                            loopRunning = false;
                            break;
                        default:
                            Console.WriteLine("Please enter a valid answer!");
                            Console.WriteLine("");
                            validAnswer = false;
                            break;
                    }

                    if(validAnswer)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Done.");
                        Console.WriteLine("");
                    }  
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("Action failed. Reason: " + e.Message);
                    Console.WriteLine("");
                }
            }
        }

        public static void AddNewVehicle(GarageLogic i_Garage)
        {
            string licencePlate = getVehicleLicenceNumber();
            string vehicleType;
            IDictionary<string, string> newVehicleInfo = new Dictionary<string, string>();

            if (i_Garage.VehicleExists(licencePlate))
            {
                Console.WriteLine("");
                Console.WriteLine("Vehicle is already in the garage!");
                i_Garage.SetVehicleStatus(licencePlate, eVehicleState.BeingFixed);
                Console.WriteLine("Vehicle status set to Being Fixed.");
            }
            else
            {
                try
                {
                    newVehicleInfo.Add("licencePlate", licencePlate);
                    Console.Clear();
                    vehicleType = getVehicleType();
                    newVehicleInfo.Add("vehicleType", vehicleType);
                    List<string> fieldsToFill = GarageLogic.GetVehicleFields(vehicleType);
                 
                    foreach (string field in fieldsToFill)
                    {
                        Console.Clear();
                        string userAnswer = getUserAnswer(field);
                        newVehicleInfo.Add(field, userAnswer);
                    }

                    i_Garage.AddNewVehicle(newVehicleInfo);
                    Console.Clear();
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine("Vehicle adding unsuccessfull. Reason: " + e.Message);
                    Console.WriteLine("Try Again!");
                }
            }
        }

        public static void ShowVehicleList(GarageLogic i_Garage)
        {
            bool loopRunning = true;
            string userAnswer;
            eVehicleState filterState;
            List<string> vehiclesList;

            if (i_Garage.IsGarageEmpty())
            {
                Console.Clear();
                Console.WriteLine("Garage Is Empty");
                loopRunning =false;
            }

            while (loopRunning)
            {
                Console.Write("Would you like to filter the list by status? (Y/N): ");
                userAnswer = Console.ReadLine();
                if (userAnswer.Equals("Y") || userAnswer.Equals("y"))
                {
                    filterState = getVehicleStatus();
                    vehiclesList = i_Garage.ShowVehicleList(filterState);
                    Console.Clear();
                    Console.WriteLine("The list requested:");
                    foreach (string vehicle in vehiclesList)
                    {
                        Console.WriteLine(vehicle);
                    }
                    loopRunning = false;
                }
                else if (userAnswer.Equals("N") || userAnswer.Equals("n"))
                {
                    Console.Clear();
                    Console.WriteLine("Would you like to see the whole list? (Y/N): ");
                    userAnswer = Console.ReadLine();
                    if (userAnswer.Equals("Y") || userAnswer.Equals("y"))
                    {
                        vehiclesList = i_Garage.ShowAllVehiclesInList();

                        Console.Clear();
                        Console.WriteLine("The list requested:");
                        foreach (string vehicle in vehiclesList)
                        {
                            Console.WriteLine(vehicle);
                        }

                        loopRunning = false;
                    }
                    else if (userAnswer.Equals("N") || userAnswer.Equals("n"))
                    {
                        loopRunning = false;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid answer! (N/Y)");
                }
            }   
        }

        public static void SetVehicleStatus(GarageLogic i_Garage)
        {
            string licenseNumber = getVehicleLicenceNumber();

            Console.Clear();
            eVehicleState newStatus = getVehicleStatus();
            i_Garage.SetVehicleStatus(licenseNumber, newStatus);
        }

        public static void FillAirPreasure(GarageLogic i_Garage)
        {
            string licenseNumber = getVehicleLicenceNumber();
            i_Garage.CheckVehicleExists(licenseNumber);

            Console.Clear();
            i_Garage.FillAirPreasure(licenseNumber);
        }

        public static void FuelVehicle(GarageLogic i_Garage)
        {
            string licenseNumber = getVehicleLicenceNumber();
            i_Garage.CheckVehicleExists(licenseNumber);

            if (i_Garage.IsElectricVehicle(licenseNumber))
            {
                Console.Clear();
                Console.Write("");
                Console.WriteLine("Vehicle number: {0} is Electric",licenseNumber);
            }
            else
            {
                Console.Clear();
                eFuelType fuelType = getVehicleFuelType();
                Console.Clear();
                float fuelAmount = getAmount();
                i_Garage.FuelVehicle(licenseNumber, fuelType, fuelAmount);
            }  
        }

        public static void ChargeVehicle(GarageLogic i_Garage)
        {
            string licenseNumber = getVehicleLicenceNumber();
            i_Garage.CheckVehicleExists(licenseNumber);

            if (i_Garage.IsElectricVehicle(licenseNumber))
            {
                Console.Clear();
                float fuelAmount = getAmount();
                i_Garage.ChargeVehicle(licenseNumber, fuelAmount);
            }
            else
            {
                Console.Clear();
                Console.Write("");
                Console.WriteLine("Vehicle number: {0} is Not Electric", licenseNumber);
            }
        }

        public static void ShowVehicleSpecs(GarageLogic i_Garage)
        {
            string licenseNumber = getVehicleLicenceNumber();

            Console.Clear();
            Console.WriteLine(i_Garage.ShowVehicleSpecs(licenseNumber));
        }

        private static eVehicleState getVehicleStatus()
        {
            bool loopRunning = true;
            string userAnswer;
            eVehicleState filterState = eVehicleState.None;

            while (loopRunning)
            {
                Console.Clear();
                Console.WriteLine("available statuses: ");
                Console.WriteLine("(1) - Being Fixed");
                Console.WriteLine("(2) - Fixed");
                Console.WriteLine("(3) - Paid");
                Console.WriteLine("");
                Console.WriteLine("What status would you like?: ");
                userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":
                        filterState = eVehicleState.BeingFixed;
                        loopRunning = false;
                        break;
                    case "2":
                        filterState = eVehicleState.Fixed;
                        loopRunning = false;
                        break;
                    case "3":
                        filterState = eVehicleState.Paid;
                        loopRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid answer!");
                        break;
                }
            }

            return filterState;
        }

        private static eFuelType getVehicleFuelType()
        {
            bool loopRunning = true;
            string userAnswer;
            eFuelType fuelType = eFuelType.Octan95;

            while (loopRunning)
            {
                Console.Clear();
                Console.WriteLine("Available fuel types: ");
                Console.WriteLine("(1) - Soler");
                Console.WriteLine("(2) - Octan95");
                Console.WriteLine("(3) - Octan96");
                Console.WriteLine("(4) - Octan98");
                Console.WriteLine("");
                Console.WriteLine("What type do you choose?: ");
                userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":
                        fuelType = eFuelType.Soler;
                        loopRunning = false;
                        break;
                    case "2":
                        fuelType = eFuelType.Octan95;
                        loopRunning = false;
                        break;
                    case "3":
                        fuelType = eFuelType.Octan96;
                        loopRunning = false;
                        break;
                    case "4":
                        fuelType = eFuelType.Octan98;
                        loopRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid answer!");
                        break;
                }
            }

            return fuelType;
        }

        private static string getVehicleLicenceNumber()
        {
            bool loopRunning = true;
            string userAnswer = "";

            while (loopRunning)
            {
                Console.WriteLine("What is the vehicle licence number?: ");
                userAnswer = Console.ReadLine();
                if (userAnswer.Length == 8 || userAnswer.Length == 7)
                { 
                    loopRunning = false;
                }
                else
                {
                    Console.WriteLine("Not a valid input. Try again!");
                }
            }

            return userAnswer;
        }

        private static string getUserAnswer(string i_FieldToFill)
        {
            bool loopRunning = true;
            string userAnswer = "";

            while (loopRunning)
            {
                Console.Write("Plese enter " + i_FieldToFill + ": ");
                Console.WriteLine("");
                helpUserWithInfput(i_FieldToFill);
                Console.WriteLine("");
                userAnswer = Console.ReadLine();
                if (userAnswer.Length > 0)
                {
                    loopRunning = false;
                }
                else
                {
                    Console.WriteLine("Not a valid input. Try again!");
                }
            }

            return userAnswer;
        }

        private static float getAmount()
        {
            bool waitingForInput = true;
            float resultNumber = 0;

            while (waitingForInput)
            {
                Console.Write("Please enter amount: ");
                string userInput = Console.ReadLine();
                bool goodInput = float.TryParse(userInput, out resultNumber);
                if (goodInput)
                {
                    waitingForInput = false;
                }
                else
                {
                    Console.WriteLine("Not a valid input. Try again!");
                }
            }

            return resultNumber;
        }

        private static string getVehicleType()
        {
            bool loopRunning = true;
            string userAnswer;
            List<string> availableTypes = GarageLogic.GetAvailableVehicleTypes();
            string vehicleType = "";
            int optionChosen;

            Console.Clear();
            Console.WriteLine("available vehicles types: ");
            for (int i = 0; i < availableTypes.Count(); i++)
            {
                Console.WriteLine(string.Format(@"({0}) - {1}", i + 1, availableTypes[i]));
            }

            while (loopRunning)
            {
                Console.WriteLine("");
                Console.WriteLine("What type is the vehicle?: ");
                bool goodInput = int.TryParse(Console.ReadLine(), out optionChosen);
                if (goodInput && optionChosen > 0 && optionChosen <= availableTypes.Count())
                {
                    vehicleType = availableTypes[optionChosen - 1];
                    loopRunning = false;
                }
                else
                {
                    Console.WriteLine("Not a valid option. Try again!");
                }
            }

            return vehicleType;
        }

        private static void helpUserWithInfput(string i_Field)
        {
            if ((i_Field == "Max Air Pressure") || (i_Field == "Maximum Energy in Vehicle"))
            {
                Console.Write("(The " + i_Field + " Has to be Equal/Larger than the Current Amount)");
            }
            else if ((i_Field == "Number of Tires") || (i_Field == "Current Air Pressure") || (i_Field == "Current Air Pressure") || (i_Field == "Energy Left in Vehicle") || (i_Field == "Engine CC") || (i_Field == "Max load"))
            {
                Console.Write("(The Number must be  Larger than 0)");
            }
            else if (i_Field == "Car Color")
            {
                Console.Write("(Please choose a Color between (Grey, White, Black, Blue))");
            }
            else if (i_Field == "Number of Doors")
            {
                Console.Write("(Number of Doors Must be between 2-5)");
            }
            else if (i_Field == "Fuel type")
            {
                Console.Write("(Please choose between (Soler, Octan95, Octan96, Octan98))");
            }
            else if (i_Field == "Licence type")
            {
                Console.Write("(Please choose between (A, AA, B1, BB))");
            }
        }
    }
}