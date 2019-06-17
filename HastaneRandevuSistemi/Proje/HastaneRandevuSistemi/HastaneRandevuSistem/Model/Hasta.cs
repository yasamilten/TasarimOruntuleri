using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HastaneRandevuSistem.Model;
using HastaneRandevuSistem.Services;
using HastaneRandevuSistemi.Database;

namespace HastaneRandevuSistemi.Model
{
	public class Hasta:Kullanici
	{
		public bool RandevuOnayi { get; set; }
		
		public override void Giris(string tc, string sifre)
		{
			Console.WriteLine("Hasta giriş yaptı");
		}

		public override void HesapBilgileriniYonet(int kullaniciId)
		{
			Console.WriteLine("Hasta hesap bilgilerini güncelliyor.");
		}

		public void RandevuAl(Randevu randevu)
		{
			HastaneRandevuSistemiContext.randevus.Add(randevu);
		}

		public IEnumerable<Randevu> RandevuGecmisiniGoruntule(int hastaId)
		{
			return HastaneRandevuSistemiContext.randevus.Where(x => x.HastaId == hastaId).ToList();
		}

		public void RandevuIptalEt(int randevuId)
		{
			HastaneRandevuSistemiContext.randevus.Where(x => x.Id == randevuId).FirstOrDefault().AktifMi = false;

		}
		public override string ToString()
		{
			Console.WriteLine($"Giriş : {Enum.GetName(typeof(UserTypes), UserType)}");

			return base.ToString();
		}
	}
}
