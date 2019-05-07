using static Retyped.dom;

namespace MonoGameOnWebGL
{
    public class App
    {
        private static Game1 _game;
        
        public static void Main()
        {
            var div = new HTMLDivElement { className = "text-center" };
            var canvas = new HTMLCanvasElement
            {
                width = 800,
                height = 480,
                id = "monogamecanvas",
                className = "monogamecanvas"
            };

            div.appendChild(canvas);
            document.body.appendChild(div);

            window.onload = ev =>
            {
                _game = new Game1();
                _game.Run();
            };
        }
    }
}
