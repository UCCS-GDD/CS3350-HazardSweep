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
    class Door : Sprite
    {
        private Rectangle activationArea;
        private Vector2 exitLocation;

        public Door(Game game, string textureFile, Vector2 position, Vector2 exitLocation)
            : base(game, textureFile, position)
        {
            this.exitLocation = exitLocation;
            activationArea = new Rectangle((int)position.X - 300, (int)position.Y - 300, 600, 600);
        }

        //get activation area
        public Rectangle getActivationArea()
        {
            return activationArea;
        }

        //get exit location
        public Vector2 getExitLocation()
        {
            return exitLocation;
        }
    }
}
