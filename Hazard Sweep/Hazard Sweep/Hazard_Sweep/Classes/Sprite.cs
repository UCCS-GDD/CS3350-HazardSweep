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
    public class Sprite : DrawableGameComponent
    {
        protected String textureFile;
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 center;
        protected Color color;
        protected SpriteBatch sb;
        protected Game game;
        protected Rectangle boundingBox;

        //default constructor (mainly to make references easy)
        public Sprite(Game game)
            : base(game)
        {
        }

        //constructor with arguments
        public Sprite(Game game, String textureFile, Vector2 position, int drawOrder)
            : base(game)
        {
            this.textureFile = textureFile;
            this.position = position;
            color = Color.White;
            this.game = game;
            this.DrawOrder = drawOrder;
        }

        protected override void LoadContent()
        {
            texture = game.Content.Load<Texture2D>(textureFile);
            //center = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //set the size and initial position of the bounding box
            boundingBox.Height = texture.Height;
            boundingBox.Width = texture.Width;
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;

            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();

            //updates the bounding box for the sprite
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (((Game1)Game).GetGameState() == Game1.GameState.PLAY)
            {
                sb = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
                sb.Draw(texture, position, color);

                base.Draw(gameTime);
            }
        }

        //returns bounding box
        public Rectangle getRectangle()
        {
            return this.boundingBox;
        }

        //returns the position
        public Vector2 getPosition()
        {
            return this.position;
        }

        //random number generator
        public int randomNumGen(int min, int max)
        {
            int value = Game1.Random.Next(min, max);
            return value;
        }
    }
}