using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazard_Sweep.Classes
{
    public class PlayerCamera
    {
        public Matrix viewMatrix;
        private Vector2 m_position;
        private Vector2 m_halfViewSize;

        public PlayerCamera(Rectangle clientRect)
        {
            m_halfViewSize = new Vector2(clientRect.Width * 0.5f, clientRect.Height * 0.5f);
            UpdateViewMatrix();
        }

        public Vector2 Pos
        {
            get
            {
                return m_position;
            }

            set
            {
                m_position = value;
                UpdateViewMatrix();
            }
        }

        private void UpdateViewMatrix()
        {
            viewMatrix = Matrix.CreateTranslation(m_halfViewSize.X - m_position.X, m_halfViewSize.Y - m_position.Y, 0.0f);
        }
    }
}

