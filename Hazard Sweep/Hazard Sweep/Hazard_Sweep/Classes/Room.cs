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
    class Room : Sprite
    {
        public Rectangle Boundary;
        private Rectangle drawRectangle;
        private int row;
        private int column;
        private bool up;
        private bool down;
        private List<Door> doors;
        private int roomWidth = (int)GlobalClass.ScreenWidth;
        private int roomHeight = (int)GlobalClass.ScreenHeight;

        Vector2 boundaryPosition;

        //door positions
        Vector2 topDoorPos;
        Vector2 leftDoorPos;
        Vector2 rightDoorPos;
        Vector2 bottomDoorPos;

        //door teleport locations
        Vector2 topTeleport;
        Vector2 leftTeleport;
        Vector2 rightTeleport;
        Vector2 bottomTeleport;
        float offset = 30.0f;

        //door activations
        bool topActive = false;
        bool leftActive = false;
        bool rightActive = false;
        bool bottomActive = false;

        //map variables


        public Room(Game game, string textureFile, Vector2 position, int row, int column, bool up, bool down)
            : base(game, textureFile, position)
        {            
            this.row = row;
            this.column = column;
            this.up = up;
            this.down = down;
            
            boundaryPosition = new Vector2(10, 10);
            doors = new List<Door>();
        }

        public override void Initialize()
        {
            Boundary = new Rectangle((int)boundaryPosition.X, (int)boundaryPosition.Y, roomWidth - 110, roomHeight - 170);
            base.Initialize();

            topDoorPos = new Vector2(boundaryPosition.X + (Boundary.Width * 5 / 8), boundaryPosition.Y + Boundary.Height * 3 / 8 );
            leftDoorPos = new Vector2(boundaryPosition.X - 50, boundaryPosition.Y + (Boundary.Height * 7 / 8));
            rightDoorPos = new Vector2(boundaryPosition.X + Boundary.Width, boundaryPosition.Y + (Boundary.Height * 7 / 8));
            bottomDoorPos = new Vector2(boundaryPosition.X + (Boundary.Width * 5 / 8), boundaryPosition.Y + Boundary.Height + 50);

            topTeleport = new Vector2(bottomDoorPos.X, bottomDoorPos.Y - offset);
            leftTeleport = new Vector2(rightDoorPos.X - offset, rightDoorPos.Y);
            rightTeleport = new Vector2(leftDoorPos.X + offset, leftDoorPos.Y);
            bottomTeleport = new Vector2(topDoorPos.X, topDoorPos.Y + offset);

            game.Components.Add(new Door(game, "Images//door", topDoorPos, topTeleport, true));
            game.Components.Add(new Door(game, "Images//door", leftDoorPos, leftTeleport, true));
            game.Components.Add(new Door(game, "Images//door", rightDoorPos, rightTeleport, true));
            game.Components.Add(new Door(game, "Images//door", bottomDoorPos, bottomTeleport, true));

            drawRectangle = new Rectangle(0, 0, (int)GlobalClass.ScreenWidth, (int)GlobalClass.ScreenHeight);
        }

        public override void Update(GameTime gameTime)
        {
            if (((Game1)Game).GetGameState() == Game1.GameState.PLAY)
            {



                base.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (((Game1)Game).GetGameState() == Game1.GameState.PLAY)
            {
                sb = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
                sb.Begin();
                sb.Draw(texture, drawRectangle, null, color, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1.0f);
                sb.End();
            }            
        }

    }
}
