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
    class MassiveObject
    {
        private List<Vertexes> vtx = new List<Vertexes>();


        private const String FILENAME = "assets/slime.obj";
        private const int FACTOR_SCALARE_IMPORT = 100;

        private List<Vector3> coordsList;
        private bool visibility;
        private Color meshColor;
        private bool hasError;

        public MassiveObject(Color col)
        {

            try
            {
                coordsList = LoadFromObjFile(FILENAME);

                if (coordsList.Count == 0)
                {
                    Console.WriteLine("Crearea obiectului a esuat: obiect negasit/coordonate lipsa!");
                    return;
                }
                visibility = true;
                meshColor = col;
                hasError = false;
                Console.WriteLine("Obiect 3D încarcat - " + coordsList.Count.ToString() + " vertexuri disponibile!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: assets file <" + FILENAME + "> is missing!!!");
                hasError = true;
                Console.WriteLine(ex.Message);
            }
        }

        /// Laborator 5.3 - stocarea vertexurilor cu fisierelor text + testare de eroare
        /// la deschiderea resursei

        public MassiveObject(string fnume)
        {
            try
            {
                StreamReader f1 = new StreamReader(fnume);
                string line;

                while ((line = f1.ReadLine()) != null)
                {
                    if (line.Split(' ')[0] == "vtx")
                    {
                        vtx.Add(new Vertexes(Double.Parse(line.Split(' ')[1]), Double.Parse(line.Split(' ')[2]), Double.Parse(line.Split(' ')[3])));
                    }
                }

                f1.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine("Nu se poate citi din fisier!!!");
                Console.WriteLine(e.Message);
            }

        }

        public void ToggleVisibility()
        {
            if (hasError == false)
            {
                visibility = !visibility;
            }
        }

        public void Draw()
        {
            if (hasError == false && visibility == true)
            {
                GL.Color3(meshColor);
                GL.Begin(PrimitiveType.Triangles);
                foreach (var vert in coordsList)
                {
                    GL.Vertex3(vert);
                }
                GL.End();
            }
        }

        private List<Vector3> LoadFromObjFile(string fname)
        {
            List<Vector3> vlc3 = new List<Vector3>();


            var lines = File.ReadLines(fname);
            foreach (var line in lines)
            {
                if (line.Trim().Length > 2)
                {
                    string ch1 = line.Trim().Substring(0, 1);
                    string ch2 = line.Trim().Substring(1, 1);
                    if (ch1 == "v" && ch2 == " ")
                    {

                        string[] block = line.Trim().Split(' ');
                        if (block.Length == 4)
                        {
                            // ATENTIE: Pericol!!!
                            float xval = float.Parse(block[1].Trim()) * FACTOR_SCALARE_IMPORT;
                            float yval = float.Parse(block[2].Trim()) * FACTOR_SCALARE_IMPORT;
                            float zval = float.Parse(block[3].Trim()) * FACTOR_SCALARE_IMPORT;

                            vlc3.Add(new Vector3((int)xval, (int)yval, (int)zval));

                        }
                    }
                }
            }

            return vlc3;
        }
    }
}
