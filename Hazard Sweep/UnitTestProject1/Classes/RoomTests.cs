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
using System.Threading.Tasks;
using Hazard_Sweep.Classes;
using Hazard_Sweep;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace RoomTest.Tests
{
    [TestClass()]
    public class RoomTests
    {

        public Rectangle Boundary;
        private Rectangle drawRectangle;
        private int row;
        private int column;
        private bool up;
        private bool down;
       // private List<Door> doors;
        private int roomWidth = (int)GlobalClass.ScreenWidth;
        private int roomHeight = (int)GlobalClass.ScreenHeight;
        private Random rand = new Random();
        private const int maxZombie = 5;

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

        //GameElements elements;
        Color r_color;
        int id;

        [TestMethod()]
        public void RoomTest()
        {
            Game game = null;
            string textureFile = "test";
            Vector2 position = Vector2.Zero;
            int row = 0;
            int column = 0;
            bool up = false;
            bool down = false;
            Color color = Color.AliceBlue;
            int id = 0;
            Assert.AreEqual(null, game);
            Assert.AreEqual("test", textureFile);
            Assert.AreEqual(Vector2.Zero, position);
            Assert.AreEqual(0, row);
            Assert.AreEqual(0, column);
            Assert.AreEqual(false, up);
            Assert.AreEqual(false, down);
            Assert.AreEqual(Color.AliceBlue, color);
            Assert.AreEqual(0, id);
        }

        [TestMethod()]
        public void InitializeTest()
        {
            Game game = null;
            Boundary = new Rectangle((int)boundaryPosition.X, (int)boundaryPosition.Y + 150, roomWidth - 110, roomHeight - 270);
            Assert.AreEqual(new Rectangle((int)boundaryPosition.X, (int)boundaryPosition.Y + 150, roomWidth - 110, roomHeight - 270), Boundary);

            topDoorPos = new Vector2(boundaryPosition.X + (Boundary.Width * 5 / 8), boundaryPosition.Y + Boundary.Height * 3 / 8);
            leftDoorPos = new Vector2(boundaryPosition.X - 20, boundaryPosition.Y + (Boundary.Height * 7 / 8) + 70);
            rightDoorPos = new Vector2(boundaryPosition.X + Boundary.Width, boundaryPosition.Y + (Boundary.Height * 7 / 8) + 70);
            bottomDoorPos = new Vector2(boundaryPosition.X + (Boundary.Width * 5 / 8), boundaryPosition.Y + Boundary.Height + 200);
            buildingDoorPos = new Vector2(120f, 75f);

            topWallPos = new Vector2((topDoorPos.X - 170f), (topDoorPos.Y - 60f));
            leftWallPos = new Vector2((leftDoorPos.X - 10f), (leftDoorPos.Y - 110f));
            rightWallPos = new Vector2((rightDoorPos.X + 40f), (rightDoorPos.Y - 190f));
            bottomWallPos = new Vector2((bottomDoorPos.X - 270f), (bottomDoorPos.Y - 100f));

            topTeleport = new Vector2(bottomDoorPos.X, bottomDoorPos.Y - offset);
            leftTeleport = new Vector2(rightDoorPos.X - offset, rightDoorPos.Y);
            rightTeleport = new Vector2(leftDoorPos.X + offset, leftDoorPos.Y);
            bottomTeleport = new Vector2(topDoorPos.X, topDoorPos.Y + offset);
            buildingTeleport = new Vector2(buildingDoorPos.X, buildingDoorPos.Y + 128);

            int zombieNum = 1;

            for (int i = 0; i <= zombieNum; i++)
            {
                Rectangle boundingBox = new Rectangle(1, 1, 1, 1);
                int xLoc = boundingBox.Width;
                Assert.AreEqual(1, xLoc);
                int yLoc = boundingBox.Height;
                Assert.AreEqual(1, yLoc);
            }

            drawRectangle = new Rectangle(0, 0, (int)GlobalClass.ScreenWidth, (int)GlobalClass.ScreenHeight);
            Assert.AreEqual(new Rectangle(0, 0, (int)GlobalClass.ScreenWidth, (int)GlobalClass.ScreenHeight), drawRectangle);
        }
    }
}
