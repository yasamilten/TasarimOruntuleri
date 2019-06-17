using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistemi.Model
{
	public class Randevu
	{
		public int Id { get; set; }
		public int HastaId { get; set; }
		public DateTime RandevuTarihi { get; set; }
		public int HastaneId { get; set; }
		public int DoktorId { get; set; }
		public bool AktifMi { get; set; }

	}

}
