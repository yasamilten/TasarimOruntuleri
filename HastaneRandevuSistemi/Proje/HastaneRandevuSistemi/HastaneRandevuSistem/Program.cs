using HastaneRandevuSistem.Controller;
using HastaneRandevuSistem.Model;
using HastaneRandevuSistem.Services.Iterotor;
using HastaneRandevuSistem.Services.Observer;
using HastaneRandevuSistem.View;
using HastaneRandevuSistemi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HastaneRandevuSistem.Services.Memento.MementoDesignPattern;

namespace HastaneRandevuSistemi
{
	class Program
	{
		static void Main(string[] args)
		{
			UserController userController = new UserController();
			RandevuController randevuController = new RandevuController();
			DatabaseController databaseController = new DatabaseController();
			
			Hasta hasta = new Hasta
			{
				ID = 1,
				Ad = "Ali",
				Soyad = "Karat",
				BabaAdi = "Kerem",
				AnneAdi = "Ayşe",
				Cinsiyet = true,
				Tc = "12345678",
				Sifre = "1234",
				UserType = (int)UserTypes.Hasta,
				AktifMi = true
			};
			

			#region Singleton Design Pattern
			Console.WriteLine("Singleton Design Pattern");
			Dosya dosya = new Dosya { Id = 1, HastaId = 1, DosyaAdi = "Kan tahlili" };
			ImportView importView = new ImportView();
			ImportController importController = new ImportController(dosya, importView);
			importController.ImportFile(dosya);

			#endregion

			Console.WriteLine("\n ************************************** \n");

			#region Prototype Design Pattern
			Console.WriteLine("");
			Console.WriteLine("Prototype Design Pattern");

			Kullanici user1 = new Hasta();
			user1.ID = 2;
			user1.Ad = "Yaşam";
			user1.UserType = (int)UserTypes.Hasta;
			CopyController copyController = new CopyController();
			copyController.CopyUser(user1);
			#endregion

			Console.WriteLine("\n ************************************** \n");

			#region Observer Design Pattern
			Console.WriteLine("Observer Design Pattern");
			Doktor doctor = new Doktor();
			doctor.AddObserver(new ManagementObserver());
			doctor.AddObserver(new PatientObserver());


			doctor.SicilNo = "185112012";
			doctor.Ad = "Yaşam";
			doctor.Soyad = "İLTEN";
			doctor.HastaneyeGeldimi = false;

			
			userController.ChechkDoctorState(doctor.HastaneyeGeldimi);
			#endregion

			Console.WriteLine("\n ************************************** \n");
			#region Object Pool
			Console.WriteLine("Object Pool Design Pattern");
			userController.TakePatientFromPool();
			#endregion

			Console.WriteLine("\n ************************************** \n");
			#region Memento Design Pattern
			Console.WriteLine("Memento Design Pattern");
			Il il = new Il { Id = 1, Adi = "İstanbul" };
			Ilce ilce = new Ilce { Id = 1, Adi = "Pendik", IlId = 1 };
			Hastane hastane = new Hastane { Id = 1, IlceId = 1, Adi = "XYZ Eğitim Araştırma Hastanesi" };
			Bolum bolum = new Bolum { Id = 1, BolumAdi = "Dahiliye" };
			Doktor doktor = new Doktor
			{
				ID = 1,
				Tc = "11111111111",
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
				ID = 2,
				Tc = "1113536211111",
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

			Hasta hasta2 = new Hasta
			{
				ID = 1,
				Tc = "123456789330",
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
			randevuController.PrintHastaRendevu(randevu); 

			RandevuCareTaker taker = new RandevuCareTaker();
			randevuController.RandevuOncekiniKaydet(taker, randevu);
			

			randevu = new Randevu { Id = 2, HastaId = 1, DoktorId = 2, RandevuTarihi = DateTime.Parse("11.11.2019"), AktifMi = true };
			randevuController.PrintHastaRendevu(randevu);

			//T anında kopyaladığımız nesneye CareTaker üzerinden erişiyor
			//ve ilgili Originator nesnemize load ediyoruz.
			randevuController.RandevuOncekiniYukle(taker, randevu);
			randevuController.PrintHastaRendevu(randevu);

			#endregion

			Console.WriteLine("\n ************************************** \n");
			#region Mediator Design Pattern
			Console.WriteLine("Mediator Design Pattern");

			#endregion

			Console.WriteLine("\n ************************************** \n");

			#region Iterator Design Pattern
			Console.WriteLine("Iterator Design Pattern");
			PatientAggregate aggregate = new PatientAggregate();
			aggregate.Add(new Hasta { Tc = "12333333333", Ad = "Yaşam", Soyad = "İLTEN" });
			aggregate.Add(new Hasta { Tc = "45622222222", Ad = "Gizem", Soyad = "Koç" });
			aggregate.Add(new Hasta { Tc = "22222222222", Ad = "Aslı", Soyad = "Dere" });

			Console.WriteLine("Hasta Listesi");
			userController.HastaIterasyon(aggregate);
			#endregion

			Console.WriteLine("\n ************************************** \n");

			#region Factory Method Design Pattern
			Console.WriteLine("Factory method Design Pattern");
			userController.CreateByUserType(UserTypes.Hasta);
			userController.CreateByUserType(UserTypes.Yonetici);
			userController.CreateByUserType(UserTypes.Doktor);
			#endregion


			Console.WriteLine("\n ************************************** \n");

			#region Decorator Method Design Pattern	
			Console.WriteLine("Decorator  Design Pattern");
			Randevu randevu1 = new Randevu();
			randevuController.AddMesajMethod(randevu);
			#endregion

			Console.WriteLine("\n ************************************** \n");

			#region Mediator Design Pattern
			Console.WriteLine("Mediator  Design Pattern");
			userController.CreaterMediator();
			#endregion

			Console.WriteLine("\n ************************************** \n");

			#region Builder Design Pattern	
			Console.WriteLine("Builder Design Pattern");
			userController.CreateUserBuilder();
			#endregion

			Console.WriteLine("\n ************************************** \n");

			#region Abstract Factory Design Pattern	
			Console.WriteLine("Abstract Factory Design Pattern");
			databaseController.CreateDatabase();
			#endregion
			Console.Read();
		}
	}
}
