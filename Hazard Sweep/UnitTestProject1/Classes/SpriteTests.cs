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
namespace Sprite.Tests
{
    [TestClass()]
    public class SpriteTests
    {
        protected String textureFile = "Test";
        protected Texture2D texture;
        protected Vector2 position = Vector2.One;
        protected Vector2 center = Vector2.One;
        protected Color color = Color.AliceBlue;
        protected Random random;
        protected SpriteBatch sb;
        protected Game game = null;
        protected Rectangle boundingBox;
        [TestMethod()]
        public void SpriteTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void SpriteTest1()
        {
            Game thisGame = game;
            Assert.AreEqual(thisGame, null);
            Assert.AreEqual("Test", textureFile);
            Assert.AreEqual(Vector2.One, center);
            position = position + center;
            Assert.AreEqual(Vector2.One + Vector2.One, position);
            Assert.AreEqual(Color.AliceBlue, color);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;

            Assert.AreEqual(Vector2.One.X, boundingBox.X);
            Assert.AreEqual(Vector2.One.Y, boundingBox.Y);
        }
    }
}
