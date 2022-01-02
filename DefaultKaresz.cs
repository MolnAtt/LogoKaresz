using System;
using System.Drawing;

namespace LogoKaresz
{
    partial class Form1
    {
        const bool fel = false;
        const bool le = true;
        private static Avatar defaultkaresz;


        int várakozás { get => defaultkaresz.varakozas; set => defaultkaresz.varakozas = value; }

        private double Irány { get => defaultkaresz.Irány; }
        private bool Kilépek_e_a_pályáról(double d) => defaultkaresz.Kilépek_e_a_pályáról(d);
        static private bool Kilépek_e_a_pályáról(Avatar a, double d) => a.Kilépek_e_a_pályáról(d);
        private void Előre(double d) => defaultkaresz.Előre(d);
        static private void Előre(Avatar a, double d) => a.Előre(d);
        private void Hátra(double d) => defaultkaresz.Hátra(d);
        static private void Hátra(Avatar a, double d) => a.Hátra(d);
        private void Jobbra(double d) => defaultkaresz.Jobbra(d);
        static private void Jobbra(Avatar a, double d) => a.Jobbra(d);
        private void Balra(double d) => defaultkaresz.Balra(d);
        static private void Balra(Avatar a, double d) => a.Balra(d);
        private void Fordulj(double d) => defaultkaresz.Fordulj(d);
        static private void Fordulj(Avatar a, double d) => a.Fordulj(d);
        private void Lépj(double d) => defaultkaresz.Lépj(d);
        static private void Lépj(Avatar a, double d) => a.Lépj(d);
        private void Pihi(int i) => defaultkaresz.Pihi(i);
        static private void Pihi(Avatar a, int i) => a.Pihi(i);
        private void Tollat(bool b) => defaultkaresz.Tollat(b);
        static private void Tollat(Avatar a, bool b) => a.Tollat(b);
        private void Tölt(Color c, bool beszólós = true) => defaultkaresz.Tölt(c, beszólós);
        static private void Tölt(Avatar a, Color c, bool beszólós = true) => a.Tölt(c, beszólós);
        void Tollszín(Color c) => defaultkaresz.Tollszín(c);
        static void Tollszín(Avatar a, Color c) => a.Tollszín(c);
        void Tollszín(int i) => defaultkaresz.Tollszín(i);
        static void Tollszín(Avatar a, int i) => a.Tollszín(i);
        void Tollvastagság(float v) => defaultkaresz.Tollvastagság(v);
        static void Tollvastagság(Avatar a, float v) => a.Tollvastagság(v);
        private void Ív(double f, double r) => defaultkaresz.Ív(f, r);
        private void Ív((double, double)fr) => defaultkaresz.Ív(fr.Item1, fr.Item2);
        static private void Ív(Avatar a, (double , double ) fr) => a.Ív(fr.Item1, fr.Item2);
        static private void Ív(Avatar a, double f, double r) => a.Ív(f, r);
        static void Ismétlés(int db, Action a) { for (int i = 0; i < db; i++) a(); } // Iteráció ciklusváltozó nélkül. Logoba való fordítás végett.
        /* Például
         
            Ismétlés(4, delegate () 
			{
				Előre(mérték);
				Jobbra(90);
			});
         */
        public class Frissítés : IDisposable
        {
            Környezet<bool, bool> k;
            public Frissítés(Avatar a, bool rajzoljone) => k = new Környezet<bool, bool>(a, delegate (Avatar av, bool fr) { av.Állandó_frissítés = fr; }, rajzoljone, delegate (Avatar av, bool fr) { av.Állandó_frissítés = fr; }, a.Állandó_frissítés);
            public Frissítés(bool fr) : this(defaultkaresz, fr) { }
            public void Dispose() { k.Dispose(); GC.SuppressFinalize(this); }
        }
        /* Például

            using (new Frissítés(false))
            {
                Előre(100);
            }
            Előre(100);

         */
        public class Rajzol : IDisposable
        {
            Környezet<bool, bool> k;
            public Rajzol(Avatar a, bool rajzoljone) => k = new Környezet<bool, bool>(a, Tollat, rajzoljone, Tollat, a.rajzole);
            public Rajzol(bool r) : this(defaultkaresz, r) { }
            public void Dispose() { k.Dispose(); GC.SuppressFinalize(this); }
        }
        /* Például

            using (new Rajzol(false))
            {
				Előre(100);
            }
			Előre(100);

         */
        public class Szín : IDisposable
        {
            Környezet<Color, Color> k;
            public Szín(Avatar a, Color új_szín) => k = new Környezet<Color, Color>(a, Tollszín, új_szín, Tollszín, a.toll.Color);
            public Szín(Color új_szín) : this(defaultkaresz, új_szín) { }
            public void Dispose() { k.Dispose(); GC.SuppressFinalize(this); }
        }
        /* Például
            using (new Szín2(Color.Red))
            {
				Előre(100);
            }
			Előre(100);
         */
        public class Vastagság : IDisposable
        {
            Környezet<float, float> k;
            public Vastagság(Avatar a, float új_vastagság) => k = new Környezet<float, float>(a, Tollvastagság, új_vastagság, Tollvastagság, a.toll.Width);
            public Vastagság(float új_vastagság) : this(defaultkaresz, új_vastagság) { }
            public void Dispose() { k.Dispose(); GC.SuppressFinalize(this); }
        }
        /* Például
            using (new Vastagság(5))
            {
				Előre(100);
            }
			Előre(100);
         */
        public class Vékonyít : IDisposable
        {
            Környezet<float, float> k;
            public Vékonyít(Avatar a, float szorzó) => k = new Környezet<float, float>(a, delegate (Avatar av, float sz) { av.toll.Width *= sz; }, szorzó, delegate (Avatar av, float sz) { av.toll.Width /= sz; }, szorzó);
            public Vékonyít(Avatar a, double szorzó) : this(a, (float)szorzó) { }
            public Vékonyít(float szorzó) : this(defaultkaresz, szorzó) { }
            public Vékonyít(double szorzó) : this(defaultkaresz, szorzó) { }
            public void Dispose() { k.Dispose(); GC.SuppressFinalize(this); }
        }
        /* Például
         	Tollvastagság(5);
            using (new Vékonyít(.5))
            {
				Előre(100);
            }
			Előre(100);
         */
        public class Átmenetileg : IDisposable
        {
            Környezet<double, double> k;
            public Átmenetileg(Action<Avatar, double> eljárás, double előjeles_mérték) => k = new Környezet<double, double>(eljárás, előjeles_mérték, eljárás, -előjeles_mérték);
            public Átmenetileg(Avatar a, Action<Avatar, double> eljárás, double előjeles_mérték) => k = new Környezet<double, double>(a, eljárás, előjeles_mérték, eljárás, -előjeles_mérték);
            public void Dispose() { k.Dispose();  GC.SuppressFinalize(this); }
        }
        /* Például

            using (new Átmenetileg(Balra, 50))
            {
                Előre(100);
            }

            vagy 

            using (new Átmenetileg(Balra, 50, defaultkaresz))
            {
                Előre(100);
            }

         */
        public class Környezet<T,R> : IDisposable
        {
            Avatar a;
            Action<Avatar, R> post_a;
            R post_p;
            public Környezet(Avatar a, Action<Avatar, T> pre_a, T pre_p, Action<Avatar, R> post_a, R post_p) { (this.a, this.post_a, this.post_p) = (a, post_a, post_p); pre_a(a, pre_p); }
            public Környezet(Action<Avatar, T> pre_a, T pre_p, Action<Avatar, R> post_a, R post_p) : this(defaultkaresz, pre_a, pre_p, post_a, post_p) { }
            public void Dispose() { post_a(a, post_p); GC.SuppressFinalize(this); }
        }
        /* Például:
        
            using (new Környezet<double,double>(Balra, 50, Jobbra, 50))
            {
				Előre(100);
            }

            vagy 

            using (new Környezet<double,double>(Balra, 50, Jobbra, 50, defaultkaresz))
            {
				Előre(100);
            }

         */
    }
}
