using System.Windows.Controls;

namespace LichtSpiel.mapping
{
    public class Knopf : Button
    {
        public void Einschalten()
        {
            IsEnabled = true;
        }

        public void Ausschalten()
        {
            IsEnabled = false;
        }
    }
}