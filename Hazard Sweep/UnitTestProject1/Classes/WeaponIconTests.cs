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
namespace WeaponIcon.Tests
{
//    [TestClass()]
//    public class WeaponIconTests
//    {
//        Rectangle drawRec;
//        Rectangle sRec;
//        string textureFile;
//        Texture2D texture;
//        int x;
//        WeaponType type;

//        [TestMethod()]
//        public void WeaponIconTest()
//        {
//(string textureFile, int x, WeaponType type, PlayerSprite player)
//                this.textureFile = textureFile;
//            this.x = x;
//            this.type = type;
//            this.player = player;

//            Initialize();
//        }

//        [TestMethod()]
//        public void InitializeTest()
//        {
//            drawRec = new Rectangle(x, 0, 64, 64);
//            sRec = new Rectangle(0, 0, 128, 128);

//            if (player.hasMelee == true && type == WeaponType.Melee)
//            {
//                WeaponAcquired();
//            }
//            if (player.hasPistol == true && type == WeaponType.Pistol)
//            {
//                WeaponAcquired();
//            }
//            if (player.hasAssaultRifle == true && type == WeaponType.AssaultRifle)
//            {
//                WeaponAcquired();
//            }
//            if (player.hasShotgun == true && type == WeaponType.Shotgun)
//            {
//                WeaponAcquired();
//            }

//            if (type == player.GetWeapon().GetWeaponType())
//            {
//                WeaponEquipped();
//            }
//        }

//        [TestMethod()]
//        public void LoadContentTest()
//        {
//            Game game;
//            texture = game.Content.Load<Texture2D>(textureFile);
//        }

//        [TestMethod()]
//        public void UpdateTest()
//        {
//            KeyboardState ks = Keyboard.GetState();

//            if (ks.IsKeyDown(Keys.D1))
//            {
//                if (player.hasMelee == true)
//                {
//                    if (type == WeaponType.Melee)
//                    {
//                        WeaponEquipped();
//                    }
//                    else
//                    {
//                        WeaponAcquired();
//                    }
//                }
//            }
//            if (ks.IsKeyDown(Keys.D2))
//            {
//                if (player.hasPistol == true)
//                {
//                    if (type == WeaponType.Pistol)
//                    {
//                        WeaponEquipped();
//                    }
//                    else
//                    {
//                        WeaponAcquired();
//                    }
//                }
//            }
//            if (ks.IsKeyDown(Keys.D3))
//            {
//                if (player.hasAssaultRifle == true)
//                {
//                    if (type == WeaponType.AssaultRifle)
//                    {
//                        WeaponEquipped();
//                    }
//                    else
//                    {
//                        WeaponAcquired();
//                    }
//                }
//            }
//            if (ks.IsKeyDown(Keys.D4))
//            {
//                if (player.hasShotgun == true)
//                {
//                    if (type == WeaponType.Shotgun)
//                    {
//                        WeaponEquipped();
//                    }
//                    else
//                    {
//                        WeaponAcquired();
//                    }
//                }
//            }
//        }

//        [TestMethod()]
//        public void DrawTest()
//        {
//            sb.Draw(texture, drawRec, sRec, Color.White, 0f, new Vector2(0f, 0f), SpriteEffects.None, 0f);

//        }

//        [TestMethod()]
//        public void WeaponAcquiredTest()
//        {
//            sRec.X = 128;
//        }

//        [TestMethod()]
//        public void WeaponEquippedTest()
//        {
//            sRec.X = 256;
//        }
//    }
}
