using HastaneRandevuSistemi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.View
{
	public class CopyView
	{
		
	public 	void ShowWithFormat(int id,string ad,string  userType)
		{
			Console.WriteLine("Id :{0} Name:{1} Type:{2} \n", id,ad, userType);
		}
	}
}
