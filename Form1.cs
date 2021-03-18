using System.Drawing;
using System.Windows.Forms;

namespace LogoKaresz
{
	public partial class Form1 : Form
	{
		void FELADAT()
		{
			// defaultkaresz.Kör(100); // bugos! sugár látszik, később megoldjuk!


			using (new Rajzol(defaultkaresz, false))
			{
				Előre(100);
			}

			Előre(100);


		}


	}
}
