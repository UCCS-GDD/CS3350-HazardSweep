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
    class Camera
    {
        //class variables
        public Matrix transform;
        Viewport view;
        Vector2 center;

        // class constructor
        public Camera()
        {
            //view = newView;
        }

        public void Update(GameTime gameTime, PlayerSprite player)
        {
            Vector2 playerPos = player.getPosition();
            Rectangle playerRect = player.getRectangle();
            center = new Vector2((playerPos.X + playerRect.Width / 2) - (GlobalClass.ScreenWidth / 2),
                (playerPos.Y + playerRect.Height / 2) - (GlobalClass.ScreenHeight / 2));

            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }


    }
}

