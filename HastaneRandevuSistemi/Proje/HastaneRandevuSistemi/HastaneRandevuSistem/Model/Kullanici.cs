using HastaneRandevuSistem.Model;
using HastaneRandevuSistem.Services.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistemi.Model
{
	public abstract class Kullanici:IPrototype
	{
		public int ID { get; set; }
		public string Tc { get; set; }
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

		public int UserType { get; set; }

		public abstract void HesapBilgileriniYonet(int kullaniciId);
		public abstract  void Giris(string tc, string sifre);

		public Kullanici ShallowCopy()
		{
			return (Kullanici)this.MemberwiseClone();
		}

		public Kullanici DeepCopy()
		{
			Kullanici clone = (Kullanici)this.MemberwiseClone();
			clone.Ad = String.Copy(Ad);
			return clone;
		}
	}
}
