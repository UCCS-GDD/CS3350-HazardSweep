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
        public Reticle(Game game, String textureFile, Vector2 position, int drawOrder)
            : base(game, textureFile, position, drawOrder)
        {

        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            position.X = ms.X;
            position.Y = ms.Y;
            base.Update(gameTime);
        }

        //move reticle when player moves
        public void moveReticle(Vector2 amount)
        {
            position += amount;
        }
    }    
}
