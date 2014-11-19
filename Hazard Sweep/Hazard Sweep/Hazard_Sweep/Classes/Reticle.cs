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
    public class Reticle : Sprite
    {
        Game1 gameRef;
        public Vector2 readPosition;

        //public Reticle(Game game, String textureFile, Vector2 position, int drawOrder)
        public Reticle(Game game, String textureFile, Vector2 position, int drawOrder, Game1 gameRef)
            : base(game, textureFile, position, drawOrder)
        {
            this.gameRef = gameRef;   
        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            position.X = gameRef.GetCamera().center.X + ms.X;
            position.Y = ms.Y;
            readPosition = position;
            base.Update(gameTime);
        }

        //move reticle when player moves
        public void moveReticle(Vector2 amount)
        {
            position += amount;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, 
                new Rectangle((int)position.X - texture.Width/2, (int)position.Y - texture.Height/2, texture.Width, texture.Height), 
                Color.White);
            spriteBatch.End();
        }
    }    
}
