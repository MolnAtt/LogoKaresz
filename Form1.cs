using System.Drawing;
using System.Windows.Forms;


namespace LogoKaresz
{
	public partial class Form1 : Form
	{
		void FELADAT()
		{

			double méret = 100;
			for (int i = 0; i < 4; i++)
			{
				Előre(méret);
				Jobbra(90);
			}
			Tollat(fel);
			Jobbra(45);
			Előre(méret);
			Vödör(Color.Red);
		}
	}
}
