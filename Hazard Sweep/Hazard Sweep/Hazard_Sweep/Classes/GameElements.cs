using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazard_Sweep.Classes
{
    class GameElements
    {
        // icons for weapons
        WeaponIcon pistolIcon;
        WeaponIcon assaultIcon;
        WeaponIcon shotgunIcon;

        // health/ammo text assets
        Vector2 ammoLabelLocation;
        Vector2 ammoNumericLocation;
        Vector2 healthLabelLocation;
        Vector2 healthNumericLocation;
        string ammoText;
        string healthText;
        SpriteFont ammoLabelFont;
        SpriteFont ammoNumericFont;

        // handling other entities
        Game game;
        PlayerSprite Player { get; set; }

        public GameElements(Game game, PlayerSprite player)
        {
            this.game = game;
            this.Player = player;
        }

        public void Initialize()
        {
            // weapon icons
            pistolIcon = new WeaponIcon
                ("Images//GUI//iconPistol", ((int)GlobalClass.ScreenWidth - (64 * 3)), WeaponType.Pistol, Player);
            assaultIcon = new WeaponIcon
                ("Images//GUI//iconAssault", ((int)GlobalClass.ScreenWidth - (64 * 2)), WeaponType.AssaultRifle, Player);
            shotgunIcon = new WeaponIcon
                ("Images//GUI//iconShotgun", ((int)GlobalClass.ScreenWidth - (64)), WeaponType.Shotgun, Player);

            // health/ammo text locations
            ammoLabelLocation = new Vector2(48, GlobalClass.ScreenHeight - 96);
            ammoNumericLocation = new Vector2(32, GlobalClass.ScreenHeight - 64);
            healthLabelLocation = new Vector2(GlobalClass.ScreenWidth - 84, GlobalClass.ScreenHeight - 96);
            healthNumericLocation = new Vector2(GlobalClass.ScreenWidth - 96, GlobalClass.ScreenHeight - 64);
            ammoText = "-1/-1";
            healthText = "-1";
        }

        public void LoadContent()
        {
            // load fonts
            ammoLabelFont = game.Content.Load<SpriteFont>("Fonts//AmmoLabel");
            ammoNumericFont = game.Content.Load<SpriteFont>("Fonts//AmmoNumeric");

            // load weapon icons
            pistolIcon.LoadContent(game);
            assaultIcon.LoadContent(game);
            shotgunIcon.LoadContent(game);
        }

        public void Update(GameTime gameTime)
        {
            // update health/ammo text
            ammoText = Player.GetWeapon().getClipBullets() + " / " + Player.GetWeapon().getTotalNumBullets();
            healthText = "" + Player.GetHealth();

            pistolIcon.Update(gameTime);
            assaultIcon.Update(gameTime);
            shotgunIcon.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // draw health
            spriteBatch.DrawString(ammoLabelFont, "Health", healthLabelLocation, Color.White);
            spriteBatch.DrawString(ammoNumericFont, healthText, healthNumericLocation, Color.White);

            // draw weapon icons
            pistolIcon.Draw(spriteBatch);
            assaultIcon.Draw(spriteBatch);
            shotgunIcon.Draw(spriteBatch);     

            // draw ammo
            if (Player.GetWeapon().GetWeaponType() != WeaponType.Melee)
            {
                spriteBatch.DrawString(ammoLabelFont, "Ammo", ammoLabelLocation, Color.White);
                spriteBatch.DrawString(ammoNumericFont, ammoText, ammoNumericLocation, Color.White);
            }
            spriteBatch.End();
        }
    }
}
