using HastaneRandevuSistem.Model;
using HastaneRandevuSistem.Services.Builder;
using HastaneRandevuSistem.Services.FactoryMethod;
using HastaneRandevuSistem.Services.Iterotor;
using HastaneRandevuSistem.Services.Mediator;
using HastaneRandevuSistem.Services.Object_Pool;
using HastaneRandevuSistem.View;
using HastaneRandevuSistemi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.Controller
{
	public class UserController
	{
		UserView userView = new UserView();

		public void ChechkDoctorState(bool doctorIsCome)
		{
			userView.PrintDoctorState(doctorIsCome);
		}

		public void TakePatientFromPool()
		{

			Console.WriteLine("Havuzun boyutu {0}", PatientPool.Instance.Size);

			Console.WriteLine("PatientPooldan bir nesne alıyoruz.");
			var patient1 = PatientPool.Instance.AcquireObject();
			patient1.Giris("123456789", "1234");


			Console.WriteLine("PatientPooldan aldığımız nesneyi bırakıyoruz");
			if (patient1 != null)
				PatientPool.Instance.ReleaseObject(patient1);


			//Burada havuzda bulunan tüm nesneler kullanılmaktadır.
			var patients = new List<Hasta>();
			for (int i = 0; i < PatientPool.Instance.Size; i++)
			{
				patients.Add(PatientPool.Instance.AcquireObject());
			}

			Console.WriteLine("PatientPool'da bulunan tüm nesneler listeye eklendi. Böylece havuzda hiç nesne kalmadı.");

			var nullPatient = PatientPool.Instance.AcquireObject();  //Havuzda bulunan tüm nesneleri kullandığımız için null döner.

			bool isNull = true;
			if (nullPatient != null)
				isNull = false;

			userView.PrintIsTherePatientInPool(isNull);

		}

		public void HastaIterasyon(PatientAggregate aggregate)
		{
			IIterator iterasyon = aggregate.CreateIterator();
			while (iterasyon.HasItem())
			{
				userView.PrintHastaList(iterasyon.CurrentItem().Tc, iterasyon.CurrentItem().Ad, iterasyon.CurrentItem().Soyad);
				iterasyon.NextItem();
			}
		}

		public void CreateByUserType(UserTypes userType)
		{
			FactoryMethodCreater create = new FactoryMethodCreater();
			var user = create.FactoryMethod(userType);
			user.Giris(user.Tc, user.Sifre);
			user.HesapBilgileriniYonet(user.ID);

			userView.PrintUserByType(Enum.GetName(typeof(UserTypes), userType));
		}

		public void CreateUserBuilder()
		{
			UserBuilder builder = new YoneticiConcreteBuilder();
			Director director = new Director();
			director.KullaniciOlustur(builder);
			builder.User.ToString();

			userView.PrintUserBuilder(Enum.GetName(typeof(UserTypes), UserTypes.Yonetici));

			builder = new HastaConcreteBuilder();
			director.KullaniciOlustur(builder);
			builder.User.ToString();

			userView.PrintUserBuilder(Enum.GetName(typeof(UserTypes), UserTypes.Hasta));


			builder = new DoktorConcreteBuilder();
			director.KullaniciOlustur(builder);
			builder.User.ToString();

			userView.PrintUserBuilder(Enum.GetName(typeof(UserTypes), UserTypes.Doktor));

		}

		public void CreaterMediator()
		{
			//ilk olarak hastaların bağlı olacağı Hastaneler(yöneticiler) oluşturulur
			IYonetici yonetici = new YasamHastanesi();
			//Hasta nesneleri oluşturulur.
			AbsHasta hasta_01 = new YasamHastanesiHastasi { hastaTC = "1234567890" };
			AbsHasta hasta_02 = new YasamHastanesiHastasi { hastaTC = "9876543210" };


			//hasta nesneleri Yonetici nesnesine kayıt ettirilir.
			//Hasta nesnesindeki IliskiliYonetici nesnesi yoneticş nesnesindeki HastaKayit metodunda yapılır.
			yonetici.HastaKayit(hasta_01);
			yonetici.HastaKayit(hasta_02);

			//sadece ilk randevu isteyene o saatteki randevu verilir.
			hasta_01.RandevuOnayIste();
			hasta_02.RandevuOnayIste();
		}
	}
}
