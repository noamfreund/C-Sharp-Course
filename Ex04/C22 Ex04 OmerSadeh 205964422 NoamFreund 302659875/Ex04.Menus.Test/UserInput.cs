namespace Ex04
{
    internal class UserInput
    {
        public static int GetUserInput(int i_MaximalValue)
        {
            int choice = 0;
            bool loopRunning = true;
            bool goodInput;
            string userInput;

            while (loopRunning)
            {
                userInput = Console.ReadLine();
                goodInput = int.TryParse(userInput, out choice);
                if (goodInput && choice <= i_MaximalValue && choice >= 0)
                {
                    loopRunning = false;
                }
                else
                {
                    Console.WriteLine("Invalid Input! Please try again.");
                }
            }

            return choice;
        }
    }
}
