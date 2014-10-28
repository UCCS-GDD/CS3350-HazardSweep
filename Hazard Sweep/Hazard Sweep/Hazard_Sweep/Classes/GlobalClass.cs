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
    //class for global variables such as screen size
    public class GlobalClass
    {
        private static float screenWidth;
        private static float screenHeight;

        public static float ScreenWidth
        {
            get { return screenWidth; }
            set { screenWidth = value; }
        }

        public static float ScreenHeight
        {
            get { return screenHeight; }
            set { screenHeight = value; }
        }
    }
}
