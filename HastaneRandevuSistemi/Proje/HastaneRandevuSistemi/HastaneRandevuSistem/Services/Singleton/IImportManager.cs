using HastaneRandevuSistemi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistemi.Services.Singleton
{
	public interface IImportManager
	{
	  bool ImportFile(Dosya dosya);
	}
}
