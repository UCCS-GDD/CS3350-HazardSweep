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
    public class Enemy : Sprite
    {
        //class variables
        protected int health;
        protected Vector2 target;
        protected float moveSpeed = 1;
        protected int damage = 5;
        protected int currentTime;
        protected int delay = 30;
        protected int spriteRows, spriteCols;
        protected Rectangle sRec;
        protected Facing direction;

        //class constructor
        public Enemy(Game game, string textureFile, Vector2 position, int spriteRows, int spriteCols)
            : base(game, textureFile, position)
        {
            health = 2;

            this.spriteRows = spriteRows;
            this.spriteCols = spriteCols;

            direction = Facing.Right;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            sRec = new Rectangle(0, 0, texture.Width / spriteCols, texture.Height / spriteRows);

            //set the size and initial position of the bounding box
            boundingBox.Height = texture.Height / spriteRows;
            boundingBox.Width = texture.Width / spriteCols;
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;

            direction = Facing.Right;
        }

        //update method
        public override void Update(GameTime gameTime)
        {
            //logic for animation
            if (this.direction == Facing.Left)
            {
                sRec.X = 0;
                sRec.Y = texture.Height / spriteRows;
            }
            else if (this.direction == Facing.Right)
            {
                sRec.X = 0;
                sRec.Y = 0;
            }

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

                    if(position.X > (s.getPlayerPosition().X - 5f))
                    {
                        this.direction = Facing.Left;
                    }
                    else
                    {
                        this.direction = Facing.Right;
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

        //draw method
        public override void Draw(GameTime gameTime)
        {
            sb = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            sb.Begin();
            // sb.Draw(texture, position, drawRectangle, Color.White);
            sb.Draw(texture, position, sRec, Color.White, 0f, new Vector2(0f, 0f), new Vector2(2f, 2f), SpriteEffects.None, 0.5f);
            sb.End();
        }
    }
}
