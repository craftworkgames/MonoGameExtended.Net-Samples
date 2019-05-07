using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameOnWebGL
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _cake;
        private Vector2 _position;
        private Vector2 _size;
        private Vector2 _direction;
        private float _speed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadContentAsync();
        }

        private async void LoadContentAsync()
        {
            // Use Content.LoadAsync instead of Content.Load if you don't want to freeze the brwoser while content gets loaded
            _cake = await Content.LoadAsync<Texture2D>("cake");
            _position = Vector2.Zero;
            _size = new Vector2(68, 73);
            _direction = Vector2.One;
            _speed = 100;
        }

        protected override void Update(GameTime gameTime)
        {
            if(GraphicsDevice?.Viewport == null)
                return;

            var elapsedSeconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();
            var width = GraphicsDevice.Viewport.Width;
            var height = GraphicsDevice.Viewport.Height;

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            _position += _direction * elapsedSeconds * _speed;

            if (_position.X <= 0)
            {
                _position.X = 0;
                _direction.X = 1;
            }

            if (_position.Y <= 0)
            {
                _position.Y = 0;
                _direction.Y = 1;
            }

            if (_position.X + _size.X >= width)
            {
                _position.X = width - _size.X;
                _direction.X = -1;
            }

            if (_position.Y + _size.X >= height)
            {
                _position.Y = height - _size.X;
                _direction.Y = -1;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (_cake != null)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(_cake, _position - (_size / 2f), Color.White);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
