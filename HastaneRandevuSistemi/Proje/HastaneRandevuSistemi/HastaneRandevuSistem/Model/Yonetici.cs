using HastaneRandevuSistemi.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistemi.Model
{
	public class Yonetici : Kullanici
	{
		public override void Giris(string tc, string sifre)
		{
			Console.WriteLine("Yönetici giriş yaptı");
		}

		public override void HesapBilgileriniYonet(int kullaniciId)
		{
			Console.WriteLine("Yönetici hesap bilgilerini güncelliyor.");
		}
		public IEnumerable<Doktor> DoktorlariListele()
		{
			return HastaneRandevuSistemiContext.doktors.Where(x => x.AktifMi == true).ToList();
		}

		public void DoktorSil(int doktorId)
		{
			HastaneRandevuSistemiContext.doktors.Where(x => x.ID == doktorId).FirstOrDefault().AktifMi = false;
		}

		public IEnumerable<Hasta> HastalariListele()
		{
			return HastaneRandevuSistemiContext.hastas.Where(x => x.AktifMi == true).ToList();
		}

		public void HastaSil(int hastaId)
		{
			HastaneRandevuSistemiContext.hastas.Where(x => x.ID == hastaId).FirstOrDefault().AktifMi = false;
		}


		public void RandevuIptalEt(int randevuId)
		{
			HastaneRandevuSistemiContext.randevus.Where(x => x.Id == randevuId).FirstOrDefault().AktifMi = false
				;
		}
	}
}
