namespace Ex03
{
    public  class GarageLogic
    {
        readonly Dictionary<string, Vehicle> r_Garage = new Dictionary<string, Vehicle>();

        public void AddNewVehicle(IDictionary<string, string> i_NewVehicleInfo)
        {
            Console.WriteLine(i_NewVehicleInfo["Current Air Pressure"]);
            Tire newTiresList = new Tire(i_NewVehicleInfo["Tire Manufacturer"], getFloat(i_NewVehicleInfo["Current Air Pressure"], "Current Air Pressure"), getFloat(i_NewVehicleInfo["Max Air Pressure"], "Max Air Pressure"));
            Engine engine;
            eFuelType fuelType = eFuelType.Electricty;
            float currentEnergy = getFloat(i_NewVehicleInfo["Energy Left in Vehicle"], "Energy Left in Vehicle");
            float maxEnergy = getFloat(i_NewVehicleInfo["Maximum Energy in Vehicle"], "Maximum Energy in Vehicle");
            int numberOfTires = getInt(i_NewVehicleInfo["Number of Tires"], "Number of Tires");
            string ownerName = i_NewVehicleInfo["Vehicles Owner Name"];
            string ownerPhoneNumber = i_NewVehicleInfo["Vehicles Owner Phone Number"];
            string modelName = i_NewVehicleInfo["Model Name"];
            string licencePlate = i_NewVehicleInfo["licencePlate"];
            eVehicleType vehicleType = getVehicleType(i_NewVehicleInfo["vehicleType"]);
            Vehicle newVehicle;
            ePaintColor paintColor;
            eLicenceType licenceType;
            int numberOfDoors;
            int engineCC;

            switch (vehicleType)
            {
                case eVehicleType.ElectricCar:
                    engine = new ElectricEngine(currentEnergy, maxEnergy, fuelType);
                    paintColor = getPaintColor(i_NewVehicleInfo["Car Color"]);
                    numberOfDoors = getNumberOfDoors(i_NewVehicleInfo["Number of Doors"]);
                    newVehicle = new Car(vehicleType, ownerName, ownerPhoneNumber, licencePlate, modelName, newTiresList, numberOfTires, engine, paintColor, numberOfDoors);
                    break;
                case eVehicleType.FueledCar:
                    fuelType = getFuelType(i_NewVehicleInfo["Fuel type"]);
                    engine = new FuelEngine(currentEnergy, maxEnergy, fuelType);
                    paintColor = getPaintColor(i_NewVehicleInfo["Car Color"]);
                    numberOfDoors = getNumberOfDoors(i_NewVehicleInfo["Number of Doors"]);
                    newVehicle = new Car(vehicleType, ownerName, ownerPhoneNumber, licencePlate, modelName, newTiresList, numberOfTires, engine, paintColor, numberOfDoors);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    engine = new ElectricEngine(currentEnergy, maxEnergy, fuelType);
                    licenceType = getLicenceType(i_NewVehicleInfo["Licence type"]);
                    engineCC = getInt(i_NewVehicleInfo["Engine CC"], "Engine CC");
                    newVehicle = new Motorcycle(vehicleType, ownerName, ownerPhoneNumber, engine, licencePlate, modelName, newTiresList, numberOfTires, licenceType, engineCC);
                    break;
                case eVehicleType.FueledMotorcycle:
                    fuelType = getFuelType(i_NewVehicleInfo["Fuel type"]);
                    engine = new FuelEngine(currentEnergy, maxEnergy, fuelType);
                    licenceType = getLicenceType(i_NewVehicleInfo["Licence type"]);
                    engineCC = getInt(i_NewVehicleInfo["Engine CC"], "Engine CC");
                    newVehicle = new Motorcycle(vehicleType, ownerName, ownerPhoneNumber, engine, licencePlate, modelName, newTiresList, numberOfTires, licenceType, engineCC);
                    break;
                case eVehicleType.Truck:
                    fuelType = getFuelType(i_NewVehicleInfo["Fuel type"]);
                    engine = new FuelEngine(currentEnergy, maxEnergy, fuelType);
                    bool coolerSystem = getBool(i_NewVehicleInfo["Cooler System (True/False)"], "Cooler System");
                    float maxLoad = float.Parse(i_NewVehicleInfo["Max load"]);
                    newVehicle = new Truck(vehicleType, ownerName, ownerPhoneNumber, licencePlate, modelName, newTiresList, numberOfTires, engine, coolerSystem, maxLoad);
                    break;
                default:
                    throw new ArgumentException("Failed to get Vehicle Type, did not recieve a valid input.");
            }

            r_Garage.Add(licencePlate, newVehicle);
        }

