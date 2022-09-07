using Delegates;

namespace Ex04
{
    public class Program
    {
        public static void Main()
        {
            runInterfacesMenu();
            Console.Clear();
            runDelegatesMenu();
        }

        private static void runInterfacesMenu()
        {
            bool loopRunningMain = true;
            bool loopRunningSub;
            int choiceMain;
            int choiceSub;
            Interfaces.MainMenu InterfacesMenu = new Interfaces.MainMenu();

            InterfacesMenu.AddItemMenu("Version and Spaces");
            InterfacesMenu.Items[0].AddItem("Count Spaces", new FunctionsOperator(3));
            InterfacesMenu.Items[0].AddItem("Show Version", new FunctionsOperator(4));
            InterfacesMenu.AddItemMenu("Show Date/Time");
            InterfacesMenu.Items[1].AddItem("Show Time", new FunctionsOperator(1));
            InterfacesMenu.Items[1].AddItem("Show Date", new FunctionsOperator(2));

            while (loopRunningMain)
            {
                InterfacesMenu.ShowMenu();
                choiceMain = UserInput.GetUserInput(InterfacesMenu.GetMaximalValueOption());
                if (choiceMain == 0)
                {
                    loopRunningMain = false;
                }
                else
                {
                    loopRunningSub = true;
                    while (loopRunningSub)
                    {
                        InterfacesMenu.Items[choiceMain - 1].ShowMenu();
                        choiceSub = UserInput.GetUserInput(InterfacesMenu.Items[choiceMain - 1].GetMaximalValueOption());
                        if (choiceSub == 0)
                        {
                            loopRunningSub = false;
                        }
                        else
                        {
                            InterfacesMenu.Items[choiceMain - 1].RunOption(choiceSub - 1);
                        }
                    }
                }
            }

        }

        private static void runDelegatesMenu()
        {
            bool loopRunningMain = true;
            bool loopRunningSub;
            int choiceMain;
            int choiceSub;
            MainMenu DelegatesMenu = new MainMenu();

            ItemMenu showDateTime = new ItemMenu("Show Date/Time");
            ItemMenu showTime = new ItemMenu("Show Time");
            ItemMenu showDate = new ItemMenu("Show Date");
            showDateTime.Items.Add(showTime);
            showDateTime.Items.Add(showDate);
            DelegatesMenu.Items.Add(showDateTime);
            showTime.ActionSelected += Item.ShowTime;
            showDate.ActionSelected += Item.ShowDate;

            ItemMenu versionAndSpaces = new ItemMenu("Version and Spaces");
            ItemMenu countSpaces = new ItemMenu("Count Spaces");
            ItemMenu showVersion = new ItemMenu("Show Version");
            versionAndSpaces.Items.Add(countSpaces);
            versionAndSpaces.Items.Add(showVersion);
            DelegatesMenu.Items.Add(versionAndSpaces);
            countSpaces.ActionSelected += Item.CountSpaces;
            showVersion.ActionSelected += Item.ShowVersion;

            while (loopRunningMain)
            {
                DelegatesMenu.ShowMenu();
                choiceMain = UserInput.GetUserInput(DelegatesMenu.GetMaximalValueOption());
                if (choiceMain == 0)
                {
                    loopRunningMain = false;
                }
                else
                {
                    loopRunningSub = true;
                    while (loopRunningSub)
                    {
                        DelegatesMenu.Items[choiceMain - 1].ShowMenu();
                        choiceSub = UserInput.GetUserInput(DelegatesMenu.Items[choiceMain - 1].GetMaximalValueOption());
                        if (choiceSub == 0)
                        {
                            loopRunningSub = false;
                        }
                        else
                        {
                            DelegatesMenu.Items[choiceMain - 1].RunOption(choiceSub - 1);
                        }
                    }
                }
            }
        }
    }
}
