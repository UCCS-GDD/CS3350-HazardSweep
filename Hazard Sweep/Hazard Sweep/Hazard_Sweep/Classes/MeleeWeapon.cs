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
    class MeleeWeapon : GameComponent
    {
        //class variables
        protected int delay;
        protected Rectangle hitBox;
        protected Vector2 offset;
        private int damage = 2;
        public Texture2D hitTexture;            
        public SpriteBatch sb;



        //constructor
        public MeleeWeapon(Game game, Vector2 offset, int hitBoxSize, Texture2D hitTexture)
            : base(game)
        {
            this.hitTexture = hitTexture;
            delay = 10;
            this.offset = offset - new Vector2(-25, 50);
            hitBox = new Rectangle(0, 0, hitBoxSize, 200);
        }

        //attack method
        public void attack(Facing facing, Vector2 position)
        {
            if (facing == Facing.Left)
            {
                hitBox.X = (int)(position.X - offset.X);
            }
            else
            {
                hitBox.X = (int)(position.X + offset.X);
            }
            hitBox.Y = (int)(position.Y + offset.Y);
            Enemy e;
            foreach (GameComponent g in Game.Components)
            {
                if (g is Enemy)
                {
                    e = (Enemy)g;                    

                    //collision logic
                    if (hitBox.Intersects(e.getRectangle()))
                    {
                        e.removeHelth(damage);
                    }
                }                
            }
        }

    }
}
