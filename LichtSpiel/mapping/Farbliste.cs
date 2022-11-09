using System.Collections.Generic;

namespace LichtSpiel.mapping
{
    public class Farbliste : List<Farbe>
    {
        public int Laenge 
        {
            get { return this.Count; }
        }

        internal void Hinzufuegen(Farbe value)
        {
            base.Add(value);
        }
    }
}
