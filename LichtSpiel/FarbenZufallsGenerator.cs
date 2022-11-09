using System;

namespace LichtSpiel
{
    public class FarbenZufallsGenerator
    {
        private Random _random;

        public FarbenZufallsGenerator()
        {
            _random = new Random();
        }

        public Farbe GibFarbe()
        {
            int von = 1;
            int bis = 3;

            int zahl = _random.Next(von, bis + 1);
            Farbe farbe = (Farbe)zahl;

            return farbe;
        }
    }
}
