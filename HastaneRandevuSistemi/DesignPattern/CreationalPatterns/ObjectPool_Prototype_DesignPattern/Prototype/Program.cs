using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    class Program
    {
        //Bu tasarım deseni sayesinde yeni user tanımlarında New anahtar sözcüğünü kullanmaya gerek yoktur. 
        static void Main(string[] args)
        {
            List<User> users = new List<User>();
            User user1 = new User(User.UserTypes.Patient);
            users.Add(user1);

            User user2 = user1.Copy() as User;
            user2.UserType = User.UserTypes.Doctor;
            users.Add(user2);

            User user3 = user1.Copy() as User;
            user3.UserType = User.UserTypes.Manager;
            users.Add(user3);

            foreach (var user in users)
            {

                Console.WriteLine("User type :{0}", user.UserType.ToString());
            }

            //Nesneler birebir copyalanarak yeni referans değişkenine atanır. Bu kontrolden doğruluğu test edildi.
            if (ReferenceEquals(user1, user2))
                Console.WriteLine("user1 and user2 are same instance");
            else
                Console.WriteLine("user1 and user2 are not same instance");

           
            Console.ReadKey();
        }
    }


    //İlk yapmamız gereken Prototipi belirten Abstract Class’ını tasarlamaktır.
    public abstract class Prototype
    {
        public abstract Prototype Copy();
    }

    //Prototipe ait Class içerisinde bulunan Abstract türündeki Copy adlı metodumuz geriye Prototip tipinden bir değer döndürmektedir. 
    //Bu sayede Prototip Class’ımızdan miras alan Class’ların, Prototip Class’ımızın Abstract olması sebebiyle New ile yeni bir nesne oluşturulamayacağından dolayı kendisinin geriye döndürülmesini sağlayacaktır.
    //Prototip tanımımızı yaptıktan sonra artık üyelerimize ait bilgilerin tutulacağı sınıfımızı tasarlayabiliriz.

    //User sınıfında miras aldığımız Prototipimiz içerisinde bulunan Abstract türündeki Copy metodunu ezerek içerisinde sınıfı tüm özellikleriyle kopyalamayı sağlayan MemberwiseClone metodunu kullandık.
    //Ancak MemberwiseClone metodu Object tipinden veri döndürdüğü için ve bizim döndürmemiz gereken tip Class’ın kendi tipi olması gerektiğinden dolayı as User komutu ile bu problemi çözüyoruz.
    public class User : Prototype
    {
        public enum UserTypes
        {
            Patient,
            Doctor,
            Manager
        }

        public UserTypes UserType { get; set; }

        public User(UserTypes userType)
        {
            UserType = userType;
        }
        public override Prototype Copy()
        {
            return this.MemberwiseClone() as User;
        }
    }
}
