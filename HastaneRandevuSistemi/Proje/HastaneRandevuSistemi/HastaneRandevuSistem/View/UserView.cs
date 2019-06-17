using HastaneRandevuSistemi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.View
{
	public class UserView
	{
		public void PrintDoctorState(bool doctorIsCome)
		{
			if (!doctorIsCome)
				Console.WriteLine("\nDoktor hastaneye gelmediğin için, ilgili yerlere mesaj gönderildi");
		}

		public void PrintIsTherePatientInPool(bool isNull)
		{
			if (isNull)
				Console.WriteLine("Patient poolda hiç nesne bulunmamaktadır. Lütfen bekleyiniz");
		}

		public void PrintHastaList(string tc,string ad,string soyad)
		{
			Console.WriteLine("{0} - {1} {2}", tc, ad,soyad);
		}

		public void PrintUserByType(string userType)
		{
			Console.WriteLine("{0} oluşturuldu",userType);
		}

		public void PrintUserBuilder(string userType)
		{
			Console.WriteLine("{0} created",userType);
		}
	}
}
