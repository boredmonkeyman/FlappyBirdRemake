using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Project1
{
    public abstract class GameScene : DrawableGameComponent
    {
        // List to manage components
        public List<GameComponent> Components { get; set; }

        // Property to track if the scene is completed
        public bool Completed { get; set; }

        // Hide and show methods
        public virtual void hide()
        {
            this.Visible = false;
            this.Enabled = false;
            Completed = false; // Reset Completed when hiding
        }

        public virtual void show()
        {
            this.Visible = true;
            this.Enabled = true;
            Completed = false; // Reset Completed when showing
        }

        // Constructor
        protected GameScene(Game game) : base(game)
        {
            Components = new List<GameComponent>();
            hide();
        }

        // Update components
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        // Draw components
        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item is DrawableGameComponent comp && comp.Visible)
                {
                    comp.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        // Hook for derived classes when the scene is completed
        public virtual void OnLevelCompleted()
        {
            // Default: Can be overridden by specific levels
            Console.WriteLine($"{this.GetType().Name} completed!");
        }
    }
}
