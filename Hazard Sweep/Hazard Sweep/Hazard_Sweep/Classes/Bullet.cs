using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Hazard_Sweep;
using Hazard_Sweep.Classes;

namespace Hazard_Sweep.Classes
{
    //bullet class
    public class Bullet : Sprite
    {
        //variables
        protected int speed;
        protected Vector2 direction;
        protected bool remove = false;
        protected int damage;

        //constructor
        public Bullet(Game game, string textureFile, Vector2 position, Vector2 dir)
            : base(game, textureFile, position, 10)
        {
            this.game = game;
            direction = dir;
            speed = 900;
        }

        //update method
        public override void Update(GameTime gameTime)
        {
            //determine how much damage the bullet should do
            foreach (GameComponent g in game.Components)
                if (g is PlayerSprite)
                {
                    PlayerSprite p = (PlayerSprite)g;
                    Weapon w = p.GetWeapon();
                    if (w.GetWeaponType() == WeaponType.Shotgun)
                    {
                        damage = 2;
                    }
                    else
                    {
                        damage = 1;
                    }

                }

            //logic for determining which direction the bullet should move
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Sprite collisionSprite = new Sprite(game);
            //check for collisions
            foreach (GameComponent g in game.Components)
            {
                if (g is Enemy)
                {
                    Enemy s = (Enemy)g;
                    Rectangle b = s.getRectangle();
                    if (b.Intersects(this.boundingBox))
                    {
                        collisionSprite = s;
                        remove = true;
                        s.removeHelth(damage);
                        s.setShouldColor();
                    }
                }
            }

            //remove objects that have collided (can't be removed in the loop)
            if (remove)
            {
                game.Components.Remove(this);
            }

            base.Update(gameTime);
        }

        //random number generator
        public int randomNumGen(int min, int max)
        {
            int value = random.Next(min, max);
            return value;
        }
    }
}
