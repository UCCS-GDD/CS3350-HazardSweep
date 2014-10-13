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
namespace Weapon.Tests
{
    [TestClass()]
    public class WeaponTests
    {        
        protected int loadedBullets;
        protected int capacity = 30;
        protected int totalBullets = 90;
        protected int delay = 10;
        protected double currentTime;
        protected Game game = null;
        [TestMethod()]
        public void WeaponTest()
        {
            Assert.AreEqual(null, game);
            Assert.AreEqual(90, totalBullets);
            loadedBullets = capacity;
            Assert.AreEqual(capacity, loadedBullets);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            double testTime = currentTime + 1;
            currentTime++;
            Assert.AreEqual(testTime, currentTime);
        }

        [TestMethod()]
        public void addBulletsTest()
        {
            totalBullets += 4;
            Assert.AreEqual(94, totalBullets);
        }


        [TestMethod()]
        public void shootTest()
        {
                int testBullets = loadedBullets - 1;
                loadedBullets--;
                currentTime = 0;
                Assert.AreEqual(testBullets, loadedBullets);
        }

        [TestMethod()]
        public void reloadTest()
        {
            if ((loadedBullets != capacity) && (totalBullets > 0))
            {
                if (totalBullets >= 30)//if there are more bullets than full reload
                {
                    totalBullets -= capacity - loadedBullets;
                    loadedBullets = capacity;
                }
                else
                {
                    if ((loadedBullets + totalBullets) < capacity)// if there are less bullets and loaded bullets than capacity
                    {
                        loadedBullets += totalBullets;
                    }
                    else// if there are less total bullets than capacity but more loaded bullets plus total bullets than capacity
                    {
                        totalBullets = capacity - loadedBullets;
                        loadedBullets = capacity;
                    }
                }
            }
        }
    }
}
