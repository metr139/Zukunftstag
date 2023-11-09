using System;

namespace LichtSpiel
{
    public class FarbenZufallsGenerator
    {
        private readonly Random _zufallsZahlenGenerator;

        public FarbenZufallsGenerator()
        {
            _zufallsZahlenGenerator = new Random();
        }

        public Farbe GibFarbe()
        {
            int von = 1;
            int bis = 3;

            int zahl = _zufallsZahlenGenerator.Next(von, bis + 1);

            Farbe farbe = (Farbe)zahl;

            return farbe;
        }
    }
}