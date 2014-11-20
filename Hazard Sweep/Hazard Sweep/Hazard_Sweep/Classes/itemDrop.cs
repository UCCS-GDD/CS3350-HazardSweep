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
    class itemDrop : Sprite
    {
        //class variables
        protected int r = 0;
        protected DropType type;
        protected int value;
        protected bool remove = false;

        //class constructor
        public itemDrop(Game game, string textureFile, Vector2 position)
            : base(game, null, position, 10)
        {
            r = randomNumGen(0, 4);
        }

        public override void Initialize()
        {
            switch(r)
            {
                case(0):
                    type = DropType.Health;
                    value = randomNumGen(5, 11);
                    texture = game.Content.Load<Texture2D>("Images//healthBox");
                    break;
                case(1):
                    type = DropType.AssaultAmmo;
                    value = randomNumGen(20, 51);
                    texture = game.Content.Load<Texture2D>("Images//ammoBoxRifle");
                    break;
                case(2):
                    type = DropType.PistolAmmo;
                    value = randomNumGen(10, 31);
                    texture = game.Content.Load<Texture2D>("Images//ammoBoxPistol");
                    break;
                case(3):
                    type = DropType.ShotgunAmmo;
                    value = randomNumGen(5, 16);
                    texture = game.Content.Load<Texture2D>("Images//ammoBoxShotgun");
                    break;
            }

            //set the size and initial position of the bounding box
            boundingBox.Height = texture.Height;
            boundingBox.Width = texture.Width;
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
        }

        //update method
        public override void Update(GameTime gameTime)
        {
            if(remove)
            {
                game.Components.Remove(this);
            }
            base.Update(gameTime);
        }

        

        //returns type
        public DropType getDropType()
        {
            return type;
        }

        //returns value
        public int getDropValue()
        {
            return value;
        }

        //sets remove variable
        public void removeItem()
        {
            remove = true;
        }
    }
}
