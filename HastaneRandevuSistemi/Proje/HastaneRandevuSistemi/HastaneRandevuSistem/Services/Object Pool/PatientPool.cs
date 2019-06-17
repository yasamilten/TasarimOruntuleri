using HastaneRandevuSistemi.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.Services.Object_Pool
{
	//PatientPool sınıfı thread-safe Singleton Pattern’i sağlamaktadır. Bu sayede proje üzerinde sadece tek bir PatientPool sınıfı kullanabilir halde olacaktır.

	//sınıf tamamen thread-safe olarak kodlanmıştır.
	//Bunu sağlamak için ConcurrentBag<T> adlı.NET sınıfı ile birlikte AcquireObject ve IncreaseSize metotlarını lock kullanarak thread-safe yapmış bulunuyoruz.
	public class PatientPool
	{
		//Lazy = Nesne kullanılmadığı sürece oluşturulmaz anlamına gelmektedir
		private static Lazy<PatientPool> instance = new Lazy<PatientPool>(() => new PatientPool());
		public static PatientPool Instance { get; } = instance.Value;
		public int Size { get { return _currentSize; } }
		public int TotalObject { get { return _counter; } }

		//Sınıfımızda maksimum üretilebilecek Patient sayısını ayarlamamız için _currentSize değişkeni bulunmaktadır.
		//Havuzun başlangıç boyutu 1000 olarak belirlendi ancak  bu değişebilir.
		private const int defaultSize = 1000; //test edebilmek için düşük boyutlandırıldı
		private ConcurrentBag<Hasta> _bag = new ConcurrentBag<Hasta>();
		private volatile int _currentSize;
		private volatile int _counter;
		private static readonly object _lockObject = new object(); //Kod bloğunu kilitlemek için kullanıyoruz.

		private PatientPool()
			: this(defaultSize)
		{
		}
		private PatientPool(int size)
		{
			_currentSize = size;
		}

		// AcquireObject :öncelikle _bag listemizden obje almaya çalışıyoruz.
		//_bag.TryTake(out Client item) kodu ile aldığımız objenin durumunu kontrol ediyoruz eğer obje doğru ise objemizi döndürüyoruz eğer obje yok ise havuzun durumuna bakarak yeni bir obje oluşturuyoruz yada null pointer dönderiyoruz.
		public Hasta AcquireObject()
		{
			if (!_bag.TryTake(out Hasta item))
			{
				lock (_lockObject)  //thread güvenliğini sağlamak için kullanılır. Yapılan iş bitmeden başka bir thread işe başlayamayacak.

				{
					if (item == null)
					{
						if (_counter >= _currentSize)
							return null;

						item = new Hasta();

						_counter++;

					}
				}

			}

			return item;
		}


		//ReleaseObject metotu ile de almış olduğumuz objeleri sisteme geri iade ederek yeniden kullanıma sunuyoruz.Geri bırakılmadığı taktirde, kaynakların doğru kullanımı gerçekleşemeyecektir.
		public void ReleaseObject(Hasta item)
		{
			_bag.Add(item);
		}

		//IncreaseSize metotu havuzun boyutunu büyütmek için kullanılmaktadır.Tabii ki sisteminizin gerekliliklerine göre bu metot değiştirilebilir.
		public void IncreaseSize()
		{
			lock (_lockObject)
			{
				_currentSize++;
			}
		}
	}
}
