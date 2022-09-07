namespace Interfaces
{
    public class ItemMenu
    {
        public readonly string r_Name;
        private readonly List<Item> r_Items;

        public ItemMenu(string i_Name)
        {
            r_Name = i_Name;
            r_Items = new List<Item>();
        }

        public void AddItem(string i_Name, IActionListener i_Action)
        {
            Item newItem = new Item(i_Name);

            newItem.AddAction(i_Action);
            r_Items.Add(newItem);
        }

        public int GetMaximalValueOption()
        {
            return r_Items.Count();
        }

        public void ShowMenu()
        {
            int i = 0;

            Console.WriteLine(string.Format("**{0}**", r_Name));
            Console.WriteLine("------------------------");
            foreach (Item item in r_Items)
            {
                i++;
                Console.WriteLine(string.Format("{0} -> {1}", i, item.r_Name));
            }
            Console.WriteLine("0 -> Back");
            Console.WriteLine("------------------------");
            Console.WriteLine(string.Format("Enter your request: (1 to {0} or press '0' to Back).", i));
        }

        public void RunOption(int i_optionNumber)
        {
            r_Items[i_optionNumber].RunAction();
        }
    }
}
