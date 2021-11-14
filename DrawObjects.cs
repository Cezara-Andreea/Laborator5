using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.IO;

namespace Dumitrache_Cezara_Andreea_3131B
{
    class DrawObjects
    {
        private Color[] ColorV = { Color.Pink, Color.Goldenrod, Color.WhiteSmoke, Color.BlueViolet, Color.Turquoise, Color.GreenYellow, Color.Olive, Color.MidnightBlue, Color.PowderBlue, Color.Purple, Color.LavenderBlush, Color.MediumAquamarine, Color.Red, Color.ForestGreen, Color.Coral };
        private bool visibility;
        private List<Vertexes> X = new List<Vertexes>();
        private List<Vertexes> Y = new List<Vertexes>();  
        private List<Vertexes> Z = new List<Vertexes>();

        public DrawObjects()
            {
            int[,] Vertexes = new int[,] {
            {70, 120, 70,
                120, 70, 120,
                70, 120, 70,
                120, 70, 120,
                70, 70, 70,
                70, 70, 70,
                70, 120, 70,
                120, 120, 70,
                120, 120, 120,
                120, 120, 120,
                70, 120, 70,
                120, 120, 70},
            {70, 70, 140,
                70, 140, 140,
                70, 70, 70,
                70, 70, 70,
                70, 140, 70,
                140, 70, 140,
                140, 140, 140,
                140, 140, 140,
                70, 140, 70,
                140, 70, 140,
                70, 70, 140,
                70, 140, 140},
            {80, 80, 80,
                80, 80, 80,
                80, 80, 140,
                80, 140, 140,
                80, 80, 140,
                80, 140, 140,
                80, 80, 140,
                80, 140, 140,
                80, 80, 140,
                80, 140, 140,
                140, 140, 140,
                140, 140, 140}};

            for (int i = 0; i < 36; i = i + 3)
            {
                X.Add(new Vertexes(Vertexes[0, i], Vertexes[1, i], Vertexes[2, i]));
                Y.Add(new Vertexes(Vertexes[0, i + 1], Vertexes[1, i + 1], Vertexes[2, i + 1]));
                Z.Add(new Vertexes(Vertexes[0, i + 2], Vertexes[1, i + 2], Vertexes[2, i + 2]));
            }

        }


        /// Laborator 5.1-part1-la apasarea unui click se genereaza obiectul si se deplaseaza
        /// in directia jos.

        public void CadereJos(ulong fps)
        {

            if (visibility)
            {
                if (fps < 70)
                {
                    GL.PushMatrix();
                    GL.Translate(0, -(long)fps, 0);
                    DrawCube();
                    GL.PopMatrix();
                }
                else
                {
                    GL.PushMatrix();
                    GL.Translate(0, -70, 0);
                    DrawCube();
                    GL.PopMatrix();
                }

            }

        }

        public void ToggleVisibility()
            {
                visibility = !visibility;
            }

            public bool GetVisibility()
            {
                return visibility;
            }

            public void DrawCube()
            {

                GL.Begin(PrimitiveType.Triangles);

                for (int i = 0; i < 12; i++)
                {
                    GL.Color3(ColorV[i / 2]);
                    GL.Vertex3(X.ElementAt(i).GetX(), X.ElementAt(i).GetY(), X.ElementAt(i).GetZ());
                    GL.Vertex3(Y.ElementAt(i).GetX(), Y.ElementAt(i).GetY(), Y.ElementAt(i).GetZ());
                    GL.Vertex3(Z.ElementAt(i).GetX(), Z.ElementAt(i).GetY(), Z.ElementAt(i).GetZ());
                }
                GL.End();
            }

        
    }
}
