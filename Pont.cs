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
		public Pont((double, double) p, string koordinátatípus = "Descartes") : this(p.Item1, p.Item2, koordinátatípus) { }
		public bool DescartesBenneVan(Bitmap rajzlap) => 0 < X && X < rajzlap.Width && 0 < Y && Y < rajzlap.Height;
		public Point ToPoint() => new Point((int)Math.Round(X), (int)Math.Round(Y));
		public Point ToPoint(Point l, int x, int y) => new Point((int)Math.Round(X) + l.X - x / 2, (int)Math.Round(Y) + l.Y - y / 2);
		public static Pont operator +(Pont P, Pont Q) => new Pont(P.X + Q.X, P.Y + Q.Y);
		public static Pont operator -(Pont P, Pont Q) => new Pont(P.X - Q.X, P.Y - Q.Y);
		public static Pont operator -(Pont P) => new Pont(-P.X , -P.Y);
		public static Pont operator *(Pont P, double a) => a * P;
		public static Pont operator *(double a, Pont P) => new Pont(a * P.X, a * P.Y);
		private static double rad2deg(double rad) => (180 * rad / Math.PI);
		private static double deg2rad(double rad) => (Math.PI * rad / 180);
		private static double Sin(double szög) => Math.Sin(deg2rad(szög));
		private static double Cos(double szög) => Math.Cos(deg2rad(szög));
		private Pont ToPolár() => new Pont(Irányszög(), Hossz());
		private double Irányszög() => rad2deg(Math.Atan2(Y, X));
		private double Hossz() => Math.Sqrt(X * X + Y * Y);
		private Pont ToDescartes() => new Pont(Y * Cos(X), Y * Sin(X));
		public Pont Tükrözése_az_y_tengelyre() => new Pont(-X, Y);
		public Pont Tükrözése_az_x_tengelyre() => new Pont(X, -Y);
		public Pont Polárforma_forgatása(double fok) => new Pont(X+fok,Y);
		public Pont Forgatása_az_origó_körül(double fok) => ToPolár().Polárforma_forgatása(-fok).ToDescartes();

		public override string ToString() => $"{X};{Y}"; // debug így kényelmesebb

    }
}
