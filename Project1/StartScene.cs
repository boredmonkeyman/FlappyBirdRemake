using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class StartScene : GameScene
    {
        private MenuComponent menu;
        public MenuComponent Menu { get => menu; set => menu = value; }

        public StartScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;

            string[] menuItems = { "Start Game", "About", "Help", "Quit" };

            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/RegularFont");
            SpriteFont hilightFont = g.Content.Load<SpriteFont>("fonts/HilightFont");

            Menu = new MenuComponent(game, g._spriteBatch, regularFont, hilightFont, menuItems);
            this.Components.Add(Menu);
        }
    }
}
