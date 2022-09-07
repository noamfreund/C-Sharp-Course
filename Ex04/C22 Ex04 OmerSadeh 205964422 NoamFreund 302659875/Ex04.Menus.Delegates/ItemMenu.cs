using System;

namespace Delegates
{
    public delegate void ActionSelectedHandler();
    
    public class ItemMenu
    {
        public readonly string r_Name;
        private readonly List<ItemMenu> r_Items;
        public event ActionSelectedHandler ActionSelected;

        public ItemMenu(string i_Name)
        {
            r_Name = i_Name;
            r_Items = new List<ItemMenu>();
        }

        public List<ItemMenu> Items
        {
            get { return r_Items; }
        }

        public int GetMaximalValueOption()
        {
            return r_Items.Count();
        }

        public void ShowMenu()
        {
            Console.Write("**");
            Console.Write(r_Name);
            Console.WriteLine("**");
            Console.WriteLine("------------------------");
            int i = 0;
            foreach (ItemMenu itemMenu in r_Items)
            {
                i++;
                Console.Write(i);
                Console.Write(" -> ");
                Console.WriteLine(itemMenu.r_Name);
            }
            Console.WriteLine("0 -> Back");
            Console.WriteLine("------------------------");
            Console.Write("Enter your request: (1 to ");
            Console.Write(i);
            Console.WriteLine(" or press '0' to Back).");
        }

        public void RunOption(int i_optionNumber)
        {
            r_Items[i_optionNumber].ActionChoosen();
        }

        public void ActionChoosen()
        {
            RunActionSelected();
        }

        public void RunActionSelected()
        {
            if (ActionSelected != null)
            {
                ActionSelected.Invoke();
            }
        }

     

    }
}
