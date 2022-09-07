namespace Ex01_04
{
    internal class Program
    {
        public static void Main()
        {
            bool waitingForInput = true;
            bool containsInt = true;
            bool containsLetter = true;

            while (waitingForInput)
            {
                System.Console.Write("Please enter a valid input made of numbers or letters only: ");
                string input = System.Console.ReadLine();
                containsInt = input.Any(char.IsDigit);
                containsLetter = input.Any(char.IsLetter);
     
                if (input.Length != 9|| (containsInt && containsLetter))
                {
                    System.Console.WriteLine("Not a valid input. Try again!");
                }

                else
                {
                    IsPalindrome(input);
                    IsDivisableBy3Number(input);
                    NumberOfLowercaseLetters(input);
                    waitingForInput = false;
                    
                }
            }

            static void IsPalindrome(string i_input)
            {
                if (IsPalindromeRecursive(i_input, 0))
                {
                    System.Console.WriteLine("The string is a palindrome!");
                }
                else
                {
                    System.Console.WriteLine("The string is NOT a palindrome!");
                }
            }

             static bool IsPalindromeRecursive(string i_input, int i_step)
            {
                bool isPalindrome = true;

                if (i_step > i_input.Length / 2)
                {
                    isPalindrome = true;
                }
                else if (i_input[i_step] != i_input[i_input.Length - 1 - i_step])
                {
                    isPalindrome = false;
                }
                else
                {
                    IsPalindromeRecursive(i_input, i_step + 1);
                }
                return isPalindrome;

            }

            static void IsDivisableBy3Number(string i_input)
            {
                int number;
                bool isNumber = int.TryParse(i_input, out number);

                if (isNumber)
                {
                    if (number % 3 == 0)
                    {
                        System.Console.WriteLine("The input is a number, divisable by 3!");
                    }
                    else
                    {
                        System.Console.WriteLine("The input is a number, NOT divisable by 3!");
                    }
                }
                else
                {
                    System.Console.WriteLine("The input is NOT a number!");
                }
            }

            static void NumberOfLowercaseLetters(string i_input)
            {
                int number;
                bool isNumber = int.TryParse(i_input, out number);

                if (!isNumber)
                {
                    number = 0;
                    for (int i = 0; i < i_input.Length; i++)
                    {
                        if (char.IsLower(i_input[i]))
                        {
                            number++;
                        }
                    }

                    string message = string.Format(@"The input is a string in english, with {0} lowercase letters!", number);
                    System.Console.WriteLine(message);
                }
                else
                {
                    System.Console.WriteLine("The input is NOT a string in english!");
                }
            }
        }
    }
}
