using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.View
{
	public class ImportView
	{
		public void PrintResultAddFile(bool result)
		{
			if (result)
				Console.WriteLine("Dosya başarılı bir şekilde eklendi");
			else
				Console.WriteLine("Dosya eklenirken bir hata oluştu");
		}
	}
}
