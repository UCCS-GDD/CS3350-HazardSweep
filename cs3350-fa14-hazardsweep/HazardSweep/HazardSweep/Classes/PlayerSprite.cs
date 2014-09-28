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

namespace HazardSweep
{
    class PlayerSprite : Sprite
    {
        Vector2 movement;
        MouseState ms;
        Vector2 mousePosition;

        public PlayerSprite(Game game, string textureFile, Vector2 position)
            : base(game, textureFile, position)
        {

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            ms = Mouse.GetState();

            //move the sprite with WASD
            if (keyboardState.IsKeyDown(Keys.D))
            {
                position.X += 5;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                position.X -= 5;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                position.Y -= 5;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                position.Y += 5;
            }

            base.Update(gameTime);
        }
    }
}
