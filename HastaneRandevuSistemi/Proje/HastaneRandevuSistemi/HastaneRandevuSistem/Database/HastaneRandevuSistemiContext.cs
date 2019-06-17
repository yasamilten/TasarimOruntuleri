using HastaneRandevuSistemi.Model;
using System.Collections.Generic;

namespace HastaneRandevuSistemi.Database
{
	public static class HastaneRandevuSistemiContext
	{

		public static IList<Bolum> bolums = new List<Bolum>();
		public static IList<Doktor> doktors = new List<Doktor>();
		public static IList<DoktorCalistigiHastaneler> doktorCalistigiHastanelers = new List<DoktorCalistigiHastaneler>();
		public static IList<Hasta> hastas = new List<Hasta>();
		public static IList<Hastane> hastanes = new List<Hastane>();
		public static IList<Il> ils = new List<Il>();
		public static IList<Ilce> ilces = new List<Ilce>();
		public static IList<Randevu> randevus = new List<Randevu>();
		public static IList<Yonetici> yoneticis = new List<Yonetici>();
		public static IList<Dosya> dosyas = new List<Dosya>();

	}
}
