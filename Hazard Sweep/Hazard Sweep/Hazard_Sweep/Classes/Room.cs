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
    public class Room : Sprite
    {
        public Rectangle Boundary;
        private Rectangle drawRectangle;
        private int row;
        private int column;
        private bool up;
        private bool down;
        private List<Door> doors;
        private List<Sprite> sprites;
        private int roomWidth = (int)GlobalClass.ScreenWidth;
        private int roomHeight = (int)GlobalClass.ScreenHeight;

        Vector2 boundaryPosition;

        //door positions
        Vector2 topDoorPos;
        Vector2 leftDoorPos;
        Vector2 rightDoorPos;
        Vector2 bottomDoorPos;
        Vector2 buildingDoorPos;
        Vector2 topWallPos;
        Vector2 leftWallPos;
        Vector2 rightWallPos;
        Vector2 bottomWallPos;

        //door teleport locations
        Vector2 topTeleport;
        Vector2 leftTeleport;
        Vector2 rightTeleport;
        Vector2 bottomTeleport;
        Vector2 buildingTeleport;
        float offset = 30.0f;

        //door activations
        bool topActive = false;
        bool leftActive = false;
        bool rightActive = false;
        bool bottomActive = false;

        //map variables

        GameElements elements;
        PlayerSprite player;
        Color r_color;
        int id;

        public Room(Game game, string textureFile, Vector2 position, int row, int column, bool up, bool down, PlayerSprite player, Color r_color, int id)
            : base(game, textureFile, position)
        {            
            this.row = row;
            this.column = column;
            this.up = up;
            this.down = down;
            
            boundaryPosition = new Vector2(10, 10);
            doors = new List<Door>();

            this.player = player;
            this.r_color = r_color;
            this.id = id;
            this.DrawOrder = 1;
        }

        public override void Initialize()
        {
            Boundary = new Rectangle((int)boundaryPosition.X, (int)boundaryPosition.Y + 150, roomWidth - 110, roomHeight - 270);
            base.Initialize();

            topDoorPos = new Vector2(boundaryPosition.X + (Boundary.Width * 5 / 8), boundaryPosition.Y + Boundary.Height * 3 / 8 );
            leftDoorPos = new Vector2(boundaryPosition.X - 50, boundaryPosition.Y + (Boundary.Height * 7 / 8) + 70 );
            rightDoorPos = new Vector2(boundaryPosition.X + Boundary.Width, boundaryPosition.Y + (Boundary.Height * 7 / 8) + 70);
            bottomDoorPos = new Vector2(boundaryPosition.X + (Boundary.Width * 5 / 8), boundaryPosition.Y + Boundary.Height + 200);
            buildingDoorPos = new Vector2(120f, 75f);

            topWallPos = new Vector2((topDoorPos.X - 170f), (topDoorPos.Y - 60f));
            leftWallPos = new Vector2((leftDoorPos.X - 10f), (leftDoorPos.Y - 110f));
            rightWallPos = new Vector2((rightDoorPos.X - 10f), (rightDoorPos.Y - 110f));
            bottomWallPos = new Vector2((bottomDoorPos.X - 170f), (bottomDoorPos.Y - 100f));

            topTeleport = new Vector2(bottomDoorPos.X, bottomDoorPos.Y - offset);
            leftTeleport = new Vector2(rightDoorPos.X - offset, rightDoorPos.Y);
            rightTeleport = new Vector2(leftDoorPos.X + offset, leftDoorPos.Y);
            bottomTeleport = new Vector2(topDoorPos.X, topDoorPos.Y + offset);
            buildingTeleport = new Vector2(buildingTeleport.X, buildingTeleport.Y);

            switch (id)
            {
                case 0:
                    game.Components.Add(new Barricade(game, "Images//WallWide", topWallPos));
                    game.Components.Add(new Barricade(game, "Images//WallEnd", leftWallPos));
                    game.Components.Add(new Door(game, "Images//door", rightDoorPos, rightTeleport, true, 1));
                    game.Components.Add(new Door(game, "Images//door", bottomDoorPos, bottomTeleport, true, 3));
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 9));
                    break;
                case 1:
                    game.Components.Add(new Barricade(game, "Images//WallWide", topWallPos));
                    game.Components.Add(new Barricade(game, "Images//WallEnd", rightWallPos));
                    game.Components.Add(new Door(game, "Images//door", leftDoorPos, leftTeleport, true, 0));
                    game.Components.Add(new Door(game, "Images//door", bottomDoorPos, bottomTeleport, true, 4));
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 10));
                    break;
                //case 2:
                case 3:
                    game.Components.Add(new Barricade(game, "Images//WallWide", bottomWallPos));
                    game.Components.Add(new Barricade(game, "Images//WallEnd", leftWallPos));
                    game.Components.Add(new Door(game, "Images//door", topDoorPos,topTeleport, true, 0));
                    game.Components.Add(new Door(game, "Images//door", rightDoorPos, rightTeleport, true, 4));
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 12));
                    break;
                case 4:
                    game.Components.Add(new Barricade(game, "Images//WallWide", topWallPos));
                    game.Components.Add(new Barricade(game, "Images//WallEnd", rightWallPos));
                    game.Components.Add(new Door(game, "Images//door", topDoorPos, topTeleport, true, 1));
                    game.Components.Add(new Door(game, "Images//door", leftDoorPos, leftTeleport, true, 3));
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 13));
                    break;
                // case 5,6,7,8
                case 9:
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 0));
                    break;
                case 10:
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 1));
                    break;
                // case 11:
                case 12:
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 3));
                    break;
                case 13:
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 4));
                    break;
                // case 14, 15, 16, 17
                
            }

            //game.Components.Add(new Door(game, "Images//door", topDoorPos, topTeleport, true));
            //game.Components.Add(new Door(game, "Images//door", leftDoorPos, leftTeleport, true));
            //game.Components.Add(new Door(game, "Images//door", rightDoorPos, rightTeleport, true));
            //game.Components.Add(new Door(game, "Images//door", bottomDoorPos, bottomTeleport, true));
            //game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true));

            drawRectangle = new Rectangle(0, 0, (int)GlobalClass.ScreenWidth, (int)GlobalClass.ScreenHeight);
            //if (!game.Components.Contains(player))
            //{
            //    game.Components.Add(player);
            //}

            elements = new GameElements(game, player);

            elements.Initialize();
            elements.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (((Game1)Game).GetGameState() == Game1.GameState.PLAY)
            {
                elements.Update(gameTime);


                base.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (((Game1)Game).GetGameState() == Game1.GameState.PLAY)
            {
                sb = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
                sb.Begin();
                sb.Draw(texture, drawRectangle, null, r_color, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1.0f);
                elements.Draw(sb);
                sb.End();
            }            
        }

        public int GetID()
        {
            return id;
        }

    }
}
