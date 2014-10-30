using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Hazard_Sweep.Classes
{
    public class Sprite : DrawableGameComponent
    {
        protected String textureFile;
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 center;
        protected Color color;
        protected Random random;
        protected SpriteBatch sb;
        protected Game game;
        protected Rectangle boundingBox;

        //default constructor (mainly to make references easy)
        public Sprite(Game game) : base(game)
        {
        }

        //constructor with arguments
        public Sprite(Game game, String textureFile, Vector2 position)
            : base(game)
        {
            this.textureFile = textureFile;
            this.position = position;
            color = Color.White;
            random = new Random();
            this.game = game;
            this.DrawOrder = 2;
        }

        protected override void LoadContent()
        {
            texture = game.Content.Load<Texture2D>(textureFile);
            //center = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //set the size and initial position of the bounding box
            boundingBox.Height = texture.Height;
            boundingBox.Width = texture.Width;
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;

            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();

            //updates the bounding box for the sprite
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;

            //Allows the sprite to wrap around the screen
            //if (position.X < 0)
            //{
            //    position.X = GlobalClass.ScreenWidth;
            //}
            //else if (position.X > GlobalClass.ScreenWidth)
            //{
            //    position.X = 0;
            //}
            //else if (position.Y < 0)
            //{
            //    position.Y = GlobalClass.ScreenHeight;
            //}
            //else if (position.Y > GlobalClass.ScreenHeight)
            //{
            //    position.Y = 0;
            //}

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (((Game1)Game).GetGameState() == Game1.GameState.PLAY)
            {
                sb = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
                sb.Begin();
                //sb.Draw(texture, position, null, color, 0f, center, 1.0f, SpriteEffects.None,
                //    0f);
                sb.Draw(texture, position, color);
                sb.End();

                base.Draw(gameTime);
            }
        }

        //returns bounding box
        public Rectangle getRectangle()
        {
            return this.boundingBox;
        }
    }
}