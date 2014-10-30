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
    class NPC : Sprite
    {
        private Rectangle activationArea;
        protected Facing direction;

        public NPC(Game game, string textureFile, Vector2 position, Facing direction)
            : base(game, textureFile, position)
        {
            this.direction = direction;
            activationArea = new Rectangle((int)position.X - 50, (int)position.Y - 50, 175, 325);
            this.DrawOrder = 10;
        }

        //update method
        //public override void Update(GameTime gameTime)
        //{
        //    if (isActive)
        //        base.Update(gameTime);
        //    }
        //}

        public override void Draw(GameTime gameTime)
        {
            if (((Game1)Game).GetGameState() == Game1.GameState.PLAY)
            {
                sb = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
                sb.Begin();
                if(direction == Facing.Left)
                    sb.Draw(texture, position, null, Color.White, 0f, new Vector2(0f, 0f), new Vector2(1f, 0.75f), SpriteEffects.FlipHorizontally, 0.5f);
                else
                    sb.Draw(texture, position, null, Color.White, 0f, new Vector2(0f, 0f), new Vector2(1f, 0.75f), SpriteEffects.None, 0.5f);
                sb.End();
            }
        }

        //get activation area
        public Rectangle getActivationArea()
        {
            return activationArea;
        }
    }
}