        public List<string> ShowVehicleList(eVehicleState i_VehicleState)
        {
            int i = 1;
            List<string> vehicleList = new List<string>();

            if (r_Garage.Count == 0)
            {
                Console.WriteLine("The Garage System is Empty");
            }
            else
            {
                foreach (KeyValuePair<string, Vehicle> entry in r_Garage)
                {
                    if (entry.Value.CheckVehicleState(i_VehicleState))
                    {
                        vehicleList.Add(i + ") " + entry.Key);
                        i++;
                    }
                }
            }

            return vehicleList;    
        }

        public List<string> ShowAllVehiclesInList()
        {
            int i = 1;
            List<string> vehicleList = new List<string>();

            if (r_Garage.Count == 0)
            {
                Console.WriteLine("The Garage System is Empty");
            }
            else
            {
                foreach (KeyValuePair<string, Vehicle> entry in r_Garage)
                {
                    vehicleList.Add(i + ") " + entry.Key);
                    i++;
                }
            }
 
            return vehicleList;
        }

        public void SetVehicleStatus(string i_LicenseNumber, eVehicleState i_NewStatus)
        {
            CheckVehicleExists(i_LicenseNumber);
            r_Garage[i_LicenseNumber].SetVehicleStatus(i_NewStatus);
        }

        public void FillAirPreasure(string i_LicenseNumber)
        {
            CheckVehicleExists(i_LicenseNumber);
            r_Garage[i_LicenseNumber].FillAirPressure();
        }

        public void FuelVehicle(string i_LicenseNumber, eFuelType i_Type, float i_Amount)
        {
            CheckVehicleExists(i_LicenseNumber);
            if(i_Type == eFuelType.Electricty)
            {
                throw new ArgumentException("Chosen vehicle is not a fuel vehicle.");
            }
            else
            {
                r_Garage[i_LicenseNumber].Engine.FuelTheVehicle(i_Amount, i_Type);
            }  
        }
        
        public void ChargeVehicle(string i_LicenseNumber, float i_Amount)
        {
            CheckVehicleExists(i_LicenseNumber);
            if (r_Garage[i_LicenseNumber].Engine.GetFuelType() == eFuelType.Electricty)
            {
                r_Garage[i_LicenseNumber].Engine.FuelTheVehicle(i_Amount, null);
            }
            else
            {
                throw new ArgumentException("Chosen vehicle is not an electric vehicle.");
            }
        }

        public string ShowVehicleSpecs(string i_LicenseNumber)
        {
            CheckVehicleExists(i_LicenseNumber);

            return r_Garage[i_LicenseNumber].ToString(); 
        }

        public void CheckVehicleExists(string i_LicenseNumber)
        {
            if (!r_Garage.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle doesn't exist in the garage.");
            }
        }

        public bool VehicleExists(string i_LicenseNumber)
        {
            return r_Garage.ContainsKey(i_LicenseNumber);
        }

        public static List<string> GetVehicleFields(string i_VehicleType)
        {
            List<string> fieldsToFill = new List<string>();

            fieldsToFill.Add("Vehicles Owner Name");
            fieldsToFill.Add("Vehicles Owner Phone Number");
            fieldsToFill.Add("Model Name");
            fieldsToFill.Add("Number of Tires");
            fieldsToFill.Add("Tire Manufacturer");
            fieldsToFill.Add("Current Air Pressure");
            fieldsToFill.Add("Max Air Pressure");
            fieldsToFill.Add("Energy Left in Vehicle");
            fieldsToFill.Add("Maximum Energy in Vehicle");
            switch (i_VehicleType)
            {
                case "ElectricCar":
                    fieldsToFill.Add("Car Color");
                    fieldsToFill.Add("Number of Doors");
                    break;
                case "FueledCar":
                    fieldsToFill.Add("Car Color");
                    fieldsToFill.Add("Number of Doors");
                    fieldsToFill.Add("Fuel type");
                    break;
                case "ElectricMotorcycle":
                    fieldsToFill.Add("Licence type");
                    fieldsToFill.Add("Engine CC");
                    break;
                case "FueledMotorcycle":
                    fieldsToFill.Add("Licence type");
                    fieldsToFill.Add("Engine CC");
                    fieldsToFill.Add("Fuel type");
                    break;
                case "Truck":
                    fieldsToFill.Add("Cooler System (True/False)");
                    fieldsToFill.Add("Max load");
                    fieldsToFill.Add("Fuel type");
                    break;
                default:
                    throw new Exception("Wrong vehicle type input, please type ElectricCar, FueledCar, ElectricMotorcycle, FueledMotorcycle or Truck");
            }

            return fieldsToFill;
        }

