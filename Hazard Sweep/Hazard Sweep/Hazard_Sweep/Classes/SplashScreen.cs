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
    public class SplashScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        string mainText;
        string secondaryText;
        string iconTextL;
        string iconTextR;
        SpriteFont mainSpriteFont;
        SpriteFont secondarySpriteFont;
        SpriteFont iconLSpriteFont;
        SpriteFont iconRSpriteFont;
        SpriteBatch spriteBatch;
        Game1.GameState currentGameState;

        public SplashScreen(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        protected override void LoadContent()
        {
            //Load fonts
            mainSpriteFont = Game.Content.Load<SpriteFont>(@"Fonts\28DaysLater_72");
            secondarySpriteFont = Game.Content.Load<SpriteFont>(@"Fonts\VtksMoney_30");
            iconLSpriteFont = Game.Content.Load<SpriteFont>(@"Fonts\Hazard_72");
            iconRSpriteFont = Game.Content.Load<SpriteFont>(@"Fonts\Hazard_72");

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
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                //If we're not in end game, move to play state
                if (currentGameState == Game1.GameState.START || currentGameState == Game1.GameState.PAUSE)
                {
                    ((Game1)Game).ChangeGameState(Game1.GameState.PLAY);
                }
                else if (currentGameState == Game1.GameState.END)
                {
                    Game.Exit();
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            //Get size of string
            Vector2 TitleSize = mainSpriteFont.MeasureString(mainText);

            //Draw main text
            spriteBatch.DrawString(mainSpriteFont, mainText, new Vector2(Game.Window.ClientBounds.Width / 2 - TitleSize.X / 2,
                Game.Window.ClientBounds.Height / 2), Color.White);

            //Draw sub text
            spriteBatch.DrawString(secondarySpriteFont, secondaryText, new Vector2(Game.Window.ClientBounds.Width / 2 -
                secondarySpriteFont.MeasureString(secondaryText).X / 2, Game.Window.ClientBounds.Height / 2 + TitleSize.Y + 10), Color.DarkRed);

            //Draw icon text
            spriteBatch.DrawString(iconRSpriteFont, iconTextR, new Vector2(Game.Window.ClientBounds.Width / 2 + 300,
                Game.Window.ClientBounds.Height / 2 - 10), Color.Yellow);
            spriteBatch.DrawString(iconLSpriteFont, iconTextL, new Vector2(Game.Window.ClientBounds.Width / 2 - 375,
                Game.Window.ClientBounds.Height / 2 - 10), Color.Yellow);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void SetData(string main, Game1.GameState currGameState)
        {
            mainText = main;
            iconTextR = "#";
            iconTextL = "#";
            this.currentGameState = currGameState;

            switch (currentGameState)
            {
                case Game1.GameState.START:
                    secondaryText = "press ENTER to begin";
                    break;
                case Game1.GameState.PAUSE:
                    secondaryText = "press ENTER to resume";
                    break;
                case Game1.GameState.END:
                    secondaryText = "press Enter to quit";
                    break;
            }
        }
    }
}
