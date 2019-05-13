using System;

namespace GettingStarted
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new GameMain())
                game.Run();
        }
    }
}
