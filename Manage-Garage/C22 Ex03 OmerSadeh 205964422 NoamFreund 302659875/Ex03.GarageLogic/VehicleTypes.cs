using System.Text;

namespace Ex03
{ 
    public class Car : Vehicle
    {
        private int m_NumbrOfDoors;
        private ePaintColor m_Color;
        public Car(eVehicleType i_VehicleType, string i_OwnerName, string i_OwnerPhoneNumber, string i_LicenseNumber, string i_ModelName, Tire i_Tire,
            int i_AmountOfTires, Engine i_Engine, ePaintColor i_Color, int i_NumbrOfDoors) : base(i_VehicleType, i_OwnerName,
                i_OwnerPhoneNumber, i_LicenseNumber, i_ModelName, i_Tire, i_AmountOfTires, i_Engine)
        {
            m_NumbrOfDoors = i_NumbrOfDoors;
            m_Color = i_Color;
        }

        public override string ToString()
        {
            StringBuilder VehicleSpecs = new StringBuilder();

            VehicleSpecs.Append(base.ToString());
            VehicleSpecs.AppendLine("Number of Doors: " + m_NumbrOfDoors);
            VehicleSpecs.AppendLine("Color: " + m_Color);

            return VehicleSpecs.ToString();
        }
    }

    public class Motorcycle : Vehicle
    {
        private int m_EngineSize;
        private eLicenceType m_TypeOfLicence;

        public Motorcycle(eVehicleType i_VehicleType, string i_OwnerName, string i_OwnerPhoneNumber, Engine i_Enginel, string i_LicenseNumber, string i_ModelName,
            Tire i_Tire, int i_AmountOfTires, eLicenceType i_TypeOfLicence, int i_EngineSize) : base(i_VehicleType, i_OwnerName,
                i_OwnerPhoneNumber, i_LicenseNumber, i_ModelName, i_Tire, i_AmountOfTires, i_Enginel)
        {
            m_EngineSize = i_EngineSize;
            m_TypeOfLicence = i_TypeOfLicence;
        }

        public override string ToString()
        {
            StringBuilder VehicleSpecs = new StringBuilder();

            VehicleSpecs.Append(base.ToString());
            VehicleSpecs.AppendLine("Engine size: " + m_EngineSize);
            VehicleSpecs.AppendLine("Type of licence: " + m_TypeOfLicence);

            return VehicleSpecs.ToString();
        }
    }

    public class Truck : Vehicle
    {
        private bool b_Cooler = true;
        private float m_MaxLoad;

        public Truck(eVehicleType i_VehicleType, string i_OwnerName, string i_OwnerPhoneNumber, string i_LicenseNumber, string i_ModelName, Tire i_Tire,
            int i_AmountOfTires, Engine i_Engine, bool i_Cooler, float i_MaxLoad) : base(i_VehicleType, i_OwnerName,
                i_OwnerPhoneNumber, i_LicenseNumber, i_ModelName, i_Tire, i_AmountOfTires, i_Engine)
        {
            b_Cooler = i_Cooler;
            m_MaxLoad = i_MaxLoad;
        }

        public override string ToString()
        {
            StringBuilder VehicleSpecs = new StringBuilder();

            VehicleSpecs.Append(base.ToString());
            VehicleSpecs.AppendLine("Has cooler system: " + b_Cooler);
            VehicleSpecs.AppendLine("Maximum load: " + m_MaxLoad);

            return VehicleSpecs.ToString();
        }
    }
}