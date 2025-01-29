using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Explosion : DrawableGameComponent
    {
        private SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position;
        private int delay;

        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;

        private int delayCounter;

        private const int ROWS = 5;
        private const int COLS = 5;

        public Vector2 Position { get => position; set => position = value; }

        private Game g;

        public Explosion(Game game, SpriteBatch sb,
            Texture2D tex, Vector2 position, int delay) : base(game)
        {
            this.g = game;
            this.sb = sb;
            this.tex = tex;
            this.Position = position;
            this.delay = delay;
            this.dimension = new Vector2(tex.Width / COLS, tex.Height / ROWS);

            createFrames();
            hide();
        }

        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);

                    frames.Add(r);
                }
            }
        }

        public void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        public void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > ROWS * COLS - 1)
                {
                    frameIndex = -1;
                    hide();
                    g.Components.Remove(this);
                }

                delayCounter = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (frameIndex >= 0)
            {
                sb.Begin();
                //v 4
                sb.Draw(tex, Position, frames[frameIndex], Color.White);
                sb.End();
            }

            base.Draw(gameTime);
        }
    }
}
