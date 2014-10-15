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
    public class PlayerSprite : Sprite
    {
        //variables
        protected Vector2 movement;
        protected MouseState ms;
        protected Vector2 mousePosition;
        protected Vector2 bulletOrigin;
        protected Facing direction;
        protected int health;
        Weapon weapon;
        protected Rectangle drawRectangle;
        protected int spriteRows, spriteCols;

        //Constructor
        public PlayerSprite(Game game, string textureFile, Vector2 position, int spriteRows, int spriteCols)
            : base(game, textureFile, position)
        {
            health = 100;

            weapon = new Weapon(game, 60);

            //sets initial direction
            direction = Facing.Left;

            //set rows and columns
            this.spriteRows = spriteRows;
            this.spriteCols = spriteCols;

        }

        //load content
        protected override void LoadContent()
        {
            base.LoadContent();

            // sets up bullet origin vector has to be here so texture is loaded when looking at width and height
            bulletOrigin = new Vector2(texture.Width / spriteCols / 2, texture.Height / spriteRows / 2);
            game.Components.Add(weapon);
            drawRectangle = new Rectangle(0, 0, texture.Width / spriteCols, texture.Height / spriteRows);

            //set the size and initial position of the bounding box
            boundingBox.Height = texture.Height / spriteRows;
            boundingBox.Width = texture.Width / spriteCols;
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
        }

        // update method
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            ms = Mouse.GetState();            

            //logic for animation
            if(direction == Facing.Left)
            {
                drawRectangle.X = 0;
                drawRectangle.Y = texture.Height / spriteRows;
            }
            else if(direction == Facing.Right)
            {
                drawRectangle.X = 0;
                drawRectangle.Y = 0;
            }

            //move the sprite with WASD
            if (keyboardState.IsKeyDown(Keys.D))
            {
                position.X += 5;
                direction = Facing.Right;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                position.X -= 5;
                direction = Facing.Left;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                position.Y -= 5;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                position.Y += 5;
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                weapon.shoot(position + bulletOrigin, direction);
            }
            if (keyboardState.IsKeyDown(Keys.R))
            {
                weapon.reload();
            }

            //ends the game if player's health is 0, will later go to menus
            if (health <= 0)
            {
                game.Exit();
            }

            base.Update(gameTime);
        }

        //draw method
        public override void Draw(GameTime gameTime)
        {
            sb = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            sb.Begin();
            sb.Draw(texture, position, drawRectangle, Color.White);
            sb.End();
        }

        //returns the health
        public int GetHealth()
        {
            return health;
        }

        // returns player's weapon
        public Weapon GetWeapon()
        {
            return weapon;
        }

        //returns player's position
        public Vector2 getPlayerPosition()
        {
            return position;
        }

        //sets the player's health
        public void reducePlayerHealth(int damage)
        {
            health -= damage;
        }
    }
}
