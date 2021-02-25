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
			avatarpb.Location = hely.ToPoint();
			avatarpb.BackColor = Color.Blue;
			szülőform.Controls.Add(avatarpb);
			avatarpb.Size = new Size(10, 10);
			avatarpb.BringToFront();


		}
	}
}
