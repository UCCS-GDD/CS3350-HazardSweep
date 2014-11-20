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
        protected Rectangle collisionRec;
        protected Facing direction;
        protected bool remove;

        // animation variables
        int frameCount;
        int animationSpeed = 10;
        float prevX;
        float prevY;
        bool animate;

        //variables for damage
        Color color;
        bool shouldColor;
        int colorTimer;

        Random dropRandom;

        //class constructor
        public Enemy(Game game, string textureFile, Vector2 position, int spriteRows, int spriteCols)
            : base(game, textureFile, position, 10)
        {
            health = 2;

            color = Color.White;
            shouldColor = false;
            colorTimer = 2;

            this.spriteRows = spriteRows;
            this.spriteCols = spriteCols;

            direction = Facing.Right;

            remove = false;

            //this.DrawOrder = 10;
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
            collisionRec.Height = texture.Height;
            collisionRec.Width = texture.Width / 3;
            collisionRec.X = boundingBox.X;
            collisionRec.Y = boundingBox.Y;

            direction = Facing.Right;
        }

        //update method
        public override void Update(GameTime gameTime)
        {
            if (((Game1)Game).GetGameState() == Game1.GameState.PLAY)
            {
                dropRandom = new Random();

                //logic for removal
                if (health <= 0)
                {
                    this.setRemove();
                    (game as Game1).playZombieDeath();
                    int r = dropRandom.Next(0, 5);
                    if (r == 0)
                    {
                        game.Components.Add(new itemDrop(game, null, position));
                    }
                }

                //player damage representation
                if (shouldColor)
                {
                    color = Color.Red;
                    colorTimer = 5;
                    shouldColor = false;
                }

                //reduce color timer
                colorTimer--;

                //reset the color
                if (colorTimer <= 0)
                {
                    color = Color.White;
                }

                //logic for animation
                if (this.direction == Facing.Left)
                {
                    sRec.Y = texture.Height / spriteRows + 1;
                }
                else if (this.direction == Facing.Right)
                {
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

                        if (position.X > (s.getPlayerPosition().X - 5f))
                        {
                            this.direction = Facing.Left;
                        }
                        else
                        {
                            this.direction = Facing.Right;
                        }
                    }
                }

                //movement
                AI();

                // animate

                // check for movement
                if (position.X == prevX && position.Y == prevY)
                {
                    animate = false;
                    frameCount = 0;
                }
                else
                {
                    animate = true;
                }

                // if enemy is moving, animate
                if (animate)
                {
                    if (frameCount == animationSpeed)
                    {
                        // check for rollover on sprite strip
                        if ((sRec.X + sRec.Width) > (texture.Width - sRec.Width))
                        {
                            sRec.X = 0;
                        }
                        else
                        {
                            sRec.X += sRec.Width;
                        }

                        // restart timer
                        frameCount = 0;
                    }
                }

                // increment animation assets as appropriate
                frameCount += 1;
                prevX = position.X;
                prevY = position.Y;
            }
            if (remove == true)
            {
                game.Components.Remove(this);
            }
            collisionRec.X = boundingBox.X;
            collisionRec.Y = boundingBox.Y;
        }

        //draw method
        public override void Draw(GameTime gameTime)
        {
            if (((Game1)Game).GetGameState() == Game1.GameState.PLAY)
            {
                sb = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
                sb.Draw(texture, position, sRec, color, 0f, new Vector2(0f, 0f), new Vector2(2f, 2f), SpriteEffects.None, 0.5f);
            }
        }

        public void setRemove()
        {
            (game as Game1).playZombieDeath();
            this.remove = true;
        }

        public void removeHelth(int amount)
        {
            health -= amount;
            (game as Game1).playZombieDamage();
        }

        //returns bounding box
        public Rectangle getRectangle()
        {
            return collisionRec;
        }

        //method for AI
        protected virtual void AI()
        {
            //enemy AI (moves enemy towards player
            Vector2 direction = target - position;
            direction.Normalize();
            //enemy will only move towards player if the player is within 300
            if (Math.Abs(Vector2.Distance(target, position)) < 300)
            {
                Vector2 velocity = direction * moveSpeed;
                position += velocity;
            }
        }

        public void setShouldColor()
        {
            shouldColor = true;
        }
    }
}
