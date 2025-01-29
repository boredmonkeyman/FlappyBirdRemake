using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class HelpScene : GameScene
    {
        private SpriteBatch sb;
        private Texture2D backgroundTexture;
        private SpriteFont font;
        private string helpText;

        public HelpScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            sb = g._spriteBatch;

            backgroundTexture = game.Content.Load<Texture2D>("images/background");
            font = game.Content.Load<SpriteFont>("ScoreFont");

            helpText = "How to Play:\n\n" +
                       "1. Press SPACE to make the bird jump.\n" +
                       "2. Avoid hitting the pipes or the ground.\n" +
                       "3. Pass through as many pipes as possible to score points.\n\n" +
                       "Controls:\n" +
                       "- SPACE: Jump\n" +
                       "- ESC: Return to Main Menu\n\n" +
                       "Good Luck!";
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

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

            Vector2 position = new Vector2(50, 50);
            sb.DrawString(font, helpText, position, Color.Black);

            sb.End();

            base.Draw(gameTime);
        }
    }
}
