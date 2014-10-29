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

namespace Hazard_Sweep.Classes
{
    class Room : Sprite
    {
        private Rectangle boundary;
        private int row;
        private int column;
        private bool up;
        private bool down;

        public Room(Game game, string textureFile, Vector2 position, Rectangle boundary, int row, int column, bool up, bool down)
            : base(game, textureFile, position)
        {
            this.boundary = boundary;
            this.row = row;
            this.column = column;
            this.up = up;
            this.down = down;
        }

    }
}
