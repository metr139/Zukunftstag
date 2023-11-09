using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using LichtSpiel.mapping;

namespace LichtSpiel
{
    public partial class Anzeige : Window
    {
        private readonly Logik _logik;

        public Anzeige()
        {
            InitializeComponent();

            _logik = new Logik(this);
            _logik.LichtSpielAufstarten();
        }

        private async void Start_Knopf_Geklickt(object sender, RoutedEventArgs e)
        {
            await _logik.Start_Knopf_Geklickt();
        }

        private async void BenutzerKnopfRot_Geklickt(object sender, RoutedEventArgs e)
        {
            await _logik.BenutzerKnopfRot_Geklickt();
        }

        private async void BenutzerKnopfBlau_Geklickt(object sender, RoutedEventArgs e)
        {
            await _logik.BenutzerKnopfBlau_Geklickt();
        }

        private async void BenutzerKnopfGruen_Geklickt(object sender, RoutedEventArgs e)
        {
            await _logik.BenutzerKnopfGruen_Geklickt();
        }

        public async Task AnimationAbspielen(Button aktuellerKnopf)
        {
            Storyboard animation = FindResource("KnopfAnimation") as Storyboard;
            Storyboard.SetTarget(animation, aktuellerKnopf);

            await AnzeigeHelfer.AnimationAbspielen(animation);
        }

        public void BildAnzeigen(string bildName)
        {
            string bildPfad = "pack://application:,,,/images/" + bildName;
            StatusBild.Source = new BitmapImage(new Uri(bildPfad));
        }

        public void TonAbspielen(string tonName)
        {
            AnzeigeHelfer.TonAbspielen(tonName);
        }
    }
}
