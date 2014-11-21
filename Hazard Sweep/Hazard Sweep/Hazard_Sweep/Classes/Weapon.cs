using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Hazard_Sweep.Classes
{
    public class Weapon : GameComponent
    {
        //variables
        protected int loadedBullets;
        protected int capacity;
        protected int totalBullets;
        protected int delay;
        protected double currentTime;
        protected Game game;
        WeaponType type;

        //weapon constructor
        public Weapon(WeaponType type, Game game, int totalBullets, int capacity, int delay)
            : base(game)
        {
            this.game = game;
            this.totalBullets = totalBullets;
            this.capacity = capacity;
            loadedBullets = capacity;
            this.delay = delay;
            currentTime = 0;
            this.type = type;
        }

        //update methode
        public override void Update(GameTime gameTime)
        {
            //currentTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            currentTime++;

            base.Update(gameTime);
        }

        //adds bullets to the total bullets
        public void addBullets(int numBullets)
        {
            totalBullets += numBullets;
        }

        //returns the total bullets in the clip
        public int getClipBullets()
        {
            return loadedBullets;
        }

        // return clip size
        public int GetClipSize()
        {
            return capacity;
        }

        //returns number of total bullets held
        public int getTotalNumBullets()
        {
            return totalBullets;

        }

        //fires the weapon
        public void shoot(Vector2 bulletOrigin, Vector2 direction)
        {
            if (((int)currentTime >= delay) && (loadedBullets > 0))
            {
                if (this.type == WeaponType.Shotgun)
                {
                    (game as Game1).playShotgun();
                    //make random offset to allow shotgun bullet spread
                    Vector2 randOffset1 = new Vector2(Game1.Random.Next(1, 5), Game1.Random.Next(1, 5));

                    //fix issues of slow bullets
                    while (randOffset1.Length() < 1)
                    {
                        randOffset1 = new Vector2(Game1.Random.Next(1, 5), Game1.Random.Next(1, 5));
                        Vector2.Normalize(randOffset1);
                    }
                    game.Components.Add(new Bullet(game, "Images/laser", bulletOrigin, direction * randOffset1));
                    //make random offset to allow shotgun bullet spread
                    Vector2 randOffset2 = new Vector2(Game1.Random.Next(1, 5), Game1.Random.Next(1, 5));
                    Vector2.Normalize(randOffset2);
                    //fix issues of slow bullets
                    while (randOffset2.Length() < 1)
                    {
                        randOffset2 = new Vector2(Game1.Random.Next(1, 5), Game1.Random.Next(1, 5));
                        Vector2.Normalize(randOffset2);
                    }
                    game.Components.Add(new Bullet(game, "Images/laser", bulletOrigin, direction * randOffset2));
                    //make random offset to allow shotgun bullet spread
                    Vector2 randOffset3 = new Vector2(Game1.Random.Next(1, 5), Game1.Random.Next(1, 5));
                    Vector2.Normalize(randOffset3);
                    //fix issues of slow bullets
                    while (randOffset3.Length() < 1)
                    {
                        randOffset3 = new Vector2(Game1.Random.Next(1, 5), Game1.Random.Next(1, 5));
                        Vector2.Normalize(randOffset3);
                    }
                    game.Components.Add(new Bullet(game, "Images/laser", bulletOrigin, direction * randOffset3));
                    //make random offset to allow shotgun bullet spread
                    Vector2 randOffset4 = new Vector2(Game1.Random.Next(1, 5), Game1.Random.Next(1, 5));
                    Vector2.Normalize(randOffset4);
                    //fix issues of slow bullets
                    while (randOffset4.Length() < 1)
                    {
                        randOffset4 = new Vector2(Game1.Random.Next(1, 5), Game1.Random.Next(1, 5));
                        Vector2.Normalize(randOffset4);
                    }
                    game.Components.Add(new Bullet(game, "Images/laser", bulletOrigin, direction * randOffset4));

                }
                else if (this.type == WeaponType.AssaultRifle)
                {
                    (game as Game1).playMachinegun();
                    game.Components.Add(new Bullet(game, "Images/laser", bulletOrigin, direction));

                }
                else if (this.type == WeaponType.Pistol)
                {
                    (game as Game1).playPistol();
                    game.Components.Add(new Bullet(game, "Images/laser", bulletOrigin, direction));

                }
                else
                { }
                loadedBullets--;

                //creates a bullet
                currentTime = 0;
            }
            if (((int)currentTime >= delay) && (loadedBullets == 0))
            {
                (game as Game1).playDryFire();
                currentTime = 0;
            }
        }

        //reloads weapon
        public void reload()
        {
            if ((loadedBullets != capacity) && (totalBullets >= 0))
            {
                if (totalBullets >= capacity)//if there are more bullets than full reload
                {
                    totalBullets -= capacity - loadedBullets;
                    loadedBullets = capacity;
                    (game as Game1).playReload();
                }
                else
                {
                    if ((loadedBullets + totalBullets) < capacity)// if there are less bullets and loaded bullets than capacity
                    {
                        loadedBullets += totalBullets;
                        totalBullets -= totalBullets;
                    }
                    else// if there are less total bullets than capacity but more loaded bullets plus total bullets than capacity
                    {
                        totalBullets -= capacity - loadedBullets;
                        loadedBullets = capacity;
                        (game as Game1).playReload();
                    }
                }
            }
        }

        public WeaponType GetWeaponType()
        {
            return type;
        }
    }
}
