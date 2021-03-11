using System.Drawing;
using System.Windows.Forms;

namespace LogoKaresz
{
	public partial class Form1 : Form
	{
		void FELADAT()
		{
			for (int i = 0; i < 4; i++)
			{
				Előre(100);
				Jobbra(90);
			}
			Tollat(fel);
			Jobbra(45);
			Előre(100);
			Tölt(Color.Red);
		}
	}
}
