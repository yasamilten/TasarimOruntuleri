using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoDesignPattern
{
    class MementoDesignPattern
    {
        static void Main(string[] args)
        {
            Il il = new Il { Id = 1, Adi = "İstanbul" };
            ILce ilce = new ILce { Id = 1, Adi = "Pendik", IlId = 1 };
            Hastane hastane = new Hastane { Id = 1, IlceId = 1, Adi = "XYZ Eğitim Araştırma Hastanesi" };
            Bolum bolum = new Bolum { Id = 1, BolumAdi = "Dahiliye" };
            Doktor doktor = new Doktor
            {
                Id = 1,
                TC = "11111111111",
                Ad = "Burak",
                Soyad = "Aslan",
                Telefon = "533333333",
                Cinsiyet = true,
                DogumTarihi = DateTime.Parse("10.10.1960"),
                DogumYeri = "Pendik",
                AnneAdi = "Aslı",
                BabaAdi = "Ahmet",
                UzmanlikAlanId = 1,
                AktifMi = true,
                Sifre = "1234k"
            };

            Doktor doktor2 = new Doktor
            {
                Id = 2,
                TC = "1113536211111",
                Ad = "Aslı",
                Soyad = "Dere",
                Telefon = "2343535223553",
                Cinsiyet = false,
                DogumTarihi = DateTime.Parse("10.10.1980"),
                DogumYeri = "Kadıköy",
                AnneAdi = "Merve",
                BabaAdi = "Mahmut",
                UzmanlikAlanId = 1,
                AktifMi = true,
                Sifre = "123467"
            };

            DoktorCalistigiHastaneler doktorCalistigiHastaneler = new DoktorCalistigiHastaneler
            {
                Id = 1,
                DoktorId = 1,
                HastaneId = 1,
                HastaneGirisTarihi = DateTime.Parse("01.02.2000"),
                HastaneCikisTarihi = (DateTime?)null
            };

            Hasta hasta = new Hasta
            {
                Id = 1,
                TC = "123456789330",
                Ad = "Gizem",
                Soyad = "Kara",
                Telefon = "5111111111",
                Cinsiyet = false,
                DogumTarihi = DateTime.Parse("04.02.2001"),
                DogumYeri = "Kartal",
                AnneAdi = "Ece",
                BabaAdi = "Ekrem",
                AktifMi = true,
                Sifre = "12345"

            };

            Randevu randevu = new Randevu { Id = 1, HastaId = 1, DoktorId = 1, RandevuTarihi = DateTime.Parse("01.02.2019"), AktifMi = false };
            Console.WriteLine("Randevu Id={0}\n Hasta Id={1}\n Doktor Id={2}\n Randevu Tarihi={3}", randevu.Id, randevu.HastaId, randevu.DoktorId, randevu.RandevuTarihi);
            Console.WriteLine("------------------------------------------------------------------");

            RandevuCareTaker Taker = new RandevuCareTaker();
            //T anında ilgili Randevu nesnesini kopyalıyoruz ve
            //CareTaker nesnesi içerisindeki Memento'ya bağlıyoruz.
            Taker.Memento = randevu.Kaydet();

            randevu = new Randevu { Id = 2, HastaId = 1, DoktorId = 2, RandevuTarihi = DateTime.Parse("11.11.2019"), AktifMi = true };
            Console.WriteLine("Randevu Id={0}\n Hasta Id={1}\n Doktor Id={2}\n Randevu Tarihi={3}", randevu.Id, randevu.HastaId, randevu.DoktorId, randevu.RandevuTarihi);
            Console.WriteLine("------------------------------------------------------------------");

            //T anında kopyaladığımız nesneye CareTaker üzerinden erişiyor
            //ve ilgili Originator nesnemize load ediyoruz.
            randevu.OncekiniYukle(Taker.Memento);
            Console.WriteLine("Randevu Id={0}\n Hasta Id={1}\n Doktor Id={2}\n Randevu Tarihi={3}", randevu.Id, randevu.HastaId, randevu.DoktorId, randevu.RandevuTarihi);

            Console.ReadKey();

        }
    }


    #region Entities
    public abstract class User
    {
        public int Id { get; set; }
        public string TC { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public bool Cinsiyet { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string DogumYeri { get; set; }
        public string BabaAdi { get; set; }
        public string AnneAdi { get; set; }
        public bool AktifMi { get; set; }
        public string Sifre { get; set; }

    }

    public class Hasta : User
    {

    }
    public class Doktor : User
    {
        public int UzmanlikAlanId { get; set; }
    }

    public class Yonetici
    {
        public int Id { get; set; }
    }

    //Originator Nesnesi
    public class Randevu
    {
        public int Id { get; set; }
        public int HastaId { get; set; }
        public int HastaneId { get; set; }
        public int DoktorId { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public bool AktifMi { get; set; }

        //t anında nesneyi tutacak metod
        public RandevuMemento Kaydet()
        {
            return new RandevuMemento
            {
                Id = this.Id,
                DoktorId = this.DoktorId,
                HastaId = this.HastaId,
                HastaneId = this.HastaneId,
                RandevuTarihi = this.RandevuTarihi,
                AktifMi = this.AktifMi
            };
        }


        //t anında nesneyi bize ulaştıracak metot
        public void OncekiniYukle(RandevuMemento Memento)
        {
            this.Id = Memento.Id;
            this.DoktorId = Memento.DoktorId;
            this.HastaId = Memento.HastaId;
            this.HastaneId = HastaneId;
            this.RandevuTarihi = Memento.RandevuTarihi;
            this.AktifMi = Memento.AktifMi;
        }


    }


    //Memento sınıfı : istenilen zaman aralığında objenin kaydetmesini istediğimiz alanları tanımlandı
    public class RandevuMemento
    {
        public int Id { get; set; }
        public int HastaId { get; set; }
        public int HastaneId { get; set; }
        public int DoktorId { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public bool AktifMi { get; set; }
    }

    //CareTaker nesnesi
    public class RandevuCareTaker
    {
        public RandevuMemento Memento { get; set; }
    }
    public class Bolum
    {
        public int Id { get; set; }
        public string BolumAdi { get; set; }
    }

    public class Hastane
    {
        public int Id { get; set; }
        public string Adi { get; set; }

        public int IlceId { get; set; }
    }

    public class Il
    {
        public int Id { get; set; }
        public string Adi { get; set; }
    }

    public class ILce
    {
        public int Id { get; set; }
        public int IlId { get; set; }
        public string Adi { get; set; }
    }

    public class DoktorCalistigiHastaneler
    {
        public int Id { get; set; }
        public int DoktorId { get; set; }
        public int HastaneId { get; set; }
        public DateTime HastaneGirisTarihi { get; set; }
        public DateTime? HastaneCikisTarihi { get; set; }
    }
    #endregion

    #region Memento Design Pattern
    /*
     Memento Design Pattern, elimizdeki mevcut nesnenin herhangi bir T anındaki durumunu kayda alarak,
     sonradan oluşabilecek değişiklikler üzerine tekrardan o kaydı elde etmemizi sağlayan bir desendir.
     Burada mevcut nesnenin özel bir halinden bahsetmemiz mümkündür. O hal ilgili tasarım kalıbı sayesinde sonradan da elde edilebilecektir.   
     */
    #region Originator
    /*    
      Yaratıcı, mucit, üretken olarak ifade edebileceğimiz bu nesne kopyası saklanacak olan nesneyi ifade etmektedir. 
      Bu nesne, kendi kopyasının oluşturulmasından sorumlu olduğu gibi geri yüklenmesinden de sorumludur.
    */
    #endregion

    #region Memento
    /*
     Kopyalanacak nesnenin hangi özelliklerinin tutulacağı, bir başka deyişle hangi değerlerinin işleneceğini belirttiğimiz nesnedir.
     */
    #endregion

    #region CareTaker
    /*   
        Bakıcı olarak nitelendirilen bu nesne, Memento referansını barındırmakta ve yapılacak tüm işlemlerin organizasyonunu sağlamaktadır.
     */
    #endregion
    #endregion


}
