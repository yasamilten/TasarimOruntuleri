using HastaneRandevuSistemi.Database;
using HastaneRandevuSistemi.Model;
using System;

namespace HastaneRandevuSistemi.Services.Singleton
{
	public class ImportManager : IImportManager
	{
		private static ImportManager _instance;
		private ImportManager()
		{

		}

		public static ImportManager GetInstance()
		{
			if (_instance == null)
				_instance = new ImportManager();

			return _instance;
		}

		public bool ImportFile(Dosya dosya)
		{
			bool state = true;

			if (dosya == null)
				state = false;
			else
			{
				HastaneRandevuSistemiContext.dosyas.Add(dosya);
				state = true;
			}

			return state;

		}
	}
}