        public static List<string> GetAvailableVehicleTypes()
        {
            List<string> vehicleTypesList = new List<string>();
            
            for(eVehicleType type = eVehicleType.ElectricCar; type <= eVehicleType.Truck; type++)
            {
                vehicleTypesList.Add(type.ToString()); 
            }
                          
            return vehicleTypesList;
        }

        private float getFloat(string i_Input, string i_Field)
        {
            float result;
            bool parsingPassed = float.TryParse(i_Input, out result);

            if (!parsingPassed)
            {
                throw new FormatException(string.Format("Failed to get {0}, did not recieve a valid number.", i_Field));
            }

            return result;
        }

        private Int32 getInt(string i_Input, string i_Field)
        {
            Int32 result;
            bool parsingPassed = Int32.TryParse(i_Input, out result);

            if (!parsingPassed)
            {
                throw new FormatException(string.Format("Failed to get {0}, did not recieve a valid number.", i_Field));
            }

            return result;
        }

        private bool getBool(string i_Input, string i_Field)
        {
            bool result;
            bool parsingPassed = bool.TryParse(i_Input, out result);

            if (!parsingPassed)
            {
                throw new FormatException(string.Format("Failed to get {0}, did not recieve True/False.", i_Field));
            }

            return result;
        }

        private eFuelType getFuelType(string i_Input)
        {
            eFuelType result;
            bool parsingPassed = eFuelType.TryParse(i_Input, out result);

            if (!parsingPassed)
            {
                throw new FormatException("Failed to get fuel type, did not recieve a valid answer.");
            }

            return result;
        }

        private ePaintColor getPaintColor(string i_Input)
        {
            ePaintColor result;
            bool parsingPassed = ePaintColor.TryParse(i_Input, out result);

            if (!parsingPassed)
            {
                throw new FormatException("Failed to get paint color. did not recieve a valid answer (Blue, Black, White or Gray).");
            }

            return result;
        }

        private eLicenceType getLicenceType(string i_Input)
        {
            eLicenceType result;
            bool parsingPassed = eLicenceType.TryParse(i_Input, out result);

            if (!parsingPassed)
            {
                throw new FormatException("Failed to get licence type. did not recieve a valid answer (A, AA, B1, BB).");
            }

            return result;
        }

        private eVehicleType getVehicleType(string i_Input)
        {
            eVehicleType result;
            bool parsingPassed = eVehicleType.TryParse(i_Input, out result);

            if (!parsingPassed)
            {
                throw new FormatException("Failed to get vehicle type. did not recieve a valid answer.");
            }
            return result;
        }

        private int getNumberOfDoors(string i_Input)
        {
            int numberOfDoors = getInt(i_Input, "Number of Doors");

            if (numberOfDoors <= 0 || numberOfDoors > 5)
            {
                throw new ValueOutOfRangeException(new ArgumentException(), 1, 5, "number of doors");
            }

            return numberOfDoors;
        }

        public bool IsGarageEmpty()
        {
            return r_Garage.Count == 0;
        }

        public bool IsElectricVehicle(string i_LicenseNumber)
        {
            bool IsElectricVehicle = false;
            eVehicleType VehicleType = r_Garage[i_LicenseNumber].eVehicleType;

            if (VehicleType == eVehicleType.ElectricMotorcycle || VehicleType == eVehicleType.ElectricCar)
            {
                IsElectricVehicle = true;
            }

            return IsElectricVehicle;
        }
    }
}