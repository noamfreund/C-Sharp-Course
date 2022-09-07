namespace Interfaces
{
    public class Item
    {
        public readonly string r_Name;
        private readonly List<IActionListener> r_ChosenActionsListener;

        public Item(string i_Name)
        {
            r_Name = i_Name;
            r_ChosenActionsListener = new List<IActionListener>();
        }

        public void AddAction(IActionListener i_Action)
        {
            r_ChosenActionsListener.Add(i_Action);
        }

        public void RunAction()
        {
            foreach (IActionListener action in r_ChosenActionsListener)
            {
                action.RunAction();
            }
        }
    }
}
