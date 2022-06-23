using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Drawer
    {
        static float x, y;
        static Graphics graphic;        

        public static void CreateNewGraphic(Graphics newGraphic)
        {
            graphic = newGraphic;
            graphic.SmoothingMode = SmoothingMode.None;
            graphic.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        { x = x0; y = y0; }

        public static void DrawLine(Pen pen, double length, double angle)
        {
            //Делает шаг длиной length в направлении angle и рисует пройденную траекторию
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphic.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle));
            y = (float)(y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        static readonly double Pi = Math.PI;
        static readonly double SquareRootOfTwo = Math.Sqrt(2);
        static readonly float FirstFloatConst = 0.375f;
        static readonly float SecondFloatConst = 0.04f;

        public static void Draw(int width, int height, double turnAngle, Graphics graphic)
        {
            Drawer.CreateNewGraphic(graphic);

            var size = Math.Min(width, height);   
            var diagonal_length = SquareRootOfTwo * (size *(FirstFloatConst+SecondFloatConst)) / 2;
            var x0 = (float)(diagonal_length * Math.Cos(1.25*Pi)) + width / 2f;
            var y0 = (float)(diagonal_length * Math.Sin(1.25*Pi)) + height / 2f;

            Drawer.SetPosition(x0, y0);
            DrawImpossibleSquare(size, turnAngle);
        }

        private static void DrawImpossibleSquare(int size, double turnAngle)
        {
            DrawSideAndTurn(size, turnAngle);
            DrawSideAndTurn(size, turnAngle-0.5);
            DrawSideAndTurn(size, turnAngle + 1);
            DrawSideAndTurn(size, turnAngle + 0.5);
        }

        private static void DrawSideAndTurn(int size, double turnAngle)
        {
            DrawSide(size, turnAngle);
            DrawTurn(size, turnAngle - 1);
        }

        private static void DrawSide(int size, double turnAngle)
        {
            Drawer.DrawLine(Pens.Yellow, size * FirstFloatConst, Pi * turnAngle);
            Drawer.DrawLine(Pens.Yellow, size * SecondFloatConst * SquareRootOfTwo, Pi * (turnAngle + 0.25));
            Drawer.DrawLine(Pens.Yellow, size * FirstFloatConst, Pi * (turnAngle + 1));
            Drawer.DrawLine(Pens.Yellow, size * (FirstFloatConst - SecondFloatConst), Pi * (turnAngle+0.5));
        }

        private static void DrawTurn(int size, double turnAngle)
        {
            Drawer.Change(size * SecondFloatConst, Pi * turnAngle);
            Drawer.Change(size * SecondFloatConst * SquareRootOfTwo, Pi * (turnAngle+1.75));
        }
    }
}
