using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using LichtSpiel.mapping;

namespace LichtSpiel
{
    public class Logik
    {
        private readonly Anzeige _anzeige;
        private Farbliste _generierteListe;
        private Farbliste _benutzerListe;

        public Logik(Anzeige anzeige)
        {
            _anzeige = anzeige;
        }

        public void LichtSpielAufstarten()
        {
            _anzeige.BenutzerKnopfRot.Ausschalten();
            _anzeige.BenutzerKnopfBlau.Ausschalten();
            _anzeige.BenutzerKnopfGruen.Ausschalten();

            _anzeige.KnopfRot.Background = Brushes.DarkRed;
            _anzeige.BenutzerKnopfRot.Background = Brushes.DarkRed;

            _anzeige.KnopfBlau.Background = Brushes.SteelBlue;
            _anzeige.BenutzerKnopfBlau.Background = Brushes.SteelBlue;

            _anzeige.KnopfGruen.Background = Brushes.Green;
            _anzeige.BenutzerKnopfGruen.Background = Brushes.Green;
        }

        public async Task Start_Knopf_Geklickt()
        {
            KnoepfeAbschalten();
            await AnzeigeHelfer.Pause();

            _generierteListe = ZufaelligeFarbliste();
            _benutzerListe = new Farbliste();

            await FarbenAbspielen(_generierteListe);
            KnoepfeEinschalten();
        }

        private void KnoepfeEinschalten()
        {
            _anzeige.KnopfStart.Einschalten();
            _anzeige.BenutzerKnopfRot.Einschalten();
            _anzeige.BenutzerKnopfBlau.Einschalten();
            _anzeige.BenutzerKnopfGruen.Einschalten();
        }

        private void KnoepfeAbschalten()
        {
            _anzeige.KnopfStart.Ausschalten();
            BenutzerKnoepfeAbschalten();
        }

        private void BenutzerKnoepfeAbschalten()
        {
            _anzeige.BenutzerKnopfRot.Ausschalten();
            _anzeige.BenutzerKnopfBlau.Ausschalten();
            _anzeige.BenutzerKnopfGruen.Ausschalten();
        }

        private async Task FarbenAbspielen(Farbliste farbliste)
        {
            foreach (Farbe farbe in farbliste)
            {
                await EineFarbeAbspielen(farbe);
            }
        }

        private async Task EineFarbeAbspielen(Farbe farbe)
        {
            Button aktuellerKnopf = null;

            switch (farbe)
            {
                case Farbe.Rot:
                    {
                        aktuellerKnopf = _anzeige.KnopfRot;
                        break;
                    }
                case Farbe.Blau:
                    {
                        aktuellerKnopf = _anzeige.KnopfBlau;
                        break;
                    }
                case Farbe.Gruen:
                    {
                        aktuellerKnopf = _anzeige.KnopfGruen;
                        break;
                    }
            }

            await _anzeige.AnimationAbspielen(aktuellerKnopf);
        }

        private Farbliste ZufaelligeFarbliste()
        {
            var neueFarbenListe = new Farbliste();
            var zufallsGenerator = new FarbenZufallsGenerator();

            for (int i = 0; i < 3; i++)
            {
                Farbe farbe = zufallsGenerator.GibFarbe();

                neueFarbenListe.Hinzufuegen(farbe);
            }

            return neueFarbenListe;
        }

        public void BenutzerKnopfRot_Geklickt()
        {
            _benutzerListe.Hinzufuegen(Farbe.Rot);
            BenutzerEingabeUeberpruefen();
        }

        public void BenutzerKnopfBlau_Geklickt()
        {
            _benutzerListe.Hinzufuegen(Farbe.Rot);
            BenutzerEingabeUeberpruefen();
        }

        public void BenutzerKnopfGruen_Geklickt()
        {
            _benutzerListe.Hinzufuegen(Farbe.Gruen);
            BenutzerEingabeUeberpruefen();
        }

        private void BenutzerEingabeUeberpruefen()
        {
            if (_benutzerListe.Laenge == _generierteListe.Laenge)
            {
                for (int i = 0; i < _benutzerListe.Laenge; i++)
                {
                    if (_benutzerListe[i] != _generierteListe[i])
                    {
                        UngueltigeEingabe();
                        BenutzerKnoepfeAbschalten();
                        return;
                    }
                }

                RichtigeEingabe();
                BenutzerKnoepfeAbschalten();
                return;
            }
        }

        private void UngueltigeEingabe()
        {
            _anzeige.TonAbspielen("LichtSpiel.sounds.Boo.wav");
            _anzeige.BildAnzeigen("Falsch.png");
        }

        private void RichtigeEingabe()
        {
            _anzeige.TonAbspielen("LichtSpiel.sounds.Applaus.wav");
            _anzeige.BildAnzeigen("Richtig.png");
        }
    }
}