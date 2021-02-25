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

		public Avatar(Form1 szülőform, Pont hely, double irány)
		{
			this.szülőform = szülőform;
			this.hely = hely;
			this.irány = irány;

			avatarpb = new PictureBox();
			avatarpb.BackColor = Color.Blue;
			avatarpb.Size = new Size(10, 10);
			szülőform.Controls.Add(avatarpb);
			avatarpb.BringToFront();

			Frissít();
		}

		public void Lépj(double t)
		{
			Pont lépővektor = new Pont(irány,t, "polár"); // ez most polárkoordinátás kellene legyen!
			hely += lépővektor;
			Thread.Sleep(100);
			Frissít();
		}

		public void Fordulj(double f) { irány += f; irány %= 360; Frissít(); }

		private void Frissít()
		{
			szülőform.dlx.Text = hely.X.ToString();
			szülőform.dly.Text = hely.Y.ToString();
			szülőform.dli.Text = irány.ToString();
			avatarpb.Location = hely.ToPoint();
			szülőform.Refresh();
		}
	}
}
