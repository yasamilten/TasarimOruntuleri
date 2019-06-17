using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.Services.Mediator
{
	abstract class AbsHasta
	{

		//Hastanın hangi yonetici ile irtibata geçmesi gerektiğini tutması gerekir.
		public IYonetici IliskiliYonetici { get; set; }
		public string hastaTC { get; set; }
		public bool RandevuOnayi { get; set; }

		public void RandevuOnayIste()
		{
			//hastanın bağlı olduğu yöneticiden randevu onayı istiyor
			IliskiliYonetici.RandevuOnayVer(hastaTC);
		}

		public virtual void SetRandevuOnayi(bool onay)
		{
			//yönetici randevu onayı isteyen hastaya bu metot ile cevap verir.
			RandevuOnayi = onay;
			if (RandevuOnayi)
				Console.WriteLine("Randevu için onay verildi");
			else
				Console.WriteLine("Randevu için onay verilmedi.");
		}
	}

	//Mediator
	interface IYonetici
	{
		//Yoneticinin gerçekleştirmesi gereken işlemler
		void HastaKayit(AbsHasta hasta);
		void RandevuOnayVer(string hastaTC);


	}

	//ConcreteMediator yapısı
	class YasamHastanesi : IYonetici
	{
		//Yonetici(hastane) kendisine bağlı olan hastaların bilgisini tutmak zorunda ki isteklere buna göre cevap verebilsin.
		private List<AbsHasta> _HastaListe = new List<AbsHasta>();
		public void HastaKayit(AbsHasta _hasta)
		{
			_HastaListe.Add(_hasta);
			//Listeye eklenen AbsHasta nesnesine yöneticisinin bu sınıf olduğunu bildiriyoruz.
			_hasta.IliskiliYonetici = this;
		}


		public void RandevuOnayVer(string hastaTC)
		{
			bool onay = true;
			// eğer başka bir hastaya iniş izni verilmedi ise ilk randevu isteyen hastaya izin ver
			if (_HastaListe.Where(u => u.RandevuOnayi == true).Count() > 0)
				onay = false;
			//hastanın cevap alması için barındırdığı metoda cevap verilir.
			_HastaListe.Where(u => u.hastaTC == hastaTC).Single().SetRandevuOnayi(onay);
		}
	}

	//ConcreteColleague1
	class YasamHastanesiHastasi : AbsHasta
	{
		//SetRandevuOnayi metotu AbsHasta abstract sınıfından gelir.
		//Yonetici cevabı mu metot ile verir.
		public override void SetRandevuOnayi(bool onay)
		{
			Console.WriteLine("TC:{0} li hasta randevu onay bekliyor.", hastaTC);
			base.SetRandevuOnayi(onay);
		}
	}

}
