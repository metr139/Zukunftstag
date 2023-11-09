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
            for (int zaehler = 0; zaehler < farbliste.Count; zaehler++)
            {
                Farbe farbe = farbliste[zaehler];
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

            for (int zaehler = 0; zaehler < 3; zaehler++)
            {
                Farbe farbe = zufallsGenerator.GibFarbe();

                neueFarbenListe.Hinzufuegen(farbe);
            }

            return neueFarbenListe;
        }

        public async Task BenutzerKnopfRot_Geklickt()
        {
            await _anzeige.AnimationAbspielen(_anzeige.BenutzerKnopfRot);
            _benutzerListe.Hinzufuegen(Farbe.Rot);
            BenutzerEingabeUeberpruefen();
        }

        public async Task BenutzerKnopfBlau_Geklickt()
        {
            await _anzeige.AnimationAbspielen(_anzeige.BenutzerKnopfBlau);
            _benutzerListe.Hinzufuegen(Farbe.Rot);
            BenutzerEingabeUeberpruefen();
        }

        public async Task BenutzerKnopfGruen_Geklickt()
        {
            await _anzeige.AnimationAbspielen(_anzeige.BenutzerKnopfGruen);
            _benutzerListe.Hinzufuegen(Farbe.Gruen);
            BenutzerEingabeUeberpruefen();
        }

        private void BenutzerEingabeUeberpruefen()
        {
            if (_benutzerListe.Laenge == _generierteListe.Laenge)
            {
                for (int zaehler = 0; zaehler < _benutzerListe.Laenge; zaehler++)
                {
                    if (_benutzerListe[zaehler] != _generierteListe[zaehler])
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