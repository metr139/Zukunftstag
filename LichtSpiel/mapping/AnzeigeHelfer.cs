using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace LichtSpiel.mapping
{
    internal class AnzeigeHelfer
    {
        public static async Task Pause()
        {
            int milisekunden = 400;
            await Task.Delay(milisekunden);
        }

        public static async Task AnimationAbspielen(Storyboard animation)
        {
            var animationKomplett = new TaskCompletionSource<bool>();
            EventHandler kompletierer = (s, e) => animationKomplett.SetResult(true);

            animation.Completed += kompletierer;

            animation.Begin();
            await animationKomplett.Task;

            animation.Completed -= kompletierer;
        }

        public static void TonAbspielen(string tonName)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(tonName);

            SoundPlayer player = new SoundPlayer(stream);
            player.Load();
            player.Play();
        }
    }
}