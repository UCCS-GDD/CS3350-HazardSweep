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
        protected int loadedBullets = 3;
        protected int capacity = 30;
        protected int totalBullets = 3;
        protected int delay = 10;
        protected double currentTime;
        protected Game game = null;
        [TestMethod()]
        public void WeaponTest()
        {
            Assert.AreEqual(null, game);
            Assert.AreEqual(3, totalBullets);
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
            totalBullets += 2;
            Assert.AreEqual(5, totalBullets);
        }


        [TestMethod()]
        public void shootTest()
        {
            loadedBullets = 3;
            while (loadedBullets > 2)
            {
                loadedBullets--;
                totalBullets--;
            }

            Assert.AreEqual(2, loadedBullets);
            Assert.AreEqual(2, totalBullets);
        }

        [TestMethod()]
        public void reloadTest()
        {
            for (int loadedBullets = 90; totalBullets > 0; loadedBullets--)
            {
                if ((loadedBullets != capacity) && (totalBullets >= 0))
                {
                    if (totalBullets >= capacity)//if there are more bullets than full reload
                    {
                        totalBullets -= capacity - loadedBullets;
                        loadedBullets = capacity;
                    }
                    else
                    {
                        if ((loadedBullets + totalBullets) < capacity)// if there are less bullets and loaded bullets than capacity
                        {       
                            totalBullets -= totalBullets;
                            loadedBullets += totalBullets;
                        }
                        else// if there are less total bullets than capacity but more loaded bullets plus total bullets than capacity
                        {
                            totalBullets -= capacity - loadedBullets;
                            loadedBullets = capacity;
                        }
                    }
                }
            }
            Assert.AreEqual(0, totalBullets);
        }
    }
}
