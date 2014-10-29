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
    class Room : Sprite
    {
        public Rectangle Boundary;
        private Rectangle drawRectangle;
        private int row;
        private int column;
        private bool up;
        private bool down;

        public Room(Game game, string textureFile, Vector2 position, int row, int column, bool up, bool down)
            : base(game, textureFile, position)
        {            
            this.row = row;
            this.column = column;
            this.up = up;
            this.down = down;
        }

        public override void Initialize()
        {
            Boundary = new Rectangle(100, 100, 600, 600);
            base.Initialize();

            drawRectangle = new Rectangle(-100, 0, texture.Width, 600);
        }

        public override void Draw(GameTime gameTime)
        {
            if (((Game1)Game).GetGameState() == Game1.GameState.PLAY)
            {
                sb = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
                sb.Begin();
                sb.Draw(texture, drawRectangle, null, color, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1.0f);
                sb.End();
            }            
        }

    }
}
