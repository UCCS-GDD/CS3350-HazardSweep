// found at http://www.dreamincode.net/forums/topic/237979-2d-camera-in-xna/

#region Version History (1.0)
// 03.07.11 ~ Created
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hazard_Sweep.Classes
{
    public class Camera2D
    {
        #region Fields

        protected Matrix transform;
        protected Vector2 pos;
        protected Viewport viewport;
        protected KeyboardState ks;
        protected Matrix inverseTransform;

        #endregion

        #region Properties

        /// <summary>
        /// Camera View Matrix Property
        /// </summary>
        public Matrix Transform
        {
            get { return transform; }
            set { transform = value; }
        }
        /// <summary>
        /// Inverse of the view matrix, can be used to get objects screen coordinates
        /// from its object coordinates
        /// </summary>
        public Matrix InverseTransform
        {
            get { return inverseTransform; }
        }
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        #endregion

        #region Constructor

        public Camera2D(Viewport viewport)
        {
            pos = Vector2.Zero;
            this.viewport = viewport;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update the camera view
        /// </summary>
        public void Update()
        {
            //Call Camera Input
            Input();
            //Create view matrix
            transform = Matrix.CreateTranslation(pos.X, pos.Y, 0);
            //Update inverse matrix
            inverseTransform = Matrix.Invert(transform);
        }

        /// <summary>
        /// Example Input Method, rotates using cursor keys and zooms using mouse wheel
        /// </summary>
        protected virtual void Input()
        {

            ks = Keyboard.GetState();

            //Check Move
            if (ks.IsKeyDown(Keys.A))
            {
                pos.X += 5f;
            }
            if (ks.IsKeyDown(Keys.D))
            {
                pos.X -= 5f;
            }
            if (ks.IsKeyDown(Keys.W))
            {
                pos.Y += 5f;
            }
            if (ks.IsKeyDown(Keys.S))
            {
                pos.Y -= 5f;
            }
        }
        #endregion
    }
}

