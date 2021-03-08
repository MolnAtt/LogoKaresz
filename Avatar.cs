using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace LogoKaresz
{
	class Avatar
	{
		private Pont hely;
		private double irány;
		Form1 szülőform;
		private PictureBox avatarpb;
		Pen toll;
		Graphics gr;
		int w;
		int h;
		bool rajzole;

		public Avatar(Form1 szülőform, Pont hely, double irány)
		{
			this.szülőform = szülőform;
			this.hely = hely;
			this.irány = irány;
			this.toll = new Pen(Color.Black);
			this.gr = Graphics.FromImage(szülőform.rajzlap); // formnak itt már lennie kell!
			this.w = 5;
			this.h = 5;
			this.rajzole = true;

			avatarpb = new PictureBox();
			avatarpb.BackColor = Color.Blue;
			avatarpb.Size = new Size(w, h);
			szülőform.Controls.Add(avatarpb);
			avatarpb.BringToFront();

			Frissít();
		}

		public void Lépj(double t)
		{
			Pont hollesz = hely + new Pont(irány, t, "polár"); // ez most polárkoordinátás kellene legyen!
			if (rajzole)
			{
				gr.DrawLine(toll, hely.ToPoint(), hollesz.ToPoint());
			}
			hely = hollesz;
			Thread.Sleep(100);
			Frissít();
		}

		public void Fordulj(double f) { irány += f; irány %= 360; Frissít(); }


		// Tollat(fel)
		// Tollat(le)
		// fel és le igazából logikai konstansok lesznek -- ebből a felhasználó semmit nem fog látni (mint Karesznél a jobbra és a balra!)
		public void Tollat(bool le) { rajzole = le; }

		private void Frissít()
		{
			szülőform.dlx.Text = hely.X.ToString();
			szülőform.dly.Text = hely.Y.ToString();
			szülőform.dli.Text = irány.ToString();
			avatarpb.Location = hely.ToPoint(szülőform.képkeret.Location, w, h);

			szülőform.képkeret.Image = szülőform.rajzlap; // innen töltődik be a legújabb változat

			szülőform.Refresh();
		}
	}
}
