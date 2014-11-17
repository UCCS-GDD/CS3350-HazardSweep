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
    class Boss : Enemy
    {
        //class constructor
        public Boss(Game game, string textureFile, Vector2 position, int spriteRows, int spriteCols)
            : base(game, textureFile, position, spriteRows, spriteCols)
        {
            health = 5;
        }
    }
}