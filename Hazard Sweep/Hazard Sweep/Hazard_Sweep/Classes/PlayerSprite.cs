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

        //Constructor
        public PlayerSprite(Game game, string textureFile, Vector2 position)
            : base(game, textureFile, position)
        {
            health = 100;

            weapon = new Weapon(game, 60);

            //sets initial direction
            direction = Facing.Left;
        }

        //load content
        protected override void LoadContent()
        {
            base.LoadContent();

            // sets up bullet origin vector has to be here so texture is loaded when looking at width and height
            bulletOrigin = new Vector2(center.X + (texture.Width / 2), texture.Height / 2);
            game.Components.Add(weapon);
        }

        // update method
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            ms = Mouse.GetState();            

            //update firing position
            bulletOrigin += position;

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
                weapon.shoot(position, direction);
            }
            if (keyboardState.IsKeyDown(Keys.R))
            {
                weapon.reload();
            }

            base.Update(gameTime);
        }

        public int GetHealth()
        {
            return health;
        }

        public Weapon GetWeapon()
        {
            return weapon;
        }
    }
}
