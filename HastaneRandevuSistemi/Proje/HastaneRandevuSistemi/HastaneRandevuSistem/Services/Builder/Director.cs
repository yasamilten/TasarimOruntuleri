using HastaneRandevuSistem.Model;
using HastaneRandevuSistemi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.Services.Builder
{

	//Builder
	public abstract class UserBuilder
	{
		protected Kullanici user;

		public Kullanici User
		{
			get
			{
				return user;
			}
		}
		abstract public void SetUserType();
	}

	//ConcreteBuilder
	public class HastaConcreteBuilder : UserBuilder
	{
		public HastaConcreteBuilder()
		{
			user = new KullaniciSpecial();
		}

		public override void SetUserType() => user.UserType =(int) UserTypes.Hasta;
	}

	public class DoktorConcreteBuilder : UserBuilder
	{
		public DoktorConcreteBuilder()
		{
			user = new KullaniciSpecial();
		}

		public override void SetUserType() => user.UserType =(int) UserTypes.Doktor;
	}

	public class YoneticiConcreteBuilder : UserBuilder
	{
		public YoneticiConcreteBuilder()
		{
			user = new KullaniciSpecial();
		}

		public override void SetUserType() => user.UserType = (int)UserTypes.Yonetici;
	}


	//Director 
	public class Director
	{
		public void KullaniciOlustur(UserBuilder builder)
		{
			builder.SetUserType();
		}
	}

}
