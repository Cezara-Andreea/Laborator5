using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace Dumitrache_Cezara_Andreea_3131B
{
    class Window3D : GameWindow
    {

        private KeyboardState previousKeyboard;
        private MouseState previousMouse;
        private readonly Axes axes;
        private readonly Grid grid;
        private readonly Camera3DIsometric camera3d;
        private DrawObjects drawObj;
        private MassiveObject obj;
        private bool displayMarker;
        private ulong updatesCounter;
        private ulong framesCounter;
        private readonly Randomizer rando;

        private readonly Color DEFAULT_BKG_COLOR = Color.FromArgb(49, 50, 51);

        public Window3D() : base(1280, 768, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;
            rando = new Randomizer();
            axes = new Axes();
            grid = new Grid();
            camera3d = new Camera3DIsometric();
            drawObj = new DrawObjects();

            /// Laborator 5.3 - stocarea vertexurilor cu fisierelor text

            obj = new MassiveObject(Color.AliceBlue);

            obj = new MassiveObject("D:\\FACULTATE\\An3 Calc\\sem1\\EGC\\Teme\\Dumitrache_Cezara_Andreea_3131B\\MassiveObject.txt");

            DisplayHelp();
            displayMarker = false;
            updatesCounter = 0;
            framesCounter = 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.ClearColor(DEFAULT_BKG_COLOR);

            GL.Viewport(0, 0, this.Width, this.Height);

            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 1024);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            camera3d.SetCamera();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            updatesCounter++;

            if (displayMarker)
            {
                TimeStampIt("update", updatesCounter.ToString());
            }

            KeyboardState currentKeyboard = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();

            if (currentKeyboard[Key.Escape])
            {
                Exit();
            }

            if (currentKeyboard[Key.H] && !previousKeyboard[Key.H])
            {
                DisplayHelp();
            }

            if (currentKeyboard[Key.R] && !previousKeyboard[Key.R])
            {
                GL.ClearColor(DEFAULT_BKG_COLOR);
                axes.Show();
                grid.Show();
            }

            if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
            {
                GL.ClearColor(rando.RandomColor());
            }

            if (currentKeyboard[Key.K] && !previousKeyboard[Key.K])
            {
                axes.ToggleVisibility();
            }

            if (currentKeyboard[Key.V] && !previousKeyboard[Key.V])
            {
                grid.ToggleVisibility();
            }


            /// Laborator 5.2 - modificare aplicație pentru a manipula valorile camerei 
            /// (permite mișcare și repoziționare la 2 locații predefinite, “aproape” și “departe”.


            if (currentKeyboard[Key.L])
            {
                camera3d.PozitionareAproape();
            }

            if (currentKeyboard[Key.M])
            {
                camera3d.PozitionareDeparte();
            }

            ////// Laborator 5.3 - stocarea vertexurilor cu fisierelor text

            if (currentKeyboard[Key.O] && !previousKeyboard[Key.O])
            {
                obj.ToggleVisibility();
            }

            //

            if (currentKeyboard[Key.W])
            {
                camera3d.MoveForward();
            }
            if (currentKeyboard[Key.S])
            {
                camera3d.MoveBackward();
            }
            if (currentKeyboard[Key.A])
            {
                camera3d.MoveLeft();
            }
            if (currentKeyboard[Key.D])
            {
                camera3d.MoveRight();
            }
            if (currentKeyboard[Key.Q])
            {
                camera3d.MoveUp();
            }
            if (currentKeyboard[Key.E])
            {
                camera3d.MoveDown();
            }

            if (currentKeyboard[Key.L] && !previousKeyboard[Key.L])
            {
                displayMarker = !displayMarker;
            }

            /// Laborator 5.1-part1-la apasarea unui click se genereaza obiectul si se deplaseaza
            /// in directia jos.

            if (currentMouse[MouseButton.Left] && !previousMouse[MouseButton.Left])
            {
                drawObj.ToggleVisibility();

            }

            previousKeyboard = currentKeyboard;
            previousMouse = currentMouse;

        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            framesCounter++;

            if (displayMarker)
            {
                TimeStampIt("render", framesCounter.ToString());
            }

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            grid.Draw();
            axes.Draw();
            obj.Draw();

            /// Laborator 5.1-part2-la contactul cu planul OXZ de pe grid, obiectul se opreste.

            if (drawObj.GetVisibility() == true)
            {
                drawObj.CadereJos(updatesCounter);
            }
            else
            {
                updatesCounter = 0;
            }

            SwapBuffers();
        }

        private void DisplayHelp()
        {
            Console.WriteLine("\n      MENIU");
            Console.WriteLine(" (H) - meniul");
            Console.WriteLine(" (ESC) - parasire aplicatie");
            Console.WriteLine(" (K) - schimbare vizibilitate sistem de axe");
            Console.WriteLine(" (R) - resteaza scena la valori implicite");
            Console.WriteLine(" (B) - schimbare culoare de fundal");
            Console.WriteLine(" (V) - schimbare vizibilitate linii");
            Console.WriteLine(" (W,A,S,D) - deplasare camera (izometric)");
            Console.WriteLine(" (O) - desenare obiect (MassiveObject.cs)");
            Console.WriteLine(" (click mouse) - desenare cub si deplasare pe directia jos - ex 1");
            Console.WriteLine(" (L) - pozitionare aproape - ex 2");
            Console.WriteLine(" (M) - pozitionare departe - ex 2");



        }

        private void TimeStampIt(String source, String counter)
        {
            String dt = DateTime.Now.ToString("hh:mm:ss.ffff");
            Console.WriteLine("     TSTAMP from <" + source + "> on iteration <" + counter + ">: " + dt);
        }

    }
}
