using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HastaneRandevuSistem.Services.Memento.MementoDesignPattern;

namespace HastaneRandevuSistem.View
{
	public class RandevuView
	{
		public void PrintHastaRendevu(int id, int hastaId, int doktorId, DateTime randevuTarihi, bool aktifMi)
		{
			Console.WriteLine("Randevu Id={0}\n Hasta Id={1}\n Doktor Id={2}\n Randevu Tarihi={3}", id, hastaId, doktorId, randevuTarihi, aktifMi);
			Console.WriteLine("------------------------------------------------------------------");
		}

		public void PrintRandevuOncekiniKaydetResult()
		{
			Console.WriteLine("Önceki randevu kaydedildi");
		}

		public void PrintRandevuOncekiniYukleResult()
		{
			Console.WriteLine("Önceki randevu yüklenildi");
		}
	}
}