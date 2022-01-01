using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogoKaresz
{
	public struct Pont
	{
		public double X { get; set; }
		public double Y { get; set; }
		public Pont(double x, double y, string koordinátatípus = "Descartes") => (X, Y) = koordinátatípus == "Descartes" ? (x, y) : (y * Cos(x), y * Sin(x));
		public Pont(Point loc) : this(loc.X, loc.Y) { }
		public bool DescartesBenneVan(Bitmap rajzlap) => 0 < X && X < rajzlap.Width && 0 < Y && Y < rajzlap.Height;
		public Point ToPoint() => new Point((int)Math.Round(X), (int)Math.Round(Y));
		public Point ToPoint(Point l, int x, int y) => new Point((int)Math.Round(X)+l.X-x/2, (int)Math.Round(Y) + l.Y - y / 2);
		public static Pont operator +(Pont P, Pont Q) => new Pont(P.X + Q.X, P.Y + Q.Y);
		public static Pont operator -(Pont P, Pont Q) => new Pont(P.X - Q.X, P.Y - Q.Y);
		private static double Sin(double szög) => Math.Sin(Math.PI * szög / 180);
		private static double Cos(double szög) => Math.Cos(Math.PI * szög / 180);
	}
}
