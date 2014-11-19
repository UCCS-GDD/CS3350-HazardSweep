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
            moveSpeed = 5;
        }
        protected override void AI()
        {
            //enemy AI (moves enemy towards player
            Vector2 direction = target - position;
            direction.Normalize();
            //enemy will only move towards player if the player is within 250
            if (Math.Abs(Vector2.Distance(target, position)) < 600)
            {
                Vector2 velocity = direction * moveSpeed;
                position += velocity;
            }
        }
    }    
}