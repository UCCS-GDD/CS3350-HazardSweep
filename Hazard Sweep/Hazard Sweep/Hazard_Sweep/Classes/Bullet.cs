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
    //bullet class
    public class Bullet : Sprite
    {
        //variables
        protected int speed;
        protected Facing direction;

        //constructor
        public Bullet(Game game, string textureFile, Vector2 position, Facing dir)
            : base(game, textureFile, position)
        {
            direction = dir;
        }

        //update method
        public override void Update(GameTime gameTime)
        {
            //logic for determining which direction the bullet should move
            if (direction == Facing.Left)
            {
                position.X -= 10;
            }
            else
            {
                position.X += 10;
            }
            base.Update(gameTime);
        }
    }
}
