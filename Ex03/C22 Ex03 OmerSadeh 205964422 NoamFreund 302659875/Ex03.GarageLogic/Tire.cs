using System.Text;

namespace Ex03
{
    public class Tire
    {
        private readonly string r_Manufacture;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Tire(string i_Manufacutre, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            if (i_CurrentAirPressure < 0)
            {
                throw new ArgumentException("Current tire air pressure has to be a positive number over 0");
            }

            if (i_MaxAirPressure <= 0)
            {
                throw new ArgumentException("Maximum tire air pressure has to be a positive number over 0");
            }

            if (i_CurrentAirPressure > i_MaxAirPressure)
            {
                ArgumentException e = new ArgumentException();
                throw new ValueOutOfRangeException(e, 0, i_MaxAirPressure, "Current tire air pressure");
            }

            r_Manufacture = i_Manufacutre;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public void FillAirPressure()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }

        public override string ToString()
        {
            StringBuilder tireSpecs = new StringBuilder();
            string vehicleTireSpecs = string.Format("Manufactre name: {0}\nCurrent Air Pressure: {1}\nMax air Pressure Possible: {2}", r_Manufacture, m_CurrentAirPressure, r_MaxAirPressure);

            tireSpecs.AppendLine(vehicleTireSpecs);

            return tireSpecs.ToString();
        }

    }
}
