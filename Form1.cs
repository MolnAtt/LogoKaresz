using System.Windows.Forms;

namespace LogoKaresz
{
	public partial class Form1 : Form
	{

		void Fa(Avatar Karesz, double hossz, int mélység)
		{
			if (mélység!=0)
			{
				Karesz.Előre(hossz);
				Karesz.Balra(60);
				Fa(Karesz, 0.7 * hossz, mélység - 1);
				Karesz.Jobbra(120);
				Fa(Karesz, 0.7 * hossz, mélység - 1);
				Karesz.Balra(60);
				Karesz.Hátra(hossz);
			}
		}
		void Fa(Avatar karesz, double hossz, double arány, int mélység)
		{
			if (mélység == 0)
			{
				return;
			}
			karesz.Előre(hossz);
			karesz.Balra(60);
			Fa(karesz, hossz * arány, arány, mélység - 1);
			karesz.Jobbra(120);
			Fa(karesz, hossz * arány, arány, mélység - 1);
			karesz.Balra(60);
			karesz.Hátra(hossz);
		}

		void Nyírfa(Avatar karesz, double hossz, int mélység)
		{

		}
		void FELADAT()
		{
			defaultkaresz.Pihi(100);

			//			Fa(Karesz, 150, 5);
			//			Fa(Karesz, 150, .5d, 7);

			Fa(defaultkaresz, 150, .5d, 7);



		}
	}
}
