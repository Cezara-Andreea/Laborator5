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
    class Vertexes
    {
            private double X;
            private double Y;
            private double Z;

            public Vertexes()
            {
                X = 0;
                Y = 0;
                Z = 0;
            }

            public Vertexes(double x, double y, double z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public double GetX()
        {
                return X;
            }

            public double GetY()
            {
                return Y;
            }

            public double GetZ()
            {
                return Z;
            }


            public void SetX(double x)
            {
                X = x;
            }

            public void SetY(double y)
            {
                Y = y;
            }

            public void SetZ(double z)
            {
                Z = z;
            }
        
    }
}
