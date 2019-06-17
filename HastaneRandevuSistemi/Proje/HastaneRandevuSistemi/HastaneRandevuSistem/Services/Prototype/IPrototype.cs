using HastaneRandevuSistemi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.Services.Prototype
{
 public interface IPrototype
	{
	  Kullanici ShallowCopy();
	  Kullanici DeepCopy();
	}
}
