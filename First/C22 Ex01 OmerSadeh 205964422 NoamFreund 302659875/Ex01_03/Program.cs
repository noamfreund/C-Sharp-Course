namespace Ex01_03
{
    internal class Program
    {
        public static void Main()
        {
            bool waitingForNumber = true;

            while (waitingForNumber)
            {
                System.Console.Write("Please enter diamond height: ");
                string input = System.Console.ReadLine();
                int number;
                bool goodInput = int.TryParse(input, out number);
                if (goodInput && number >= 0)
                {
                    if(number % 2 == 0)
                    {
                        Ex01_02.Program.PrintDiamond(number + 1);
                        waitingForNumber = false;
                    }
                    else
                    {
                        Ex01_02.Program.PrintDiamond(number);
                        waitingForNumber = false;
                    }
                    
                }
                else
                {
                    System.Console.WriteLine("Not a valid number. Try again!");
                }
            }
        }
    }
}
