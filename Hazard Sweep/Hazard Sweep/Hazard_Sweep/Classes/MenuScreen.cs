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
        protected MouseState ms;
        Game game;
        SpriteFont mainSpriteFont;
        SpriteFont secondarySpriteFont;
        SpriteBatch spriteBatch;
        string credits = "Executive Producer     Eaven Sheets\n\nLead Game Designer     James Carlson\n\nLead Game Engineer     Tate Krejci\n\nLead Game Tester        Nick Kasza\n\nLead Game Artist        Alex Nissen"
            + "\n\n\n\nSound Effects\n\nDownloaded from Soundbible.com\n\n\n\nPistol Firing                 GoodSoundForYou\n\nShotgun Firing              Soundeffects\n\nMachine Gun Firing         WEL\n\nShell Hitting Ground     Marcel"
            + "\n\nReload                          Mike Koenig\n\nZombies                         Nick Kasza\n                                    Stefanie Matthews";
        KeyboardState newState;
        KeyboardState lastState = Keyboard.GetState();
        string[] menuItemSelected = { "start", "how to play", "credits", "exit" };
        int itemSelected = 0;
        bool enterRelease = false, upRelease = false, downRelease = false;
        bool creditsVisible = false;
        Rectangle startRec, howRec, credRec, exitRec;
        Texture2D temp;
        Vector2 creditsPosition;
        int creditsMove = 0;
        Vector2 position;

        public MenuScreen(Game game, Texture2D tempNew)
            : base(game)
        {
            this.game = game;
            temp = tempNew;
            // TODO: Construct any child components here
        }

        protected override void LoadContent()
        {
            //Load fonts
            mainSpriteFont = Game.Content.Load<SpriteFont>(@"Fonts\VtksMoney_30");
            secondarySpriteFont = Game.Content.Load<SpriteFont>(@"Fonts\Necro_14");

            startRec = new Rectangle(Game.Window.ClientBounds.Width / 2 + 100,
                (Game.Window.ClientBounds.Height / 2), 150, 40);

            howRec = new Rectangle(Game.Window.ClientBounds.Width / 2 + 100,
                (Game.Window.ClientBounds.Height / 2) + 40, 150, 40);

            credRec = new Rectangle(Game.Window.ClientBounds.Width / 2 + 100,
                (Game.Window.ClientBounds.Height / 2) + 80, 150, 40);

            exitRec = new Rectangle(Game.Window.ClientBounds.Width / 2 + 100,
                (Game.Window.ClientBounds.Height / 2) + 120, 150, 40);

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
            ms = (game as Game1).GetMouseState();
            // TODO: Add your update code here
            //Did the player press Enter?
            if (((Game1)Game).GetGameState() == Game1.GameState.MENU)
            {
                newState = Keyboard.GetState();

                if (startRec.Contains(ms.X, ms.Y))
                {
                    itemSelected = 0;
                }
                else if (howRec.Contains(ms.X, ms.Y))
                {
                    itemSelected = 1;
                }
                else if (credRec.Contains(ms.X, ms.Y))
                {
                    itemSelected = 2;
                }
                else if (exitRec.Contains(ms.X, ms.Y))
                {
                    itemSelected = 3;
                }

                if ((newState.IsKeyDown(Keys.Enter) && enterRelease && ((Game1)Game).GetGameState() == Game1.GameState.MENU) || (ms.LeftButton == ButtonState.Pressed && ((Game1)Game).GetGameState() == Game1.GameState.MENU))
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
                creditsMove -= gameTime.ElapsedGameTime.Milliseconds / 8;

                if (creditsMove < -1000)
                {
                    creditsVisible = false;
                    creditsMove = 0;
                }
            }

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            Color tint;
            spriteBatch.Draw(temp, startRec, Color.White);
            spriteBatch.Draw(temp, howRec, Color.White);
            spriteBatch.Draw(temp, credRec, Color.White);
            spriteBatch.Draw(temp, exitRec, Color.White);

            Vector2 TitleSize = mainSpriteFont.MeasureString(credits);
            creditsPosition = new Vector2(Game.Window.ClientBounds.Width / 2 - TitleSize.X / 2 - 130,
                Game.Window.ClientBounds.Height / 2 + 300 + creditsMove);
            position = new Vector2(Game.Window.ClientBounds.Width / 2 + 100,
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
