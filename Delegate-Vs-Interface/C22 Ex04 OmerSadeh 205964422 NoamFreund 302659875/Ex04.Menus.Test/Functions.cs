namespace Ex04
{
    public class FunctionsOperator : Interfaces.IActionListener
    {
        private int m_ActionToRun;
        public FunctionsOperator(int i_Choice)
        {
            m_ActionToRun = i_Choice;
        }

        public void RunAction()
        {
            switch(m_ActionToRun)
            {
                case 1:
                    ShowTime();
                    break;
                case 2:
                    ShowDate();
                    break;
                case 3:
                    CountSpaces();
                    break;
                case 4:
                    ShowVersion();
                    break;
                default:
                    break;
            }
        }
        public static void ShowTime()
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
        }

        public static void ShowDate()
        {
            Console.WriteLine(DateTime.Now.ToShortDateString());
        }

        public static void CountSpaces()
        {
            int count = 0;
            Console.WriteLine("Enter a sentence:");
            string input = Console.ReadLine();
            foreach (char c in input)
            {
                if (c == ' ')
                {
                    count++;
                }
            }

            Console.WriteLine(string.Format("Number of spaces: {0}", count));
        }

        public static void ShowVersion()
        {
            Console.WriteLine("Version: 22.3.4.8650");
        }

    }
}
