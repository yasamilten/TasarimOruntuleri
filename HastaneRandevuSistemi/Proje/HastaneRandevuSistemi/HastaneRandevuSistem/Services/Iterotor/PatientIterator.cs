using HastaneRandevuSistemi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.Services.Iterotor
{

	public interface IAggregate
	{
		IIterator CreateIterator();
	}

	public interface IIterator
	{
		//Bir sonraki adımda hasta var mı?
		bool HasItem();

		//Bir sonraki adımdaki hastayı getir.
		Hasta NextItem();

		//Mevcut hastayı getir.
		Hasta CurrentItem();
	}

	public class PatientAggregate : IAggregate
	{
		public List<Hasta> PatientList = new List<Hasta>();

		public void Add(Hasta Model)
		{
			PatientList.Add(Model);
		}

		public Hasta GetItem(int index)
		{
			return PatientList[index];
		}

		public int Count { get { return PatientList.Count; } }


		public IIterator CreateIterator()
		{
			return new PatientIterator(this);
		}
	}

	class PatientIterator : IIterator
	{
		PatientAggregate aggregate;
		int currentindex;

		public PatientIterator(PatientAggregate aggregate)
		{
			this.aggregate = aggregate;
		}

		public Hasta CurrentItem()
		{
			return aggregate.GetItem(currentindex);
		}
		public bool HasItem()
		{
			if (currentindex < aggregate.Count)
				return true;
			return false;
		}
		public Hasta NextItem()
		{
			if (HasItem())
				return aggregate.GetItem(currentindex++);
			return new Hasta();
		}
	}
}
