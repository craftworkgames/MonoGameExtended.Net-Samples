using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace MyGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.FillRectangle(100, 100, 200, 200, Color.Red);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
