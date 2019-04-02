using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HastaneRandevuSistemi.Services.User
{
    abstract class SoyutKullaniciFabrikasi
    {
        abstract public SoyutKullanici KullaniciUret();
    }

    class HastaFabrikasi : SoyutKullaniciFabrikasi
    {
        public override SoyutKullanici KullaniciUret()
        {
            return new Hasta();
        }
    }

    class DoktorFabrikasi : SoyutKullaniciFabrikasi
    {
        public override SoyutKullanici KullaniciUret()
        {
            return new Doktor();
        }
    }

    abstract class SoyutKullanici
    {
        abstract public void KullaniciyiGoster();
    }

    class Hasta : SoyutKullanici
    {
        public override void KullaniciyiGoster()
        {
            Console.WriteLine("Kullanici: Hasta");
        }
    }

    class Doktor : SoyutKullanici
    {
        public override void KullaniciyiGoster()
        {
            Console.WriteLine("Kullanici: Doktor");
        }

    }
    class KullaniciOtomasyon
    {
        private SoyutKullanici Kullanici;
        public KullaniciOtomasyon(SoyutKullaniciFabrikasi fabrika)
        {
            Kullanici = fabrika.KullaniciUret();
        }
    }

    class Kullanici
    {
        public static void Main()
        {
            SoyutKullaniciFabrikasi hasta = new HastaFabrikasi();
            KullaniciOtomasyon hastaOtomasyon = new KullaniciOtomasyon(hasta);

            SoyutKullaniciFabrikasi doktor = new DoktorFabrikasi();
            KullaniciOtomasyon doktorOtomasyon = new KullaniciOtomasyon(doktor);
        }
    }

}


