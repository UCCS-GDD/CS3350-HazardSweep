using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazard_Sweep.Classes.Maps.ExternalMaps
{
    class ExternalMap
    {
        protected Game game;
        protected Texture2D texture;
        protected string textureFile;
        protected Color color;
        protected Rectangle sRec;
        protected float x;

        public ExternalMap(Game game, string textureFile, float x, Color color)
        {
            this.game = game;
            this.textureFile = textureFile;
            this.color = color;
            this.x = x;
        }

        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>(textureFile);
            sRec = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, new Vector2(x,0f), sRec, color, 0f, new Vector2(0f,0f), new Vector2(2f,2f),SpriteEffects.None,1);
        }
    }
}
