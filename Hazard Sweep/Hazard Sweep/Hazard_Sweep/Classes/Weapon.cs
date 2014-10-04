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
    class Weapon : GameComponent
    {
        //variables
        protected int loadedBullets;
        protected int capacity = 30;
        protected int totalBullets;
        protected int delay = 90;
        protected double currentTime;
        protected KeyboardState keyboardState;

        //weapon constructor
        public Weapon(Game game, int totalBullets) : base(game)
        {
            this.totalBullets = totalBullets;
            loadedBullets = capacity;
        }        

        //update methode
        public override void Update(GameTime gameTime)
        {
            currentTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            keyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }

        //adds bullets to the total bullets
        public void addBullets(int numBullets)
        {

        }

        //returns number of bullets
        public int getNumBullets()
        {
            return totalBullets;
        }

        //fires the weapon
        public void shoot()
        {
            if(((int)currentTime >= delay) && keyboardState.IsKeyDown(Keys.Space) &&
                (loadedBullets > 0))
            {
                loadedBullets--;
            }
        }

        //reloads weapon
        public void reload()
        {
            if((loadedBullets != capacity) && (totalBullets > 0) && 
                keyboardState.IsKeyDown(Keys.R))
            {
                if(totalBullets >= 30)
                {
                    totalBullets -= capacity - loadedBullets;
                    loadedBullets = capacity;
                }
                else 
                {
                    if((loadedBullets + totalBullets) < capacity)
                    {
                        loadedBullets += totalBullets;
                    }
                    else
                    {
                        totalBullets = capacity - loadedBullets;
                        loadedBullets = capacity;
                    }
                }
            }
        }
    }
}
