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
		private double X;
		private double Y;

		public Pont(double x, double y)
		{
			X = x;
			Y = y;
		}

		public Point ToPoint() => new Point((int)Math.Round(X), (int)Math.Round(Y));


	}
}
