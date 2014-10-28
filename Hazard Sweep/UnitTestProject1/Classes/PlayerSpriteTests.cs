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
using System.Threading.Tasks;
using Hazard_Sweep.Classes;
using Hazard_Sweep;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace PlayerSprite.Tests
{
    [TestClass()]
    public class PlayerSpriteTests
    {
        //variables
        protected Vector2 movement;
        protected MouseState ms;
        protected Vector2 mousePosition;
        protected Vector2 bulletOrigin;
        protected Facing direction;
        protected int health;
        protected Rectangle drawRectangle;
        protected int spriteRows, spriteCols;
        protected Game game = null;

        [TestMethod()]
        public void PlayerSpriteTest()
        {
            Assert.AreEqual(null, game);
            health = 100;
            Assert.AreEqual(100, health);

            //sets initial direction
            direction = Facing.Left;

            //set rows and columns
            spriteRows = 10;
            spriteCols = 10;

            Assert.AreEqual(Facing.Left, direction);
            Assert.AreEqual(10, spriteCols);
            Assert.AreEqual(10, spriteRows);
        }

        /*[TestMethod()]
        public void UpdateTest()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            ms = Mouse.GetState();

            //logic for animation
            if (direction == Facing.Left)
            {
                drawRectangle.X = 0;
                drawRectangle.Y = texture.Height / spriteRows;
            }
            else if (direction == Facing.Right)
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

            //ends the game if player's health is 0, will later go to menus
            if (health <= 0)
            {
                game.Exit();
            }
        }*/

        [TestMethod()]
        public void reducePlayerHealthTest()
        {
            health = 100;
            int damage = 10;
            health -= damage;
            Assert.AreEqual(90, health);
        }
    }
}
