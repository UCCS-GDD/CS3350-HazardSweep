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

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PlayerSprite player;

        // health/ammo text assets
        Vector2 ammoLabelLocation;
        Vector2 ammoNumericLocation;
        Vector2 healthLabelLocation;
        Vector2 healthNumericLocation;
        string ammoText;
        string healthText;
        SpriteFont ammoLabelFont;
        SpriteFont ammoNumericFont;

        Camera camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            //Add game components
            Components.Add(player = new PlayerSprite(this, "Images//playerWalk", new Vector2(GlobalClass.ScreenWidth / 2,
                GlobalClass.ScreenHeight / 2), 2, 6));
            Components.Add(new Enemy(this, "Images//zombie", new Vector2(200, 100)));

            //testing
            //Components.Add(new Bullet(this, "Images//Animation", new Vector2(150, 150), Facing.Right));

            // health/ammo text locations
            ammoLabelLocation = new Vector2(48, GlobalClass.ScreenHeight - 96);
            ammoNumericLocation = new Vector2(32, GlobalClass.ScreenHeight - 64);
            healthLabelLocation = new Vector2(GlobalClass.ScreenWidth - 84, GlobalClass.ScreenHeight - 96);
            healthNumericLocation = new Vector2(GlobalClass.ScreenWidth - 96, GlobalClass.ScreenHeight - 64);
            ammoText = "-1/-1";
            healthText = "-1";

            Random rand = new Random();

            camera = new Camera(GraphicsDevice.Viewport);

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

            // load fonts
            ammoLabelFont = Content.Load<SpriteFont>("Fonts//AmmoLabel");
            ammoNumericFont = Content.Load<SpriteFont>("Fonts//AmmoNumeric");

            // TODO: use this.Content to load your game content here
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

            // TODO: Add your update logic here
            
            // update health/ammo text
            ammoText = player.GetWeapon().getClipBullets() + " / " + player.GetWeapon().getTotalNumBullets();
            healthText = "" + player.GetHealth();

            //update camera
            camera.Update(gameTime, player);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null,
                null, null, camera.transform);

            // draw score & font
            spriteBatch.DrawString(ammoLabelFont, "Ammo", ammoLabelLocation, Color.White);
            spriteBatch.DrawString(ammoNumericFont, ammoText, ammoNumericLocation, Color.White);
            spriteBatch.DrawString(ammoLabelFont, "Health", healthLabelLocation, Color.White);
            spriteBatch.DrawString(ammoNumericFont, healthText, healthNumericLocation, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
