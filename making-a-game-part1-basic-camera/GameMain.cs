using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace ElthenBlade
{
    public class GameObject
    {
        public Vector2 Position { get; set; }
        public Sprite Sprite { get; set; }
        public float Speed { get; set; }
        public Vector2 Offset => Sprite.Origin;
    }

    public class GameMain : Game
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;
        private OrthographicCamera _camera;

        private Texture2D _background;
        private Texture2D _adventurer;
        private GameObject _player;

        public GameMain()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            IsFixedTimeStep = true;
            _graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
        }

        protected override void LoadContent()
        {
            _background = Content.Load<Texture2D>("background");
            _adventurer = Content.Load<Texture2D>("adventurer");

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _camera = new OrthographicCamera(GraphicsDevice) { Zoom = 2.0f };

            _player = new GameObject
            {
                Sprite = new Sprite(_adventurer),
                Position = new Vector2(120, 200),
                Speed = 128
            };
        }

        protected override void Update(GameTime gameTime)
        {
            var elapsedSeconds = gameTime.GetElapsedSeconds();
            var keyboardState = Keyboard.GetState();
            var direction = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                direction.X -= 1;
                _player.Sprite.Effect = SpriteEffects.FlipHorizontally;
            }

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                direction.X += 1;
                _player.Sprite.Effect = SpriteEffects.None;
            }

            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                direction.Y -= 1;

            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                direction.Y += 1;

            if (keyboardState.IsKeyDown(Keys.R))
                _camera.ZoomIn(elapsedSeconds);

            if (keyboardState.IsKeyDown(Keys.F))
                _camera.ZoomOut(elapsedSeconds);


            _player.Position += direction * _player.Speed * elapsedSeconds;

            var lookAtPosition = Vector2.Lerp(_camera.Position + _camera.Origin, _player.Position, 0.05f);
            _camera.LookAt(lookAtPosition);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.GetViewMatrix());
            _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            _spriteBatch.Draw(_player.Sprite, _player.Position);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
