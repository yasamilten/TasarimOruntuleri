using HastaneRandevuSistem.Model;
using HastaneRandevuSistem.View;
using HastaneRandevuSistemi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.Controller
{
	public class CopyController
	{
		CopyView copyView = new CopyView();

		public void CopyUser(Kullanici kullanici)
		{
			Kullanici user1 = kullanici;

			Kullanici user2 = user1.ShallowCopy() as Kullanici;
			Kullanici user3 = user1.DeepCopy() as Kullanici;

			Console.WriteLine("Original");
			Console.Write("User 1: ");
			copyView.ShowWithFormat(user1.ID, user1.Ad, Enum.GetName(typeof(UserTypes), user1.UserType));
			Console.WriteLine("Shallow copy");
			Console.Write("User 2: ");
			copyView.ShowWithFormat(user2.ID,user2.Ad, Enum.GetName(typeof(UserTypes), user2.UserType));
			Console.WriteLine("Deep copy");
			Console.Write("User 3: ");
			copyView.ShowWithFormat(user3.ID, user3.Ad, Enum.GetName(typeof(UserTypes), user3.UserType));
			Console.WriteLine();

			user1.ID = 2;
			user1.Ad = "Ahmet";
			user1.UserType = (int)UserTypes.Doktor;


			user2.Ad = "Ali";
			user3.UserType = (int)UserTypes.Yonetici;

			Console.WriteLine("After changes\n");
			Console.WriteLine("Original");
			Console.Write("User 1: ");
			copyView.ShowWithFormat(user1.ID, user1.Ad, Enum.GetName(typeof(UserTypes), user1.UserType));
			Console.WriteLine("Shallow copy");
			Console.Write("User 2: "); //reference values have changed
			copyView.ShowWithFormat(user2.ID, user2.Ad, Enum.GetName(typeof(UserTypes), user2.UserType));
			Console.WriteLine("Deep copy");
			Console.Write("User 3: "); //everything was kept the same
			copyView.ShowWithFormat(user3.ID, user3.Ad, Enum.GetName(typeof(UserTypes), user3.UserType));

		}
	}
}
