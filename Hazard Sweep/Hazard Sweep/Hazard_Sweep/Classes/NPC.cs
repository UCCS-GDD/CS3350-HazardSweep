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
    public enum Object { Scientist, Bomb, Cure, Helicopter };

    class NPC : Sprite
    {
        private Rectangle activationArea;
        protected Facing direction;
        public Object type;

        public NPC(Game game, string textureFile, Vector2 position, Facing direction)
            : base(game, textureFile, position, 9)
        {
            this.direction = direction;
            activationArea = new Rectangle((int)position.X - 50, (int)position.Y - 50, 175, 325);

            if (textureFile == "Images//scientist")
                type = Object.Scientist;
            if (textureFile == "Images//bomb")
                type = Object.Bomb;
            if (textureFile == "Images//Vial")
                type = Object.Cure;
            if (textureFile == "Images//helicopter")
                type = Object.Helicopter;
            //this.DrawOrder = 10;
        }

        //update method
        public override void Update(GameTime gameTime)
        {
            if (type == Object.Cure && ((Game1)Game).gameObj == Objective.Scientist2)
                ((Game1)Game).Components.Remove(this);
            if (type == Object.Scientist && ((Game1)Game).gameObj == Objective.Helicopter2)
                ((Game1)Game).Components.Remove(this);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (((Game1)Game).GetGameState() == Game1.GameState.PLAY)
            {
                sb = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
                //sb.Begin();
                if (direction == Facing.Left)
                {
                    if (type == Object.Scientist)
                        sb.Draw(texture, position, null, Color.White, 0f, new Vector2(0f, 0f), new Vector2(1f, 0.75f), SpriteEffects.FlipHorizontally, 0.5f);
                    else
                        sb.Draw(texture, position, null, Color.White, 0f, new Vector2(0f, 0f), new Vector2(2f, 2f), SpriteEffects.None, 0.5f);
                     
                }
                else
                {
                    if (type == Object.Scientist)
                        sb.Draw(texture, position, null, Color.White, 0f, new Vector2(0f, 0f), new Vector2(1f, 0.75f), SpriteEffects.None, 0.5f);
                }
                 //sb.End();
            }
        }

        //get activation area
        public Rectangle getActivationArea()
        {
            return activationArea;
        }
    }
}
