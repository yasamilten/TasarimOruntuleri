using HastaneRandevuSistemi.Model;
using System;

namespace HastaneRandevuSistem.Services.Memento
{
	public class MementoDesignPattern
	{
		private Randevu _randevu { get; set; }
		public MementoDesignPattern(Randevu randevu)
		{
			this._randevu = randevu;
		}
		//t anında nesneyi tutacak metod
		public RandevuMemento Kaydet()
		{
			return new RandevuMemento
			{
				Id = _randevu.Id,
				DoktorId = _randevu.DoktorId,
				HastaId = _randevu.HastaId,
				HastaneId = _randevu.HastaneId,
				RandevuTarihi = _randevu.RandevuTarihi,
				AktifMi = _randevu.AktifMi
			};
		}


		//t anında nesneyi bize ulaştıracak metot
		public void OncekiniYukle(RandevuMemento Memento)
		{
			_randevu.Id = Memento.Id;
			_randevu.DoktorId = Memento.DoktorId;
			_randevu.HastaId = Memento.HastaId;
			_randevu.HastaneId =Memento.HastaneId;
			_randevu.RandevuTarihi = Memento.RandevuTarihi;
			_randevu.AktifMi = Memento.AktifMi;
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
	}
}
