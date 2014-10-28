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
        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTime, PlayerSprite sprite)
        {
            Vector2 playerPos = sprite.GetCenter();
            Rectangle playerRect = sprite.getRectangle();
            center = new Vector2((playerPos.X + playerRect.Width / 2) - GlobalClass.ScreenWidth,
                (playerPos.Y + playerRect.Height / 2) - GlobalClass.ScreenHeight);

            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }


    }
}

