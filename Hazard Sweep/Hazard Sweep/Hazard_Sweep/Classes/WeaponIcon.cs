using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazard_Sweep.Classes
{
    public class WeaponIcon
    {
        Rectangle drawRec;
        Rectangle sRec;
        string textureFile;
        Texture2D texture;
        int x;
        WeaponType type;
        PlayerSprite player;

        public WeaponIcon(string textureFile, int x, WeaponType type, PlayerSprite player)
        {
            this.textureFile = textureFile;
            this.x = x;
            this.type = type;
            this.player = player;

            Initialize();
        }

        public void Initialize()
        {
            drawRec = new Rectangle(x, 0, 64, 64);
            sRec = new Rectangle(0, 0, 128, 128);
        }
        
        public void LoadContent(Game game)
        {
            texture = game.Content.Load<Texture2D>(textureFile);  
        }

        public void Update(GameTime gameTime)
        {
            if (player.hasPistol == true && type == WeaponType.Pistol)
            {
                WeaponAcquired();
            }
            if (player.hasAssaultRifle == true && type == WeaponType.AssaultRifle)
            {
                WeaponAcquired();
            }
            if (player.hasShotgun == true && type == WeaponType.Shotgun)
            {
                WeaponAcquired();
            }

            if (type == player.GetWeapon().GetWeaponType())
            {
                WeaponEquipped();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, drawRec, sRec, Color.White,0f,new Vector2(0f,0f),SpriteEffects.None,0f);
            
        }

        // weapon acquired
        public void WeaponAcquired()
        {
            sRec.X = 128;
        }

        // weapon equipped
        public void WeaponEquipped()
        {
            sRec.X = 256;
        }
    }
}
