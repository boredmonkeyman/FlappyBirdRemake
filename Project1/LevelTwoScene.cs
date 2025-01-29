using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace Project1
{
    public class LevelTwoScene : GameScene
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _birdTexture;
        private Vector2 _birdPosition;
        private Vector2 _birdVelocity;
        private Texture2D _pipeTexture;
        private Vector2[] _pipePositions;
        private SpriteFont _font;

        //private int _score;
        private int _lives;

        private KeyboardState _currentKeyState;
        private KeyboardState _previousKeyState;

        private bool _isCollision;
        private float _collisionTimer;
        private Explosion explosion;
        private Texture2D explosionTexture;

        public int LivesRemaining => _lives;
        public bool PlayerOutOfLives => _lives <= 0;


        public LevelTwoScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            _spriteBatch = spriteBatch; // Assign the shared SpriteBatch
            _birdPosition = new Vector2(0, 200);
            _birdVelocity = new Vector2(2, 0);
            _pipePositions = new[] { new Vector2(700, 300), new Vector2(500, 200), new Vector2(300, 300), new Vector2(100, 400) };
            //_score = 0;
            _lives = 3;
            _isCollision = false;
            _collisionTimer = 0;
            /* EndScene endScene = (EndScene)this.Game.Components.FirstOrDefault(c => c is EndScene);*/
        }

        protected override void LoadContent()
        {
            try
            {
                _birdTexture = Game.Content.Load<Texture2D>("images/yellowbird-upflap");
                _pipeTexture = Game.Content.Load<Texture2D>("images/pipe-green");
                _font = Game.Content.Load<SpriteFont>("ScoreFont");
                explosionTexture = Game.Content.Load<Texture2D>("images/explosion");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading content: {ex.Message}");
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            if (_birdTexture == null || _lives <= 0) return;

            _currentKeyState = Keyboard.GetState();

            //checks for collision
            if (_isCollision)
            {
                _collisionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_collisionTimer > 1)
                {
                    _isCollision = false;
                    _collisionTimer = 0;
                }

                base.Update(gameTime);
                return;
            }

            _birdPosition.X += _birdVelocity.X;

            //bird velocity logic
            if (IsKeyPressed(Keys.Space)) _birdVelocity.Y = -5;
            _birdVelocity.Y += 0.2f;
            _birdPosition.Y += _birdVelocity.Y;

            if (_birdPosition.Y < 0) _birdPosition.Y = 0;

            if (_birdPosition.Y > Shared.stage.Y - _birdTexture.Height)
                _birdPosition.Y = Shared.stage.Y - _birdTexture.Height;

            if (_birdPosition.X > Shared.stage.X)
            {
                Game1 game = (Game1)Game;
                this.hide();
                game.endScene.show();

                return;
            }

            //collision logic
            foreach (var pipePosition in _pipePositions)
            {
                if (CheckCollision(pipePosition))
                {
                    _lives--;
                    _isCollision = true;

                    explosion = new Explosion(Game, _spriteBatch, explosionTexture, _birdPosition, 3);
                    explosion.show();
                    Components.Add(explosion);

                    _birdPosition = new Vector2(100, 200);
                    _birdVelocity = new Vector2(2, 0);
                    break;
                }
            }

            _previousKeyState = _currentKeyState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (_spriteBatch == null)
            {
                Console.WriteLine("SpriteBatch is null in Draw. Ensure LoadContent is called.");
                return;
            }

            _spriteBatch.Begin();
            if (_birdTexture == null)
            {
                LoadContent();
            }

            if (!_isCollision)
            {
                _spriteBatch.Draw(_birdTexture, _birdPosition, Color.White);
            }

            foreach (var pipePosition in _pipePositions)
            {
                _spriteBatch.Draw(_pipeTexture, pipePosition, Color.White);
            }

            //_spriteBatch.DrawString(_font, $"Score: {_score}", new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(_font, $"Lives: {_lives}", new Vector2(10, 30), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
            Update(gameTime);
        }

        private bool IsKeyPressed(Keys key)
        {
            return _currentKeyState.IsKeyDown(key) && !_previousKeyState.IsKeyDown(key);
        }

        //check collision method
        private bool CheckCollision(Vector2 pipePosition)
        {
            return _birdPosition.X < pipePosition.X + _pipeTexture.Width &&
                   _birdPosition.X + _birdTexture.Width > pipePosition.X &&
                   _birdPosition.Y < pipePosition.Y + _pipeTexture.Height &&
                   _birdPosition.Y + _birdTexture.Height > pipePosition.Y;
        }
    }
}
