using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.Services.Observer
{
	abstract public class Observer
	{
		public abstract void SendMessage(string sicilNo, string firstName, string lastName);
	}

	public class ManagementObserver : Observer
	{
		public override void SendMessage(string sicilNo, string firstName, string lastName)
		{
			Console.WriteLine("Management\nSicil No: {0} \nDoctor {1} {2} didnt come to hospital, dont take appointment.\n", sicilNo, firstName, lastName);
		}
	}

	public class PatientObserver : Observer
	{
		public override void SendMessage(string sicilNo, string firstName, string lastName)
		{
			Console.WriteLine("Patient \nDoctor {0} {1}  didnt come to hospital,cancelled your appointment", firstName, lastName);
		}
	}

}
