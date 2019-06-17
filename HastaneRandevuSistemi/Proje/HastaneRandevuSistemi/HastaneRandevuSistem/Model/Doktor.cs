using HastaneRandevuSistem.Model;
using HastaneRandevuSistem.Services.Observer;
using HastaneRandevuSistemi.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HastaneRandevuSistemi.Model
{
	public class Doktor:Kullanici
	{
		public int UzmanlikAlanId { get; set; }

		public string SicilNo { get; set; }

		public override void Giris(string tc, string sifre)
		{
			Console.WriteLine("Doktor giriş yaptı");
		}

		public override void HesapBilgileriniYonet(int kullaniciId)
		{
			Console.WriteLine("Doktor hesap bilgilerini güncelliyor.");
		}

		public IEnumerable<Randevu> RandevulariGoruntule(int doktorId)
		{
			return HastaneRandevuSistemiContext.randevus.Where(x => x.DoktorId == doktorId).ToList();
		}

		public Hasta RandevuListesindeArama(string hastaTc)
		{
			return HastaneRandevuSistemiContext.hastas.Where(x => x.Tc == hastaTc).FirstOrDefault();
		}

		public override string ToString()
		{
			Console.WriteLine($"Giriş : {Enum.GetName(typeof(UserTypes), UserType)}");
			return base.ToString();
		}

		bool hastaneyeGeldimi { get; set; }

		public bool HastaneyeGeldimi
		{
			get { return hastaneyeGeldimi; }
			set
			{
				if (value == false)
				{
					Notify();
					hastaneyeGeldimi = value;
				}
				else
					hastaneyeGeldimi = value;
			}
		}


		//Subject nesnesi kendisine abone olan gözlemcileri bu koleksiyonda tutacaktır.
		List<Observer> observers;

		public Doktor()
		{
			this.observers = new List<Observer>();
		}
		//Gözlemci ekle
		public void AddObserver(Observer observer)
		{
			observers.Add(observer);
		}


		//Herhangi bir güncelleme olursa ilgili gözlemcilere haber verilecek
		public void Notify()
		{
			observers.ForEach(g =>
			{
				g.SendMessage(SicilNo, Ad, Soyad);
			});
		}
	}

}
