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
    public class AnimateSprite : Sprite
    {
        protected int frameCount;
        protected int currentFrame;
        protected Rectangle frame;

        public AnimateSprite(Game game, string textureFile, Vector2 position, int frameCount)
            : base(game, textureFile, position)
        {
            this.frameCount = frameCount;
        }

        public override void Initialize()
        {
            currentFrame = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            frame = new Rectangle(0, 0, texture.Width / 2, texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //switches animation
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (currentFrame == 0)
                {
                    frame.X = (texture.Width / 2);
                    currentFrame = 1;
                }
                else if (currentFrame == 1)
                {
                    frame.X = 0;
                    currentFrame = 0;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            sb.Begin();
            sb.Draw(texture, position, frame, Color.White);
            sb.End();
        }
    }
}
