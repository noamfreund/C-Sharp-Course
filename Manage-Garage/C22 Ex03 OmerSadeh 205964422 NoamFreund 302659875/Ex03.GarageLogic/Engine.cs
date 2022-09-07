namespace Ex03
{
    public abstract class Engine
    {
        internal float m_CurrentLevel;
        internal readonly float r_MaxCapacity;
        internal float m_PercentOfEnergyLeft;

        public Engine(float i_CurrentLevel, float i_MaxCapacity)
        {
            if (i_CurrentLevel < 0 || i_MaxCapacity <= 0)
            {
                throw new ArgumentException("Energy amounts must be over the value 0.");
            }

            if (i_CurrentLevel > i_MaxCapacity)
            {
                throw new ArgumentException("Max energy capacity must be higher/equal to current Level.");
            }

            m_CurrentLevel = i_CurrentLevel;
            r_MaxCapacity = i_MaxCapacity;
            m_PercentOfEnergyLeft = (m_CurrentLevel / r_MaxCapacity) * 100;
        }

        public virtual void FuelTheVehicle(float i_AmountToFill, eFuelType? i_FuelType = null)
        {
            if (i_AmountToFill > r_MaxCapacity - m_CurrentLevel)
            {
                throw new ValueOutOfRangeException(new ArgumentException(), 0, r_MaxCapacity - m_CurrentLevel, "Amount of energy");
            }

            m_CurrentLevel = m_CurrentLevel + i_AmountToFill;
            m_PercentOfEnergyLeft = (m_CurrentLevel / r_MaxCapacity) * 100;
        }

        public abstract eFuelType GetFuelType();

        public override string ToString()
        {
            return string.Format("Energy left in precentage: {0:0.00}%\n", m_PercentOfEnergyLeft);
        }
    }

    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public FuelEngine(float i_CurrentLevel, float i_MaxCapacity, eFuelType i_FuelType) : base(i_CurrentLevel, i_MaxCapacity)
        {
            m_FuelType = i_FuelType;
        }

        public override void FuelTheVehicle(float i_AmountToFill, eFuelType? i_FuelType = null)
        {
            if (m_FuelType != i_FuelType)
            {
                throw new ArgumentException("This is not the Corecct Fuel Type for this Vehicle");
            }
            else
            {
                base.FuelTheVehicle(i_AmountToFill, i_FuelType);
            }
        }

        public override eFuelType GetFuelType()
        {
            return m_FuelType;
        }

        public override string ToString()
        {
            return String.Format(base.ToString() + "Type of Fuel: {0}\nFuel Left in litters: {1}\nMaximum amount of fuel: {2}", m_FuelType, m_CurrentLevel, r_MaxCapacity);
        }
    }

    public class ElectricEngine : Engine
    {
        private eFuelType m_FuelType;
        public ElectricEngine(float i_CurrentLevel, float i_MaxCapacity, eFuelType i_FuelType) : base(i_CurrentLevel, i_MaxCapacity)
        {
            m_FuelType = i_FuelType;
        }

        public override eFuelType GetFuelType()
        {
            return m_FuelType;
        }

        public override string ToString()
        {
            return String.Format(base.ToString() + "Battery life in hours: {0:0.00}\nMaximum battery life in hours: {1:0.00}", m_CurrentLevel, r_MaxCapacity);
        }
    }
}