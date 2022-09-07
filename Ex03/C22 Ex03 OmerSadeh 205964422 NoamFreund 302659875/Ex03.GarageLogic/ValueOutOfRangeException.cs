namespace Ex03
{
    internal class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;

        public float MaxValue
        {
            get { return m_MaxValue; }
        }

        private float m_MinValue;
        public float MinValue
        {
            get { return m_MinValue; }
        }

        public ValueOutOfRangeException(Exception i_InnerException, float i_MinValue, float i_MaxValue, string i_Field)
                : base(string.Format("{0} must be in range {1} - {2}", i_Field, i_MinValue, i_MaxValue),
                i_InnerException)
        { }
    }
}
