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

		void Nyírfa(double hossz, int mélység)
		{
			if (mélység>0)
			{
				Előre(hossz/2);
				Balra(30);
					Nyírfa(hossz / 2, mélység - 1);
				Jobbra(30);
				// 1. kisnyírfa kész
				Előre(hossz / 4);
				Balra(30);
					Nyírfa(hossz / 4, mélység - 2);
				Jobbra(30);
				// mininyírfa kész
				Előre(hossz / 4);
				Jobbra(30);
					Nyírfa(hossz / 2, mélység - 1);
				Balra(30);
				// 2. kisnyírfa kész
					Nyírfa(hossz / 2, mélység - 1);
				// 3. kisnyírfa kész
				Hátra(hossz);
			}
		}


		void Pitypang2(double hossz, int mélység)
		{
			if (mélység > 0)
			{
				Előre(hossz);
				Balra(22.5);
				Pitypang3(.75 * hossz, mélység - 1);
				Jobbra(45);
				Pitypang3(.75 * hossz, mélység - 1);
				Balra(22.5);
				Hátra(hossz);
			}
		}

		void Pitypang3(double hossz, int mélység)
		{
			if (mélység > 0)
			{
				Előre(hossz);
				Balra(45);
	 				Pitypang2(.75 * hossz, mélység - 1);
				Jobbra(45);
					Pitypang2(.75 * hossz, mélység - 1);
				Jobbra(45);
					Pitypang2(.75 * hossz, mélység - 1);
				Balra(45);
				Hátra(hossz);
			}
		}



		void FELADAT()
		{
			//			Fa(Karesz, 150, 5);
			//			Fa(Karesz, 150, .5d, 7);

			// Nyírfa(150, 5);

			Pitypang3(150, 5);

		}
	}
}
