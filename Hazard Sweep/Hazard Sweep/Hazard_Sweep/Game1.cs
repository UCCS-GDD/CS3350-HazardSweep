using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Hazard_Sweep.Classes;

namespace Hazard_Sweep
{
    public enum Facing { Left, Right};
    public enum WeaponType { Melee, Pistol, AssaultRifle, Shotgun };

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PlayerSprite player;
        GameElements elements;
        Camera2D camera;
        Room room0;

        //Splash screen
        public enum GameState { START, PLAY, PAUSE, END };
        SplashScreen splashScreen;
        GameState currentGameState = GameState.START;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 576;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Adds screen size
            GlobalClass.ScreenWidth = graphics.PreferredBackBufferWidth;
            GlobalClass.ScreenHeight = graphics.PreferredBackBufferHeight;

            //add rooms to game
            Components.Add(room0 = new Room(this, "Images//Maps//External//test02", new Vector2(100, 100), 1, 1, false, false));

            //Add game components
            Components.Add(player = new PlayerSprite(this, "Images//playerWalk", new Vector2(GlobalClass.ScreenWidth / 2,
                GlobalClass.ScreenHeight / 2), 2, 6));
            Components.Add(new Enemy(this, "Images//enemyWalk", new Vector2(200, 100), 2, 5));
            elements = new GameElements(this, player);
            elements.Initialize();
            camera = new Camera2D(GraphicsDevice.Viewport);

            //Splashscreen component
            splashScreen = new SplashScreen(this);
            Components.Add(splashScreen);
            splashScreen.SetData("HAZARD SWEEP", currentGameState);

            // what is this for?
            Random rand = new Random();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Creates a service for the spritebatch so it can be used in other classes
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            // load game elements
            elements.LoadContent();

            //testExMap.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (currentGameState == GameState.PLAY)
            {
                // update game elements
                elements.Update(gameTime);

                //update camera
                camera.Update();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (currentGameState == GameState.PLAY)
            {
                // draw objects
                spriteBatch.Begin();
                //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null,
                //    null, null, camera.Transform);
                //testExMap.Draw(spriteBatch);
                elements.Draw(spriteBatch);

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        public void ChangeGameState(GameState state)
        {
            currentGameState = state;

            switch (currentGameState)
            {
                case GameState.PLAY:
                    splashScreen.Enabled = false;
                    splashScreen.Visible = false;
                    break;
                case GameState.PAUSE:
                    splashScreen.SetData("PAUSED!", GameState.PAUSE);
                    splashScreen.Enabled = true;
                    splashScreen.Visible = true;
                    break;
                case GameState.END:
                    splashScreen.SetData("You have been devoured by the horde!", GameState.END);
                    splashScreen.Enabled = true;
                    splashScreen.Visible = true;
                    break;
            }
        }

        public GameState GetGameState()
        {
            return currentGameState;
        }
    }
}
