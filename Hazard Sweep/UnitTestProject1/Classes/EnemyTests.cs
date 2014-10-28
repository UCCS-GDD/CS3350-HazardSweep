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
namespace Enemy.Tests
{
    [TestClass()]
    public class EnemyTests
    {
        //class variables
        protected int health;
        protected Vector2 target;
        protected float moveSpeed = 1;
        protected int damage = 5;
        protected int currentTime;
        protected int delay = 30;
        public Game game = null;
        public string texturefile = "test";
        Vector2 position = Vector2.One;
        public Rectangle boundingBox = new Rectangle(1, 1, 1, 1);

        [TestMethod()]
        public void EnemyTest()
        {
            Assert.AreEqual(null, game);
            Assert.AreEqual("test", texturefile);
            Assert.AreEqual(Vector2.One, position);
            health = 2;
            Assert.AreEqual(2, health);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            //enemy AI (moves enemy towards player
            Vector2 direction = target - position;
            direction.Normalize();
            //enemy will only move towards player if the player is within 250
            if (Math.Abs(Vector2.Distance(target, position)) < 250)
            {
                Vector2 velocity = direction * moveSpeed;
                position += velocity;
            }
        }
    }
}
