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
        SpriteFont secondarySpriteFont;
        SpriteBatch spriteBatch;
        string credits = "Executive Producer-Eaven Sheets\nLead Game Designer-James Carlson\nLead Game Engineer-Tate Krejci\nLead Game Tester-Nick Kasza\nLead Game Artist-Alex Nissen";
        KeyboardState newState;
        KeyboardState lastState = Keyboard.GetState();
        string[] menuItemSelected = { "start", "how to play", "credits", "exit" };
        int itemSelected = 0;
        bool enterRelease = false, upRelease = false, downRelease = false;
        bool creditsVisible = false;
        Vector2 creditsPosition;
        Vector2 position;

        public MenuScreen(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        protected override void LoadContent()
        {
            //Load fonts
            mainSpriteFont = Game.Content.Load<SpriteFont>(@"Fonts\VtksMoney_30");
            secondarySpriteFont = Game.Content.Load<SpriteFont>(@"Fonts\Necro_14");

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
            if (((Game1)Game).GetGameState() == Game1.GameState.MENU)
            {
                newState = Keyboard.GetState();

                if (newState.IsKeyDown(Keys.Enter) && enterRelease)
                {
                    if (itemSelected == 0)
                        ((Game1)Game).ChangeGameState(Game1.GameState.PLAY);
                    if (itemSelected == 1)
                        ((Game1)Game).ChangeGameState(Game1.GameState.TUT);
                    if (itemSelected == 2)
                        creditsVisible = true;
                    if (itemSelected == 3)
                        ((Game1)Game).Exit();
                }

                if (newState.IsKeyUp(Keys.Enter))
                    enterRelease = true;

                if (newState.IsKeyDown(Keys.S) && downRelease || newState.IsKeyDown(Keys.Down) && downRelease)
                {
                    itemSelected++;
                    downRelease = false;
                }

                if (newState.IsKeyUp(Keys.S) && newState.IsKeyUp(Keys.Down))
                    downRelease = true;

                if (newState.IsKeyDown(Keys.W) && upRelease || newState.IsKeyDown(Keys.Up) && upRelease)
                {
                    itemSelected -= 1;
                    upRelease = false;
                }

                if (newState.IsKeyUp(Keys.W) && newState.IsKeyUp(Keys.Up))
                    upRelease = true;

                if (itemSelected == 4)
                    itemSelected = 0;

                if (itemSelected == -1)
                    itemSelected = 3;

                base.Update(gameTime);
            }

            if (creditsVisible == true)
            {
                creditsPosition.Y += 3;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            Color tint;

            Vector2 TitleSize = mainSpriteFont.MeasureString(credits);
            creditsPosition = new Vector2(Game.Window.ClientBounds.Width / 2 - TitleSize.X / 2 - 165,
                Game.Window.ClientBounds.Height / 2 + 200);
            position = new Vector2(Game.Window.ClientBounds.Width / 2 + 50,
                Game.Window.ClientBounds.Height / 2);

            if (creditsVisible == true)
            {
                spriteBatch.DrawString(secondarySpriteFont, credits, creditsPosition, Color.Yellow);
            }

            for (int i = 0; i < menuItemSelected.Length; i++)
            {
                if (i == itemSelected)
                    tint = Color.DarkRed;
                else
                    tint = Color.White;

                spriteBatch.DrawString(mainSpriteFont, menuItemSelected[i], position, tint);
                position.Y += 37;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
