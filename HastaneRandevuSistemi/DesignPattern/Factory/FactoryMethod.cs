using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev_06
{
    public abstract class User
    {
        public abstract void Login();
        public abstract void ManageUserInfo();
    }

    public class Yonetici : User
    {

        public override void Login()
        {
            Console.WriteLine("Yönetici giriş yaptı");
        }

        public override void ManageUserInfo()
        {
            Console.WriteLine("Yönetici hesap bilgilerini güncelliyor.");
        }
    }

    public class Hasta : User
    {
        public override void Login()
        {
            Console.WriteLine("Hasta giriş yaptı");
        }

        public override void ManageUserInfo()
        {
            Console.WriteLine("Hasta hesap bilgilerini güncelliyor.");
        }
    }

    public class Doktor : User
    {
        public override void Login()
        {
            Console.WriteLine("Doktor giriş yaptı");
        }

        public override void ManageUserInfo()
        {
            Console.WriteLine("Doktor hesap bilgilerini güncelliyor.");
        }
    }

    public enum UserType
    {
        Hasta,
        Yonetici,
        Doktor
    }

    public class Creater
    {
        public User FactoryMethod(UserType userType)
        {

            User user = null;

            switch (userType)
            {
                case UserType.Hasta:
                    user = new Hasta();
                    break;
                case UserType.Yonetici:
                    user = new Yonetici();
                    break;
                case UserType.Doktor:
                    user = new Doktor();
                    break;
                default:
                    break;
            }

            return user;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Creater create = new Creater();
            var user=create.FactoryMethod(UserType.Hasta);
            user.Login();
            user.ManageUserInfo();

            user=create.FactoryMethod(UserType.Yonetici);
            user.Login();
            user.ManageUserInfo();

            user =create.FactoryMethod(UserType.Hasta);
            user.Login();
            user.ManageUserInfo();
            Console.Read();
        }
    }
}
