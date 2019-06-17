using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistemi.Model
{
	public class DoktorCalistigiHastaneler
	{
		public int Id { get; set; }
		public int DoktorId { get; set; }
		public int HastaneId { get; set; }
		public DateTime HastaneGirisTarihi { get; set; }
		public DateTime? HastaneCikisTarihi { get; set; }
	}
}
