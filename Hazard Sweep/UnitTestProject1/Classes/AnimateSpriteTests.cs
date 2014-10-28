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
namespace AnimateSprite.Tests
{
    [TestClass()]
    public class AnimateSpriteTests
    {
        protected int frameCount = 1;
        protected int currentFrame;
        protected Rectangle frame = new Rectangle();
        protected Game dumbGame = null;
        protected string textureFile = "test";
        protected Vector2 testPosition = Vector2.One;

        [TestMethod()]
        public void AnimateSpriteTest()
        {
            Game game = null;
            Assert.AreEqual(dumbGame, game);
            Assert.AreEqual("test", textureFile);
            Assert.AreEqual(Vector2.One, testPosition);
            Assert.AreEqual(1, frameCount);
        }

        [TestMethod()]
        public void InitializeTest()
        {
            currentFrame = 0;

            Assert.AreEqual(0, currentFrame);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            currentFrame++;
            Assert.AreEqual(1, currentFrame);
        }

        [TestMethod()]
        public void DrawTest()
        {
        }
    }
}
