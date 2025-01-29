using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
    ///created by chatGPT to help make an animation for the level number
{
    public class ImageAnimation : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _image;
        private Vector2 _position;
        private float _timeElapsed;
        private float _duration;
        private bool _isFinished;

        public bool IsFinished => _isFinished;

        public ImageAnimation(Game game, SpriteBatch spriteBatch, string imageName, Vector2 position, float duration)
            : base(game)
        {
            _spriteBatch = spriteBatch;
            _image = game.Content.Load<Texture2D>(imageName);
            _position = position;
            _duration = duration;
            _timeElapsed = 0f;
            _isFinished = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (_isFinished) return;

            _timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timeElapsed >= _duration)
            {
                _isFinished = true;  // The animation finishes after the specified duration
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (_isFinished) return;

            _spriteBatch.Begin();
            _spriteBatch.Draw(_image, _position, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}
