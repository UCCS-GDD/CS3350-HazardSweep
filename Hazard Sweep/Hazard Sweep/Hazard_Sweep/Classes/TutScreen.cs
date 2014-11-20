﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Hazard_Sweep.Classes
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class TutScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        string mainText;
        SpriteFont mainSpriteFont;
        SpriteBatch spriteBatch;
        Texture2D tutTexture;

        public TutScreen(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        protected override void LoadContent()
        {
            //Load fonts
            mainSpriteFont = Game.Content.Load<SpriteFont>(@"Fonts\VtksMoney_30");
            tutTexture = Game.Content.Load<Texture2D>(@"Images\Instructions");

            //Create sprite batch
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            //Did the player press Enter?
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ((Game1)Game).ChangeGameState(Game1.GameState.MENU);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle sourceRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            mainText = "press escape to return";
            Vector2 TitleSize = mainSpriteFont.MeasureString(mainText);

            spriteBatch.Begin();

            //Draw main text
            spriteBatch.Draw(tutTexture, sourceRect, Color.White);
            spriteBatch.DrawString(mainSpriteFont, mainText, new Vector2(Game.Window.ClientBounds.Width / 2 - TitleSize.X / 2 + 100,
                Game.Window.ClientBounds.Height / 2 - 240), Color.DarkRed);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
