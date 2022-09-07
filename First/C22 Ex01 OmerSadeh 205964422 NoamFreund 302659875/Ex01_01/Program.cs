namespace Ex01_01
{
    class Program
    {

        public static int s_InputZeroes = 0;
        public static int s_InputPalindromes = 0;
        public static int s_InputDivisableByThree = 0;
        public static void Main()
        {
            int[] inputNumbers = new int[3];
            string message;

            for (int i = 0; i < 3; i++)
            {
                message = string.Format(@"Please enter binary number {0}: ", i + 1);
                System.Console.Write(message);
                string input = System.Console.ReadLine();
                if (IsValidNumber(input))
                {
                    inputNumbers[i] = ProcessNumber(input);
                }
                else
                {
                    System.Console.WriteLine("Not a valid binary number!");
                    i--;
                }
            }

            System.Console.WriteLine("");
            Array.Sort(inputNumbers);
            System.Console.Write("The sorted array is: ");
            foreach (int i in inputNumbers)
            {
                Console.Write(i + " ");
            }

            System.Console.WriteLine("\n");
            System.Console.WriteLine("Some Statistics:");
            float averageZeroes = (float)s_InputZeroes / 3.0f;
            message = string.Format(@"The average number of 0's is: {0}", averageZeroes);
            System.Console.WriteLine(message);
            foreach (int i in inputNumbers)
            {
                if (i % 3 == 0)
                {
                    s_InputDivisableByThree++;
                }
            }

            message = string.Format(@"The number of numbers divisable by 3 is: {0}", s_InputDivisableByThree);
            System.Console.WriteLine(message);
            message = string.Format(@"The number of palindromes is: {0}", s_InputPalindromes);
            System.Console.WriteLine(message);
        }

        static bool IsValidNumber(string i_input)
        {
            bool validNum = !(i_input.Length == 0);

            for (int i = 0; i < i_input.Length; i++)
            {
                if (i_input[i] != '0' && i_input[i] != '1')
                {
                    validNum = false;
                    break;
                }
            }

            return validNum;
        }

        static int ProcessNumber(string i_input)
        {
            int val;
            int number = 0;

            for (int i = 0; i < i_input.Length; i++)
            {
                val = Int32.Parse(i_input[i].ToString());
                if (val == 1)
                {
                    number += (int)Math.Pow(2, i_input.Length - 1 - i);
                }
                else
                {
                    s_InputZeroes++;
                }
            }

            if (NumberIsPalindrome(number.ToString()))
            {
                s_InputPalindromes++;
            }

            return number;
        }

        static bool NumberIsPalindrome(string i_input)
        {
            char[] inputArray = i_input.ToCharArray();
            Array.Reverse(inputArray);
            string reversedInput = new(inputArray);
            bool isPalindrome = true;

            for (int i = 0; i < i_input.Length; i++)
            {
                if (i_input[i] != reversedInput[i])
                {
                    isPalindrome = false;
                    break;
                }
            }

            return isPalindrome;
        }
    }
}
