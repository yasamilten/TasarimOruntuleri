using HastaneRandevuSistemi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.Services.Decorator
{
	//Component
	interface IRandevuIslemler
	{
		void Ekle(Randevu randevu);
		void Sil(Randevu randevu);
	}

	//ConcreteComponent
	class RandevuIslemler : IRandevuIslemler
	{
		//Projede veritabanı bağlanıcak. Bu metot veritabanındaki Randevu tablosuna kayıt eklicek
		public void Ekle(Randevu randevu)
		{

			//_db.Randevus.Add(randevu);
			Console.WriteLine("hasta randevu eklendi.");
		}

		//Projede veritabanı bağlanıcak. Bu metot veritabanındaki Randevu tablosundan randevu silicek
		public void Sil(Randevu randevu)
		{
			//_db.Randevus.Remove(randevu);
			Console.WriteLine("hasta randevu silindi.");
		}
	}

	//Decorator
	abstract class RandevuIslemlerDecorator : IRandevuIslemler
	{
		private IRandevuIslemler randevuOperation;
		public RandevuIslemlerDecorator(IRandevuIslemler randevuOperationn)
		{
			this.randevuOperation = randevuOperationn;
		}

		public void Ekle(Randevu randevu)
		{
			randevuOperation.Ekle(randevu);
		}

		public void Sil(Randevu randevu)
		{
			randevuOperation.Sil(randevu);
		}
	}

	//ConcreteDecorator
	class RandevuMesajOperation : RandevuIslemlerDecorator
	{
		public RandevuMesajOperation(IRandevuIslemler randevuOperation) : base(randevuOperation)
		{
		}
		public void MesajGonder(string mesaj)
		{
			Console.WriteLine("Hastaya '{0}' mesajı gönderildi.", mesaj);
		}
	}
}
