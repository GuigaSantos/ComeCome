using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Pong
   
{

    class Program : GameWindow
    {

        double tamanhoBola = 40;
        double tamanhoJ = 15;
        double tamanhoFruta = 10;
        double velJ = 5;

        double xBola = 0;
        double xVelBola = 10;
        double yBola = 0;
        double yVelBola = 10;
        double yJ1 = 0;
        double xJ1 = -262.5;

        double yF = 0;
        double xF = 0;


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            xBola += xVelBola;
            yBola += yVelBola;

            if (xBola > ClientSize.Width / 2 )
            {
                xVelBola = -xVelBola;
            }

            if (xBola < -ClientSize.Width / 2)
            {
                xVelBola = -xVelBola;
            }



            if (yBola + tamanhoBola / 2 > ClientSize.Height / 2)
            {
                yVelBola = -yVelBola;
            }

            if (yBola - tamanhoBola / 2 < -ClientSize.Height / 2)
            {
                yVelBola = -yVelBola;
            }



            if (xJ1 + tamanhoJ / 2 > xF - tamanhoFruta / 2
                && xJ1 - tamanhoJ / 2 < xF + tamanhoFruta / 2
                && yJ1 + tamanhoJ / 2 > yF - tamanhoFruta / 2
                && yJ1 - tamanhoJ / 2 < yF + tamanhoFruta / 2)
            {
                if (tamanhoBola > 30) tamanhoBola -= 5;
                if (tamanhoJ < 150) tamanhoJ += 10;

                Random NewPositionX = new Random();
                Random NewPositionY = new Random();

                xF = NewPositionX.Next(-ClientSize.Width /2, ClientSize.Width /2);
                yF = NewPositionY.Next(-ClientSize.Height /2, ClientSize.Height /2);

            }




            if (xJ1 + tamanhoJ / 2 > xBola - tamanhoBola / 2
                && xJ1 - tamanhoJ / 2 < xBola + tamanhoBola / 2
                && yJ1 + tamanhoJ / 2 > yBola - tamanhoBola / 2
                && yJ1 - tamanhoJ / 2 < yBola + tamanhoBola / 2)
            {
                xVelBola = -xVelBola;
                yVelBola = -yVelBola;

                if (tamanhoBola < 150) tamanhoBola += 5;
                if (tamanhoJ > 10) tamanhoJ -= 5;
            }



            if (Keyboard.GetState().IsKeyDown(Key.W) && yJ1 + tamanhoJ / 2 < ClientSize.Height / 2)
            {
                yJ1 += velJ;
            }
            if (Keyboard.GetState().IsKeyDown(Key.S) && yJ1 - tamanhoJ / 2 > -ClientSize.Height / 2)
            {
                yJ1 -= velJ;
            }
            
            if (Keyboard.GetState().IsKeyDown(Key.D) && xJ1 + tamanhoJ / 2 < ClientSize.Width / 2)
            {
                xJ1 += velJ;
            }
            if (Keyboard.GetState().IsKeyDown(Key.A) && xJ1 - tamanhoJ / 2 > -ClientSize.Width / 2)
            {
                xJ1 -= velJ;
            }
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
            Matrix4 projection = Matrix4.CreateOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Clear(ClearBufferMask.ColorBufferBit);


            DesenharObjt(xF, yF - 1, tamanhoFruta, tamanhoFruta, 1.0f, 1.0f, 0.0f);
            DesenharObjt(xJ1, yJ1, tamanhoJ, tamanhoJ, 0.0f, 0.0f, 1.0f);
            DesenharObjt(xBola, yBola, tamanhoBola, tamanhoBola, 1.0f, 0.0f, 0.0f);

            SwapBuffers();
        }

        void DesenharObjt(double x, double y, double largura, double altura, float r, float g, float b)
        {
            GL.Color3(r, g, b);

            GL.Begin(PrimitiveType.Quads);

            GL.Vertex2(-0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, 0.5f * altura + y);
            GL.Vertex2(-0.5f * largura + x, 0.5f * altura + y);

            GL.End();
        }



        static void Main()
        {
            new Program().Run();

        }
    }
}

