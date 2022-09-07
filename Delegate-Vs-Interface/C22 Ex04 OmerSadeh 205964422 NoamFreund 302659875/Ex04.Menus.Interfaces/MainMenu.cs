namespace Interfaces
{
    public class MainMenu
    {
        private  readonly List<ItemMenu> r_Items;

        public MainMenu()
        {
            r_Items = new List<ItemMenu>();
        }

        public List<ItemMenu> Items
        {
            get { return r_Items; }
        }

        public void AddItemMenu(string i_Name)
        {
            ItemMenu itemMenu = new ItemMenu(i_Name);

            r_Items.Add(itemMenu);
        }

        public void ShowMenu()
        {
            int i = 0;

            Console.WriteLine("**Interfaces Main Menu**");
            Console.WriteLine("------------------------");
            foreach (ItemMenu itemMenu in r_Items)
            {
                i++;
                Console.WriteLine(string.Format("{0} -> {1}", i, itemMenu.r_Name));
            }

            Console.WriteLine("0 -> Exit");
            Console.WriteLine("------------------------");
            Console.WriteLine(string.Format("Enter your request: (1 to {0} or press '0' to Exit).", i));
        }

        public int GetMaximalValueOption()
        {
            return r_Items.Count();
        }
    }
}
