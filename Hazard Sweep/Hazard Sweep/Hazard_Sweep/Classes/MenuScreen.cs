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


namespace Hazard_Sweep.Classes
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MenuScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteFont mainSpriteFont;
        SpriteBatch spriteBatch;
        KeyboardState newState;
        KeyboardState lastState = Keyboard.GetState();
        string[] menuItemSelected = { "start", "how to play", "credits", "exit" };
        bool press, release = false;

        public MenuScreen(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        protected override void LoadContent()
        {
            //Load fonts
            mainSpriteFont = Game.Content.Load<SpriteFont>(@"Fonts\VtksMoney_30");

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
            newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Enter))
            {
                press = true;
            }
            if (newState.IsKeyUp(Keys.Enter) && press == true)
            {
                release = true;
            }

            if (newState.IsKeyDown(Keys.Enter) && (press == true) && (release == true))
            {
                ((Game1)Game).ChangeGameState(Game1.GameState.PLAY);
                release = false;
                press = false;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            Color tint;
            Vector2 position = new Vector2(Game.Window.ClientBounds.Width / 2 + 50,
                Game.Window.ClientBounds.Height / 2);

            for (int i = 0; i < menuItemSelected.Length; i++)
            {
                tint = Color.White;

                spriteBatch.DrawString(mainSpriteFont, menuItemSelected[i], position, tint);
                position.Y += 37;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
