using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class AboutScene : GameScene
    {
        private SpriteBatch sb;
        private Texture2D backgroundTexture;
        private SpriteFont font;
        private string aboutText;

        public AboutScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            sb = g._spriteBatch;

            // Load resources
            backgroundTexture = game.Content.Load<Texture2D>("images/background");
            font = game.Content.Load<SpriteFont>("ScoreFont");

            aboutText = "Flappy Bird Game\n\n" +
                        "Creators: Ashley John and Maria Pop\n\n" +
                        "Purpose: " +
                        "This game was created as part of our final project for \n\n" +
                        "demonstrating game development using Microsoft XNA framework.\n\n" +
                        "Enjoy the game and try to achieve a high score!";
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            // Return to the Start Menu on ESC
            if (ks.IsKeyDown(Keys.Escape))
            {
                hide();
                ((Game1)Game).startScene.show();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw About text
            Vector2 position = new Vector2(50, 50); // Adjust position as needed
            sb.DrawString(font, aboutText, position, Color.Black);

            sb.End();

            base.Draw(gameTime);
        }
    }
}
