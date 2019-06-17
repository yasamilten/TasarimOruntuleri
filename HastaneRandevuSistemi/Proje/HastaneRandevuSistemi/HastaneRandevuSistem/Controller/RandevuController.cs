using HastaneRandevuSistem.Services.Decorator;
using HastaneRandevuSistem.Services.Memento;
using HastaneRandevuSistem.View;
using HastaneRandevuSistemi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HastaneRandevuSistem.Services.Memento.MementoDesignPattern;

namespace HastaneRandevuSistem.Controller
{
	public class RandevuController
	{
		RandevuView randevuView = new RandevuView();
		MementoDesignPattern mementoDesignPattern;
		public void PrintHastaRendevu(Randevu randevu)
		{
			randevuView.PrintHastaRendevu(randevu.Id, randevu.HastaId, randevu.DoktorId, randevu.RandevuTarihi, randevu.AktifMi);
		}

		public void RandevuOncekiniKaydet(RandevuCareTaker taker,Randevu randevu)
		{
			mementoDesignPattern = new MementoDesignPattern(randevu);
			//T anında ilgili Randevu nesnesini kopyalıyoruz ve
			//CareTaker nesnesi içerisindeki Memento'ya bağlıyoruz.
			taker.Memento = mementoDesignPattern.Kaydet();
			randevuView.PrintRandevuOncekiniKaydetResult();
		}

		public void RandevuOncekiniYukle(RandevuCareTaker taker, Randevu randevu)
		{
			mementoDesignPattern = new MementoDesignPattern(randevu);
			//T anında kopyaladığımız nesneye CareTaker üzerinden erişiyor
			//ve ilgili Originator nesnemize load ediyoruz.
			mementoDesignPattern.OncekiniYukle(taker.Memento);
			randevuView.PrintRandevuOncekiniYukleResult();
		}

		public void AddMesajMethod(Randevu randevu)
		{
			//decorator uygulanacak component nesnesi
			RandevuIslemler randevuIsl = new RandevuIslemler();
			//mesaj decorator nesnesine component i veriyoruz
			RandevuMesajOperation mHasta = new RandevuMesajOperation(randevuIsl);
			//decorator üzerinden component yeni metotlara sahip oluyor.
			mHasta.Ekle(randevu);
			mHasta.MesajGonder("Randevu kayıt edildi");
		}
	}
}
