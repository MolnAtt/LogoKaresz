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
		public Form1()
		{
			InitializeComponent();
			Avatar Karesz = new Avatar(this, new Pont(400, 400), 90);

			Karesz.Lépj(40);
			Thread.Sleep(100);
			Karesz.Fordulj(60);
			Thread.Sleep(100);
			Karesz.Lépj(40);
			Thread.Sleep(100);


		}

		private void startgomb_Click(object sender, EventArgs e)
		{

		}

		private void rajzlap_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
