namespace Delegates
{
    public class MainMenu
    {
        private readonly List<ItemMenu> r_Items;

        public MainMenu()
        {
            r_Items = new List<ItemMenu>();
           
        }

        public List<ItemMenu> Items
        {
            get { return r_Items; }
        }

        public void ShowMenu()
        {
            Console.WriteLine("**Delegates Main Menu**");
            Console.WriteLine("------------------------");
            int i = 0;
            foreach (ItemMenu itemMenu in r_Items)
            {
                i++;
                Console.Write(i);
                Console.Write(" -> ");
                Console.WriteLine(itemMenu.r_Name);
            }
            Console.WriteLine("0 -> Exit");
            Console.WriteLine("------------------------");
            Console.Write("Enter your request: (1 to ");
            Console.Write(i);
            Console.WriteLine(" or press '0' to Exit).");
        }

        public int GetMaximalValueOption()
        {
            return r_Items.Count();
        }
    }
}
