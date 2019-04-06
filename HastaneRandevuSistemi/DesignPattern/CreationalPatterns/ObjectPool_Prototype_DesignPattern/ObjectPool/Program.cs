using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ObjectPool
{

    public abstract class Patient
    {
        public abstract void Login();
    }

    internal class RequestPatient : Patient
    {
        public override void Login()
        {
            Console.WriteLine("Patient is logined");
        }
    }


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
        private const int defaultSize = 1000;
        private ConcurrentBag<Patient> _bag = new ConcurrentBag<Patient>();
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
        public Patient AcquireObject()
        {
            if (!_bag.TryTake(out Patient item))
            {
                lock (_lockObject)  //thread güvenliğini sağlamak için kullanılır. Yapılan iş bitmeden başka bir thread işe başlayamayacak.

                {
                    if (item == null)
                    {
                        if (_counter >= _currentSize)
                            return null;

                        item = new RequestPatient();

                        _counter++;

                    }
                }

            }

            return item;
        }


        //ReleaseObject metotu ile de almış olduğumuz objeleri sisteme geri iade ederek yeniden kullanıma sunuyoruz.Geri bırakılmadığı taktirde, kaynakların doğru kullanımı gerçekleşemeyecektir.
        public void ReleaseObject(Patient item)
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
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Havuzun boyutu {0}", PatientPool.Instance.Size);

            Console.WriteLine("Patient sınıfı ediniyoruz.");
            var patient1 = PatientPool.Instance.AcquireObject();
            patient1.Login();

            Console.WriteLine("Havuzun boyutu {0}", PatientPool.Instance.Size);

            Console.WriteLine("Patient'ı geri bırakıyoruz");
            if (patient1 != null)
                PatientPool.Instance.ReleaseObject(patient1);


            //Burada havuzda bulunan tüm nesneler kullanılmaktadır.
            var patients = new List<Patient>();
            for (int i = 0; i < PatientPool.Instance.Size; i++)
            {
                patients.Add(PatientPool.Instance.AcquireObject());
            }

            Console.WriteLine("PatientPool'da bulunan tüm objeler listeye eklendi. Böylece havuzda hiç obje kalmadı.");

            var nullPatient = PatientPool.Instance.AcquireObject();  //Havuzda bulunan tüm nesneleri kullandığımız için null döner.

            if (nullPatient == null)
                Console.WriteLine("Patient poolda hiç nesne bulunmamaktadır.");


            #region Yeni nesne oluştururken havuzumuzda nesne yoksa ,havuzun boyutunu bir artırarak yeni nesneyi kullanabiliriz.
            //Console.WriteLine("Havuzun boyutunu arttırıyoruz");
            //PatientPool.Instance.IncreaseSize();

            //Console.WriteLine("Yeni bir Patient sınıfı ediniyoruz.");
            //var newPatient = PatientPool.Instance.AcquireObject();

            //newPatient.Connect();

            //Console.WriteLine("Edindiğimiz sınıfı geri veriyoruz.");
            //if (newPatient != null)
            //    PatientPool.Instance.ReleaseObject(newPatient);

            #endregion

            Console.WriteLine("Listedeki tüm Patient sınıflarını geri bırakıyoruz.");

            foreach (var item in patients)
                PatientPool.Instance.ReleaseObject(item);

            Console.ReadKey();
        }
    }
}

#region  ConcurrentBag<T>

/*
Concurrent Collections deyince aklımıza Thread-Safe koleksiyon tipleri gelmelidir. 
Bu sınıf sırasız bir yapıda nesneleri bir koleksiyon içinde barındırır.
Bu sınıfı ancak elemanları işlerken hangi sırada işlediğinizin kesinlikle önemi olmadığı durumlarda kullanmak  fayda verecektir.
Zira, bu sınıf gerek yeni bir eleman eklerken gerekse koleksiyondan bir eleman alırken iş parçacıkları arasında hemen hemen hiç bir zaman bir yarış yada çekişme olmaksızın çalışabilmelerini sağlamaktadır.
Aslında içeride bu sınıfla çalışan her iş parçacığı için ayrı bir liste oluşturulmaktadır.
Bu sayede de normal şartlar altında her iş parçacığı kendine ait liste üzerinde çalıştığından iş parçacıkları arası bir çekişme söz konusu olmamaktadır.
Normalde listeden bir eleman almak istediğinizde yığın yapısında olduğu gibi en son eklenen elemanı alırsınız.
Ama bir fark vardır ki o da aldığınız bu eleman o iş parçacığında eklenen son elemandır. 
Eğer ki o iş parçacığından eklenen eleman sayısı sıfır ise bu durumda rastgele olarak diğer iş parçacıklarından rastgele birinin listesindeki son elemanı almaya çalışacaktır. 
İşte sadece bu durumda iş parçacıkları arasında bir çekişme olmaktadır ki o da sadece (genel olarak) iki iş parçacığı arasında söz konusu olabilmektedir.
Yani eğer listeniz ile çalışan örneğin 5 iş parçacığınız var ise ve bunların hepsi bu listeye eleman ekleyip alıyorsa çok büyük ihtimalle bu iş parçacıkları ikili olarak bir çekişmeye gireceklerdir. 
Oysa bir kuyruk yada yığın listesi kullanıyor olursak tüm iş parçacıkları her zaman çekişme içinde olacaklardır.

Yeni bir eleman eklemek istediğiniz zamanlarda ise neredeyse hiç bir zaman iş parçacıkları arasında bir çekişme söz konusu olmamaktadır (ama buna rağmen yeni eleman ekleme kuyruk sınıfı ile kıyaslandığında yavaş olmaktadır). 
Aynı şeyi kuyruk (queue) yada yığın (stack) yapıları için söylemek söz konusu değildir. 
Bu yeni koleksiyonda eğer bir iş parçacığınız eklediğinden daha fazla sayıda eleman işlemeyecek ise okuma işlemi kesinlikle çok efektif ve hızlı çalışacaktır.

ConcurrentBag'ler paralel işlemleriniz çoğunlukla ekleme yaptığı pozisyonlarda veya okuma  ve yazma işlemleri iş parçacıklarınızda dengeli ise çok faydalı olacaktır. 
Ama unutmayınız ki okuma yaparken ya en son eklenen elemanı yada bir başka iş parçacığı tarafından en son eklenen elemanı alacaksınız. 
Yani eğer hangi elemanı işlediğiniz önemli değilse ve yukarıda bahsettiğim mantıkta bir programınız var ise mutlaka bu koleksiyonu kullanmalısınız. 

*/
#endregion

#region TryTake
//This returns, in the out parameter, the most recently added element. It removes the element from the contents.
#endregion

#region Thread-safe singleton
/*
 Herhangi bir thread paylaşılan nesne ile işi bitene kadar nesneyi kilitlemektedir ve her thread nesnenin instance’ının oluşturulup oluşturulmadığını 
 her defasında kontrol etmektedir.Bu durum arka planda bellek bariyerlerini ilgilendirir ve bu durumda sadece bir thread’in tek instance oluşturduğundan 
 emin olunduğu durumdur.Ne yazık ki her instance çağırma durumunda kilitleme (lock) işlemi gerçekleşeceğinden yüksek ölçüde performans kaybı gözlemlenir.
     
     */
#endregion

