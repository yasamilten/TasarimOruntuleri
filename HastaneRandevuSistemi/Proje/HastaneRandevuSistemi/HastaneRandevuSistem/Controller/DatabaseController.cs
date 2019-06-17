using HastaneRandevuSistem.Services.Abstract_Factory;
using HastaneRandevuSistem.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistem.Controller
{
	public class DatabaseController
	{
		DatabaseView databaseView = new DatabaseView();
		public void CreateDatabase()
		{
			Creater create = new Creater(new MsSqlDatabaseFactory());
			create.Database().CreateCommand().Execute("Select*from Hasta");
			databaseView.PrintResult("MsSql");
			Console.WriteLine("-----------------------------");
			create = new Creater(new SqlDatabaseFactory());
			create.Database().CreateCommand().Execute("Select*from Hasta");
			databaseView.PrintResult("Sql");
		}
	}
}
