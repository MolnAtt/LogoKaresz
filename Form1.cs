using System.Drawing;
using System.Windows.Forms;

namespace LogoKaresz
{
	public partial class Form1 : Form
	{
		void FELADAT()
		{
			for (int i = 0; i < 11; i++)
			{
				Előre(50);
				Jobbra(360/11d);
			}
			Tollat(fel);
			Jobbra(45);
			Előre(100);
			Tölt(Color.Red);


		}
	}
}
