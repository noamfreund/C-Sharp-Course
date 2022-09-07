
namespace Ex01_05
{
    internal class Program
    {
        public static void Main()
        {
            bool waitingForInput = true;
            string message;

            while (waitingForInput)
            {
                System.Console.Write("Please enter input: ");
                string input = System.Console.ReadLine();
                int number;
                bool goodInput = int.TryParse(input, out number);
                if (input.Length == 9 && goodInput)
                {
                    message = string.Format(@"The number of digits that are smaller than the first is: {0}", NumberOfSmallerDigits(input));
                    System.Console.WriteLine(message);
                    message = string.Format(@"The largest digit is: {0}", LargestDigit(input));
                    System.Console.WriteLine(message);
                    message = string.Format(@"The number of digits that are divisable by 3 is: {0}", NumberOfDivisableByThreeDigits(input));
                    System.Console.WriteLine(message);
                    message = string.Format(@"The average of all the digits is: {0}", DigitsAverage(input));
                    System.Console.WriteLine(message);
                    waitingForInput = false;
                }
                else
                {
                    System.Console.WriteLine("Not a valid input. Try again!");
                }
            }
        }

        public static int NumberOfSmallerDigits(string i_input)
        {
            int smallerDigitsCount = 0;

            foreach (char c in i_input)
            {
                if (int.Parse(c.ToString()) < int.Parse(i_input[8].ToString()))
                {
                    smallerDigitsCount++;
                }
            }

            return smallerDigitsCount;
        }

        public static int LargestDigit(string i_input)
        {
            int largestDigit = 0, currentDigit;

            foreach (char c in i_input)
            {
                currentDigit = int.Parse(c.ToString());
                if (currentDigit > largestDigit)
                {
                    largestDigit = currentDigit;
                }
            }

            return largestDigit;
        }

        public static int NumberOfDivisableByThreeDigits(string i_input)
        {
            int divisableDigitsCount = 0, currentDigit;

            foreach (char c in i_input)
            {
                currentDigit = int.Parse(c.ToString());
                if (currentDigit % 3 == 0)
                {
                    divisableDigitsCount++;
                }
            }

            return divisableDigitsCount;
        }

        public static float DigitsAverage(string i_input)
        {
            float DigitsSum = 0;

            foreach (char c in i_input)
            {
                DigitsSum += float.Parse(c.ToString());
            }

            return DigitsSum / 9;
        }
    }
}
