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
	public partial class Form1 : Form
	{
		public Bitmap rajzlap;
		const bool fel = false;
		const bool le = true;

		void FELADAT()
		{
			Avatar Karesz = new Avatar(this, new Pont(400, 400), 90);
			Thread.Sleep(100);

			for (int i = 0; i < 4; i++)
			{
				Karesz.Lépj(50);
				Karesz.Jobbra(90);
			}

			Karesz.Tollat(fel);

			Karesz.Balra(90);

			Karesz.Lépj(50);

			Karesz.Tollat(le);

			for (int i = 0; i < 3; i++)
			{
				Karesz.Lépj(100);
				Karesz.Jobbra(120);
			}

		}

		public Form1()
		{
			InitializeComponent();
			rajzlap = new Bitmap(this.Width, this.Height);

		}

		private void startgomb_Click(object sender, EventArgs e)
		{
			FELADAT();
		}

		private void rajzlap_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
