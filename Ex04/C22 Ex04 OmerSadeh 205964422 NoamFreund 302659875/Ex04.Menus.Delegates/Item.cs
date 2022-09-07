namespace Delegates
{
    public class Item
    {
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
