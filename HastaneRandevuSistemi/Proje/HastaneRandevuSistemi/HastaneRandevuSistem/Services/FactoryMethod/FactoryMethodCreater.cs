using HastaneRandevuSistem.Model;
using HastaneRandevuSistemi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.Services.FactoryMethod
{
public	class FactoryMethodCreater
	{
		public Kullanici FactoryMethod(UserTypes userType)
		{

			Kullanici user = null;

			switch (userType)
			{
				case UserTypes.Hasta:
					user = new Hasta();
					break;
				case UserTypes.Yonetici:
					user = new Yonetici();
					break;
				case UserTypes.Doktor:
					user = new Doktor();
					break;
				default:
					break;
			}

			return user;
		}
	}
}
