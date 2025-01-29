using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

///Summary for FinalProject: FlappyBird, by Ashley John (studentNum) and Maria Pop (8416653)
///Thursday, December 5th: finalized decision to rebuild flappy bird game for our final project and started brainstorming 
///got starter code from you (Mrs. Maiti) that had initial code with the jumping bird velocity as well as the static pole
///got velocity to work and worked on the collision animation 
///Friday, December 6th: got the collision explosion animation to work properly when it ran into the pole
///Saturday, December 7th: worked on trying to get the menu working with the help of chatGPT but we kept getting an error about the texture of the bird being null; couldn't figure it out so I went back to the original code without the menu logic and restarted; this time started by using the same classes as your AllInOne from Week 11
///We first copied and pasted the main logic from the AllInOne code and then changed it to be speicifc to our code and then took a break until Monday because of work and personal commitments
///Monday, December 9th: Tried changing the logic for succeeding LevelOneScene to go to EndScene but it kept giving me a null exception
///Tuesday, December 10th: Had a lot of trouble fixing a null exception, Vraj helped me fix it, in the end it was because of LoadContent being loaded before or after Draw/Update I think?? We had to call Update, Load and Draw in each other for it to stop giving null exceptions 
///After that we worked together to create the other scenes like HelpScene and ActionScene and LevelTwoScene, and made the lives left as our score
///Final notes/statements
///A LOT of this code is made by ChatGPT (Majority of it is) BUT we did use the same logic over again and coded it ourselves, mostly for repetitive things like the levels and other stuff
///Whenever we ran into errors we asked ChatGPT to help us and while it didn't always work, it usually did
///PS. Thank you again for the extension, we are so so grateful. You're very kind and understanding we hope that you achieve everything you want in the future <3. 

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        public StartScene startScene;
        public EndScene endScene;
        private HelpScene helpScene;
        private AboutScene aboutScene;
        private LevelOneScene levelOneScene;
        private LevelTwoScene levelTwoScene; // Add Level Two Scene

        private Song backgroundMusic;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load your music file
            backgroundMusic = Content.Load<Song>("audio/backgroundMusic");

            // Play the music
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true; // Loop the music

            startScene = new StartScene(this);
            this.Components.Add(startScene);

            // Initialize scenes
            levelOneScene = new LevelOneScene(this, _spriteBatch);
            levelTwoScene = new LevelTwoScene(this, _spriteBatch);

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            startScene.show();
        }

        //scene logic mostly
        protected override void Update(GameTime gameTime)
        {
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();

            // Start Scene Logic
            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter)) // Start Level One
                {
                    startScene.hide();
                    levelOneScene.show();
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter)) // Help Scene
                {
                    startScene.hide();
                    aboutScene.show();
                }
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter)) // About Scene
                {
                    startScene.hide();
                    helpScene.show();
                }
                else if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter)) // Exit Game
                {
                    Exit();
                }
            }

            // Level One Logic
            if (levelOneScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape)) // Return to Start Menu
                {
                    levelOneScene.hide();
                    startScene.show();
                }

                if (levelOneScene.Completed) // Transition to Level Two
                {
                    levelOneScene.hide();
                    levelTwoScene.show();
                }

                // When transitioning to EndScene, pass remaining lives
                if (levelOneScene.PlayerOutOfLives)
                {
                    levelOneScene.hide();
                    endScene = new EndScene(this, levelOneScene.LivesRemaining); // Pass lives
                    this.Components.Add(endScene); // Add dynamically created scene
                    endScene.show();
                }
            }

            // Level Two Logic
            if (levelTwoScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape)) // Return to Start Menu
                {
                    levelTwoScene.hide();
                    startScene.show();
                }

                if (levelTwoScene.Completed) // Transition to End Scene
                {
                    levelTwoScene.hide();
                    endScene = new EndScene(this, levelTwoScene.LivesRemaining); // Pass lives
                    this.Components.Add(endScene); // Add dynamically created scene
                    endScene.show();
                }
            }

            // Help Scene Logic
            if (helpScene.Enabled && ks.IsKeyDown(Keys.Escape))
            {
                helpScene.hide();
                startScene.show();
            }

            if (aboutScene.Enabled && ks.IsKeyDown(Keys.Escape))
            {
                aboutScene.hide();
                startScene.show();
            }

            // End Scene Logic
            if (endScene != null && endScene.Enabled)
            {
                selectedIndex = endScene.Menu.SelectedIndex;

                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter)) // Next Level
                {
                    endScene.hide();
                    levelTwoScene.show();  // Transition to Level Two
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter)) // Back to Menu
                {
                    endScene.hide();
                    startScene.show(); // Show the Start Scene
                }
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter)) // Exit Game
                {
                    Exit();
                }
            }

            // Pause Music Logic
            if (ks.IsKeyDown(Keys.P))
            {
                if (MediaPlayer.State == MediaState.Playing)
                    MediaPlayer.Pause();
                else if (MediaPlayer.State == MediaState.Paused)
                    MediaPlayer.Resume();
            }

            base.Update(gameTime);
        }

        //draws each scene that is called
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (startScene.Enabled)
            {
                startScene.Draw(gameTime);
            }
            else if (levelOneScene.Enabled)
            {
                levelOneScene.Draw(gameTime);
            }
            else if (levelTwoScene.Enabled) // Draw Level Two Scene
            {
                levelTwoScene.Draw(gameTime);
            }
            else if (helpScene.Enabled)
            {
                helpScene.Draw(gameTime);
            }
            else if (aboutScene.Enabled)
            {
                aboutScene.Draw(gameTime);
            }
            else if (endScene != null && endScene.Enabled)
            {
                endScene.Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}
