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
	struct Pont
	{
		public double X { get; set; }
		public double Y { get; set; }

		public Pont(double x, double y, string koordinátatípus = "Descartes")
		{
			if (koordinátatípus == "Descartes")
			{
				X = x;
				Y = y;
			}
			else //koordinátatípus == "polár"
			{
				X = y * Math.Cos(x);
				Y = y * Math.Sin(x);
			}
		}

		public Point ToPoint() => new Point((int)Math.Round(X), (int)Math.Round(Y));

		public static Pont operator +(Pont P, Pont Q) => new Pont(P.X + Q.X, P.Y + Q.Y);


	}
}
