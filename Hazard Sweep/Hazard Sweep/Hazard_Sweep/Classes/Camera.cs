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
        Game1 game;
        // class constructor
        public Camera(Game1 game)
        {
            //view = newView;
            this.game = game;
        }

        public void Update(GameTime gameTime, PlayerSprite player)
        {
            Vector2 playerPos = player.getPosition();
            Rectangle playerRect = player.getRectangle();
            center = new Vector2((playerPos.X + playerRect.Width / 2) - (GlobalClass.ScreenWidth / 2),
                (playerPos.Y + playerRect.Height / 2) - (GlobalClass.ScreenHeight / 2));
            
            // clamping
            // top
            if (center.Y < 0)
                center.Y = 0;
            // left
            if (center.X < 0)
                center.X = 0;
            // bottom
            if (center.Y + GlobalClass.ScreenHeight > game.GetRoom(game.GetRoomID()).GetDrawRectangle().Height)
                center.Y = game.GetRoom(game.GetRoomID()).GetDrawRectangle().Height - GlobalClass.ScreenHeight;
            // right
            if (center.X + GlobalClass.ScreenWidth > game.GetRoom(game.GetRoomID()).GetDrawRectangle().Width)
                // center.X = game.GetRoom(game.GetRoomID()).GetDrawRectangle().Width - GlobalClass.ScreenWidth;

            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }


    }
}