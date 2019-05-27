using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverDesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Doctor doctor = new Doctor();
            doctor.AddObserver(new ManagementObserver());
            doctor.AddObserver(new PatientObserver());


            doctor.SicilNo = "185112012";
            doctor.Ad = "Yaşam";
            doctor.Soyad = "İLTEN";
            doctor.HastaneyeGeldimi = false;
            Console.ReadKey(true);
        }
    }

    abstract public class Observer
    {
        public abstract void SendMessage(string sicilNo,string firstName,string lastName);
    }

    public class ManagementObserver : Observer
    {
        public override void SendMessage(string sicilNo, string firstName, string lastName)
        {
            Console.WriteLine("Management\nSicil No: {0} \nDoctor {1} {2} didnt come to hospital, dont take appointment.\n",sicilNo,firstName,lastName);
        }


    }

    public class PatientObserver : Observer
    {
        public override void SendMessage(string sicilNo, string firstName, string lastName)
        {
            Console.WriteLine("Patient \nDoctor {0} {1}  didnt come to hospital,cancelled your appointment",firstName,lastName);
        }
    }


    public class Doctor
    {
        public int Id { get; set; }
        public string  SicilNo { get; set; }
        public string TC { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }


        bool hastaneyeGeldimi { get; set; }

        public bool HastaneyeGeldimi {
            get { return hastaneyeGeldimi; }
            set
            {
                if (value == false)
                {
                    Notify();
                    hastaneyeGeldimi = value;
                }
                else
                    hastaneyeGeldimi = value;
            }
        }


        //Subject nesnesi kendisine abone olan gözlemcileri bu koleksiyonda tutacaktır.
        List<Observer> observers;

        public Doctor()
        {
            this.observers = new List<Observer>();
        }
        //Gözlemci ekle
        public void AddObserver(Observer observer)
        {
            observers.Add(observer);
        }


        //Herhangi bir güncelleme olursa ilgili gözlemcilere haber verilecek
        public void Notify()
        {
            observers.ForEach(g =>
            {
                g.SendMessage(SicilNo, Ad,Soyad);
            });
        }
    }

}
