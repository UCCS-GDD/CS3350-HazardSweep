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
namespace Bullet.Tests
{
    [TestClass()]
    public class BulletTests
    {
        protected int speed;
        protected Facing direction;
        protected bool remove = false;
        Game game = null;
        Vector2 position;

        [TestMethod()]
        public void BulletTest()
        {
            remove = false;
            Assert.AreEqual(false, remove);
            Vector2 position = Vector2.One;
            Assert.AreEqual(null, game);
            Facing dir = Facing.Left;
            direction = dir;
            Assert.AreEqual(Facing.Left, direction);
        }

       [TestMethod()]
       public void UpdateTest()
        {
           direction = Facing.Left;
            //logic for determining which direction the bullet should move
           position.X = 0;
            if (direction == Facing.Left)
            {
                position.X -= 10;
            }
            Assert.AreEqual(-10, position.X);
            direction = Facing.Right;
           position.X = 0;
            if (direction == Facing.Right)
            {
                position.X += 10;
            }
            Assert.AreEqual(10, position.X);
        }
    }
}
