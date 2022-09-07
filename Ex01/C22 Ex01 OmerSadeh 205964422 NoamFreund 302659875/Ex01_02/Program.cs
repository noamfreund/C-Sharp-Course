using System.Text;

namespace Ex01_02
{
    public class Program
    {
        public static void Main()
        {
            PrintDiamond(9);
        }

        public static void PrintDiamond(int i_height)
        {
            printDiamondRecursively(i_height, 1);
        }

        private static void printDiamondRecursively(int i_maxHeight, int i_currentHeight)
        {
            int numberOfDots;

            if (i_currentHeight > i_maxHeight)
            {
                return;
            }
            if ((i_currentHeight <= i_maxHeight / 2) || ((i_maxHeight > 2 * (i_maxHeight / 2)) && (i_currentHeight == i_maxHeight / 2 + 1)))
            {
                numberOfDots = 2 * i_currentHeight - 1;
            }
            else
            {
                numberOfDots = 2 * (i_maxHeight - i_currentHeight + 1) - 1;
            }
            printDotsLine(numberOfDots, (i_maxHeight - numberOfDots) / 2);
            printDiamondRecursively(i_maxHeight, i_currentHeight + 1);
        }

        private static void printDotsLine(int i_dotsNumber, int i_blanksNumber)
        {
            StringBuilder message = new StringBuilder("");

            for (int i = 0; i < i_blanksNumber; i++)
            {
                message.Append(' ');
            }
            for (int i = 0; i < i_dotsNumber; i++)
            {
                message.Append('*');
            }

            System.Console.WriteLine(message.ToString());
        }
    }
}
