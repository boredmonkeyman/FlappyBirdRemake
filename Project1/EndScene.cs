using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Project1
{
    public class EndScene : GameScene
    {
        private MenuComponent menu;
        public MenuComponent Menu { get => menu; set => menu = value; }
        private string lifeCounter;
        private SpriteFont font; // Font for displaying lives
        private int livesRemaining; // Lives remaining

        public EndScene(Game game, int lives) : base(game)
        {
            Game1 g = (Game1)game;

            string[] menuItems = { "Next Level", "Menu", "Quit" };

            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/RegularFont");
            SpriteFont hilightFont = g.Content.Load<SpriteFont>("fonts/HilightFont");
            font = g.Content.Load<SpriteFont>("ScoreFont"); // Load the font for lives

            Menu = new MenuComponent(game, g._spriteBatch, regularFont, hilightFont, menuItems);
            this.Components.Add(Menu);

            livesRemaining = lives; // Initialize lives remaining
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            var spriteBatch = ((Game1)Game)._spriteBatch;
            spriteBatch.Begin();

            // Display the number of lives remaining
            spriteBatch.DrawString(font, $"Lives Remaining: {livesRemaining}", new Vector2(10, 10), Color.White);

            spriteBatch.End();
        }
    }
}
