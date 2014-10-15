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
    class Enemy : Sprite
    {
        //class variables
        protected int health;
        protected Vector2 target;
        protected float moveSpeed = 1;
        protected int damage = 5;
        protected int currentTime;
        protected int delay = 30;

        //class constructor
        public Enemy(Game game, string textureFile, Vector2 position)
            : base(game, textureFile, position)
        {
            health = 2;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            
        }

        //update method
        public override void Update(GameTime gameTime)
        {
            Console.WriteLine(boundingBox.X + " " + boundingBox.Y);
            base.Update(gameTime);

            //update current time
            currentTime++;

            //check for collisions with player
            foreach (GameComponent g in game.Components)
            {
                if (g is PlayerSprite)
                {
                    PlayerSprite s = (PlayerSprite)g;
                    //get position of player
                    target = s.getPlayerPosition();

                    //collision logic
                    Rectangle b = s.getRectangle();
                    if (b.Intersects(this.boundingBox))
                    {
                        //damage the player
                        if (currentTime >= delay)
                        {
                            s.reducePlayerHealth(damage);
                            currentTime = 0;
                        }
                    }
                }
            }

            //enemy AI (moves enemy towards player
            Vector2 direction = target - position;
            direction.Normalize();
            //enemy will only move towards player if the player is within 250
            if (Math.Abs(Vector2.Distance(target, position)) < 250)
            {
                Vector2 velocity = direction * moveSpeed;
                position += velocity;
            }

        }


    }
}
