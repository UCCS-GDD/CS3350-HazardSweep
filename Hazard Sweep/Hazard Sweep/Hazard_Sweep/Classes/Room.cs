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
        private Rectangle drawRectangle1, drawRectangle2, drawRectangle3, 
            sourceRectangle1, sourceRectangle2, sourceRectangle3, roomRectangle;
        private int row;
        private int column;
        private bool up;
        private bool down;
        private List<Door> doors;
        private List<Sprite> sprites;
        private int roomWidth = 1049;
        private int roomHeight = 576;
        private const int maxZombie = 7;

        Vector2 boundaryPosition;

        public int leftBoundary, topBoundary, rightBoundary, bottomBoundary;

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

        //GameElements elements;
        PlayerSprite player;
        Color r_color;
        int id;

        public Room(Game game, string textureFile, Vector2 position, int row, int column, bool up, bool down, PlayerSprite player, Color r_color, int id)
            : base(game, textureFile, position, 1)
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
            Boundary = new Rectangle((int)boundaryPosition.X, (int)boundaryPosition.Y + 150, 800, 576);
            leftBoundary = (int)boundaryPosition.X;
            topBoundary = (int)boundaryPosition.Y + 15;
            rightBoundary = roomWidth - 110;
            bottomBoundary = roomHeight - 270;

            base.Initialize();

            // topDoorPos = new Vector2(boundaryPosition.X + (roomWidth * 5 / 8), boundaryPosition.Y + roomHeight * 3 / 8 );
            // leftDoorPos = new Vector2(boundaryPosition.X - 20, boundaryPosition.Y + (roomHeight * 7 / 8) + 70 );
            // rightDoorPos = new Vector2(boundaryPosition.X + roomWidth, boundaryPosition.Y + (roomHeight * 7 / 8) + 70);
            // bottomDoorPos = new Vector2(boundaryPosition.X + (roomWidth * 5 / 8), boundaryPosition.Y + roomHeight + 200);
            buildingDoorPos = new Vector2(300f, 84f);

            topDoorPos = new Vector2(1900, 200);
            leftDoorPos = new Vector2(15, 400);
            rightDoorPos = new Vector2(2350, 380);
            bottomDoorPos = new Vector2(1900, 500);

            topWallPos = new Vector2((topDoorPos.X - 150f), (topDoorPos.Y - 160f));
            leftWallPos = new Vector2((leftDoorPos.X - 20f), (leftDoorPos.Y - 110f));
            rightWallPos = new Vector2((rightDoorPos.X + 30f), (rightDoorPos.Y - 110f));
            bottomWallPos = new Vector2((bottomDoorPos.X - 150f), (bottomDoorPos.Y + 40f));

            topTeleport = new Vector2(bottomDoorPos.X, bottomDoorPos.Y - offset);
            leftTeleport = new Vector2(rightDoorPos.X - offset, rightDoorPos.Y);
            rightTeleport = new Vector2(leftDoorPos.X + offset, leftDoorPos.Y);
            bottomTeleport = new Vector2(topDoorPos.X, topDoorPos.Y + offset);
            buildingTeleport = new Vector2(buildingDoorPos.X, buildingDoorPos.Y + 128);

            switch (id)
            {
                case 0:
                    game.Components.Add(new Barricade(game, "Images//WallWide", topWallPos));
                    game.Components.Add(new Barricade(game, "Images//WallEnd", leftWallPos));
                    game.Components.Add(new Door(game, "Images//door", rightDoorPos, rightTeleport, true, 1));
                    game.Components.Add(new Door(game, "Images//door", bottomDoorPos, bottomTeleport, true, 3));
                    // figure something out to link interior to exterior
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 9));
                    break;
                case 1:
                    game.Components.Add(new Barricade(game, "Images//WallWide", topWallPos));
                    game.Components.Add(new Door(game, "Images//door", leftDoorPos, leftTeleport, true, 0));
                    game.Components.Add(new Door(game, "Images//door", rightDoorPos, rightTeleport, true, 2));
                    game.Components.Add(new Door(game, "Images//door", bottomDoorPos, bottomTeleport, true, 4));
                    // nope
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 10));
                    break;
                case 2:
                    game.Components.Add(new Barricade(game, "Images//WallWide", topWallPos));
                    game.Components.Add(new Barricade(game, "Images//WallEnd", rightWallPos));
                    game.Components.Add(new Door(game, "Images//door", leftDoorPos, leftTeleport, true, 1));
                    game.Components.Add(new Door(game, "Images//door", bottomDoorPos, bottomTeleport, true, 5));
                    // no
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 11));
                    break;
                case 3:
                    game.Components.Add(new Barricade(game, "Images//WallEnd", leftWallPos));
                    game.Components.Add(new Door(game, "Images//door", topDoorPos,topTeleport, true, 0));
                    game.Components.Add(new Door(game, "Images//door", rightDoorPos, rightTeleport, true, 4));
                    game.Components.Add(new Door(game, "Images//door", bottomDoorPos, bottomTeleport, true, 6));
                    // nah
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 12));
                    break;
                case 4:
                    game.Components.Add(new Door(game, "Images//door", topDoorPos,topTeleport, true, 1));
                    game.Components.Add(new Door(game, "Images//door", rightDoorPos, rightTeleport, true, 5));
                    game.Components.Add(new Door(game, "Images//door", leftDoorPos, leftTeleport, true, 3));
                    game.Components.Add(new Door(game, "Images//door", bottomDoorPos, bottomTeleport, true, 7));
                    // nuh-uh
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 13));
                    break;
                case 5:
                    game.Components.Add(new Barricade(game, "Images//WallEnd", rightWallPos));
                    game.Components.Add(new Door(game, "Images//door", leftDoorPos, leftTeleport, true, 4));
                    game.Components.Add(new Door(game, "Images//door", topDoorPos,topTeleport, true, 2));
                    game.Components.Add(new Door(game, "Images//door", bottomDoorPos, bottomTeleport, true, 8));
                    // fix it!
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 14));
                    break;
                case 6:
                    game.Components.Add(new Barricade(game, "Images//WallWide", bottomWallPos));
                    game.Components.Add(new Barricade(game, "Images//WallEnd", leftWallPos));
                    game.Components.Add(new Door(game, "Images//door", topDoorPos,topTeleport, true, 3));
                    game.Components.Add(new Door(game, "Images//door", rightDoorPos, rightTeleport, true, 7));
                    // stahp
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 15));
                    break;
                case 7:
                    game.Components.Add(new Barricade(game, "Images//WallWide", bottomWallPos));
                    game.Components.Add(new Door(game, "Images//door", topDoorPos,topTeleport, true, 4));
                    game.Components.Add(new Door(game, "Images//door", rightDoorPos, rightTeleport, true, 8));
                    game.Components.Add(new Door(game, "Images//door", leftDoorPos, leftTeleport, true, 6));
                    // no no
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 16));
                    break;
                case 8:
                    game.Components.Add(new Barricade(game, "Images//WallWide", bottomWallPos));
                    game.Components.Add(new Barricade(game, "Images//WallEnd", rightWallPos));
                    game.Components.Add(new Door(game, "Images//door", topDoorPos, topTeleport, true, 5));
                    game.Components.Add(new Door(game, "Images//door", leftDoorPos, leftTeleport, true, 7));
                    // your code is bad and you should feel bad
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 17));
                    break;




                // indoor
                case 9:
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 0));
                    break;
                case 10:
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 1));
                    break;
                case 11:
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 2));
                    break;
                case 12:
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 3));
                    break;
                case 13:
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 4));
                    break;
                case 14:
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 5));
                    break;
                case 15:
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 6));
                    break;
                case 16:
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 7));
                    break;
                case 17:
                    game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true, 8));
                    break;
            }

            SpawnObjective();

            if (((Game1)Game).GetObjRoom() != this.id)
            {
                //load in random zombie count
                //spawn bosses
                int bossChance = randomNumGen(0, 3);
                if (bossChance == 0)
                {
                    //int xLoc = rand.Next(boundingBox.Width);
                    int xLoc = Game1.Random.Next(200, 2210);
                    int yLoc = Game1.Random.Next(200, 400);
                    Vector2 location = new Vector2(xLoc, yLoc);

                    while (Vector2.Distance(location, player.getPosition()) < 700)
                    {
                        location.X = Game1.Random.Next(200, 2210);
                        location.Y = Game1.Random.Next(200, 400);
                    }
                    Enemy temp = new Boss(game, "Images//enemyWalk", location, 2, 5);
                    game.Components.Add(temp);
                }

                int zombieNum = randomNumGen(1, maxZombie);

                for (int i = 0; i <= zombieNum; i++)
                {
                    // int xLoc = rand.Next(boundingBox.Width);
                    int xLoc = Game1.Random.Next(200, 2210);
                    int yLoc = Game1.Random.Next(200, 400);
                    Vector2 location = new Vector2(xLoc, yLoc);

                    while (Vector2.Distance(location, player.getPosition()) < 500)
                    {
                        location.X = Game1.Random.Next(200, 2210);
                        location.Y = Game1.Random.Next(200, 400);
                    }
                    Enemy temp = new Enemy(game, "Images//enemyWalk", location, 2, 5);
                    game.Components.Add(temp);
                }
            }
            //game.Components.Add(new Door(game, "Images//door", topDoorPos, topTeleport, true));
            //game.Components.Add(new Door(game, "Images//door", leftDoorPos, leftTeleport, true));
            //game.Components.Add(new Door(game, "Images//door", rightDoorPos, rightTeleport, true));
            //game.Components.Add(new Door(game, "Images//door", bottomDoorPos, bottomTeleport, true));
            //game.Components.Add(new Door(game, "Images//DoorClosed", buildingDoorPos, buildingTeleport, true));

            drawRectangle1 = new Rectangle(0, 0, 804, 576);
            sourceRectangle1 = new Rectangle(0, 0, 402, 288);
            drawRectangle2 = new Rectangle(804, 0, 804, 576);
            sourceRectangle2 = new Rectangle(402, 0, 402, 288);
            drawRectangle3 = new Rectangle(1608, 0, 802, 576);
            sourceRectangle3 = new Rectangle(804, 0, 401, 288);
            //if (!game.Components.Contains(player))
            //{
            //    game.Components.Add(player);
            //}
            roomRectangle = new Rectangle(0, 0, roomWidth, roomHeight);
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
               // sb.Begin();
                sb.Draw(texture, drawRectangle1, sourceRectangle1, r_color, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1.0f);
                sb.Draw(texture, drawRectangle2, sourceRectangle2, r_color, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1.0f);
                sb.Draw(texture, drawRectangle3, sourceRectangle3, r_color, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1.0f);
               // sb.End();
            }            
        }

        public int GetID()
        {
            return id;
        }

        public Rectangle GetRoomRectangle()
        {
            return roomRectangle;
        }

        // spawn objective in randomly assigned room
        public void SpawnObjective()
        {
            NPC scientist = new NPC(game, "Images//scientist", new Vector2(1000, 300), Facing.Left);

            if (((Game1)Game).GetObjRoom() == this.id)
            {
                if (((Game1)Game).GetObjective() == Objective.Scientist)
                    game.Components.Add(scientist);
            }
        }
    }
}
