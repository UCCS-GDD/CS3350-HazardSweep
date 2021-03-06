﻿using System;
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
            : base(game, textureFile, position, 9)
        {
            this.frameCount = frameCount;
        }

        public override void Initialize()
        {
            //set animation to start from beginning
            currentFrame = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //set frame rectangle
            base.LoadContent();
            frame = new Rectangle(0, 0, texture.Width / 2, texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

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
