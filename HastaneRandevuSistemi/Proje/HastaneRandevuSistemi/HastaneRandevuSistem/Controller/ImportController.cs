using HastaneRandevuSistem.View;
using HastaneRandevuSistemi.Model;
using HastaneRandevuSistemi.Services.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.Controller
{
	public class ImportController
	{
		private Dosya dosyaModel;
		private ImportView importView;

	    ImportManager importManager;
		public ImportController(Dosya dosyaModel, ImportView importView)
		{
			this.dosyaModel = dosyaModel;
			this.importView = importView;
		}

		public void ImportFile(Dosya dosya)
		{
			importManager=ImportManager.GetInstance();
			var result=importManager.ImportFile(dosya);
			importView.PrintResultAddFile(result);
		}
	}
}
