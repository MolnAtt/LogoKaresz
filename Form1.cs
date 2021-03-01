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


		void FELADAT()
		{
			Avatar Karesz = new Avatar(this, new Pont(400, 400), 90);
			Thread.Sleep(100);

			for (int i = 0; i < 8; i++)
			{
				Karesz.Lépj(50);
				Karesz.Fordulj(90);
			}

			for (int i = 0; i < 6; i++)
			{
				Karesz.Lépj(100);
				Karesz.Fordulj(120);
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
