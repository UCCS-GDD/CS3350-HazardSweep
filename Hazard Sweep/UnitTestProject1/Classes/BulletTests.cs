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
            Vector2 position = Vector2.One;
            Assert.AreEqual(null, game);
            Facing dir = Facing.Left;
            direction = dir;
            Assert.AreEqual(Facing.Left, direction);
        }

       /*  [TestMethod()]
       public void UpdateTest()
        {
            //logic for determining which direction the bullet should move
            if (direction == Facing.Left)
            {
                position.X -= 10;
            }
            else
            {
                position.X += 10;
            }

            Sprite collisionSprite = new Sprite(game);
            //check for collisions
            foreach (GameComponent g in game.Components)
            {
                if (g is Enemy)
                {
                    Sprite s = (Sprite)g;
                    Rectangle b = s.getRectangle();
                    if (b.Intersects(this.boundingBox))
                    {
                        collisionSprite = s;
                        remove = true;
                    }
                }
            }

            //remove objects that have collided (can't be removed in the loop)
            if (remove)
            {
                game.Components.Remove(this);
                game.Components.Remove(collisionSprite);
            }
        }*/
    }
}
