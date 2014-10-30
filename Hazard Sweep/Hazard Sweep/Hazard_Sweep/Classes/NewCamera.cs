using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazard_Sweep.Classes
{
    class NewCamera
    {
        private static NewCamera instance;
        Vector2 position;
        Matrix viewMatrix;

        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }
        public static NewCamera Instance
        {
            get
            {
                if (instance == null)
                    instance = new NewCamera();
                return instance;
            }
        }

        public void SetFocalPoint(Vector2 focalPosition)
        {
            position = new Vector2(focalPosition.X - GlobalClass.ScreenWidth / 2, 
                focalPosition.Y - GlobalClass.ScreenHeight / 2);

            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
        }

        public void Update()
        {
            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }
    }
}
