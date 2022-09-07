using System.Text;

namespace Ex03
{ 
    public class Vehicle
    {
        private readonly string r_VechicleOwnerName;
        private readonly string r_VechicleOwnerPhoneNumber;
        private readonly string r_LicenseNumber;
        private readonly string r_ModelName;
        protected List<Tire> m_Tires;
        private Engine m_Engine;
        private eVehicleState m_VechicleStatus;
        private eVehicleType m_VehicleType;

        public Vehicle(eVehicleType i_VehicleType, string i_OwnerName, string i_OwnerPhoneNumber, string i_LicenseNumber, string i_ModelName, Tire i_Tire, int i_AmountOfTires, Engine i_Engine)
        {
            r_VechicleOwnerName = i_OwnerName;
            r_VechicleOwnerPhoneNumber = i_OwnerPhoneNumber;
            m_Engine = i_Engine;
            r_LicenseNumber = i_LicenseNumber;
            r_ModelName = i_ModelName;
            m_VechicleStatus = eVehicleState.BeingFixed;
            m_VehicleType = i_VehicleType;
            m_Tires = new List<Tire>();

            for (int i = 0; i < i_AmountOfTires; i++)
            {
                m_Tires.Add(i_Tire);
            }
        }

        public void FillAirPressure()
        {
            foreach (Tire tire in m_Tires)
            {
                tire.FillAirPressure();
            }
        }

        public void SetVehicleStatus(eVehicleState i_VehicleState)
        {
            m_VechicleStatus = i_VehicleState;
        }

        public bool CheckVehicleState(eVehicleState i_VehicleState)
        {
            return m_VechicleStatus == i_VehicleState;
        }

        public Engine Engine
        {
            get { return m_Engine; }
        }

        public eVehicleType eVehicleType
        {
            get { return m_VehicleType; }
        }

        public override string ToString()
        {
            StringBuilder vehicleSpecs = new StringBuilder();

            vehicleSpecs.AppendLine("License Number: " + r_LicenseNumber);
            vehicleSpecs.AppendLine("Model Name: " + r_ModelName);
            vehicleSpecs.AppendLine("Status: " + m_VechicleStatus.ToString());
            vehicleSpecs.AppendLine("Number of Tires: " + m_Tires.Count());
            vehicleSpecs.AppendLine("");
            for (int i = 0; i < m_Tires.Count; i++)
            {
                vehicleSpecs.AppendLine("Tire number " + (i + 1) + ": ");
                vehicleSpecs.Append(m_Tires[0].ToString());
            }

            vehicleSpecs.AppendLine("");
            vehicleSpecs.AppendLine(m_Engine.ToString());
     
            return vehicleSpecs.ToString();
        }
    }
}
