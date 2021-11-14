using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK;

namespace Dumitrache_Cezara_Andreea_3131B
{
    class Axes
    {
        private bool myVisibility;

        private const int AXIS_LENGTH = 375;

        public Axes()
        {
            myVisibility = true;
        }

        public void Draw()
        {
            if (myVisibility)
            {
                GL.LineWidth(5.0f);

                GL.Begin(PrimitiveType.Lines);
                GL.Color3(Color.RoyalBlue);
                GL.Vertex3(0, 0, 0);
                GL.Vertex3(AXIS_LENGTH, 0, 0);
                GL.Color3(Color.DeepPink);
                GL.Vertex3(0, 0, 0);
                GL.Vertex3(0, AXIS_LENGTH, 0);
                GL.Color3(Color.Purple);
                GL.Vertex3(0, 0, 0);
                GL.Vertex3(0, 0, AXIS_LENGTH);
                GL.End();


            }
        }

        public void ToggleVisibility()
        {
            myVisibility = !myVisibility;
        }

        public void Show()
        {
            myVisibility = true;
        }

        public void Hide()
        {
            myVisibility = false;
        }
    }
}

