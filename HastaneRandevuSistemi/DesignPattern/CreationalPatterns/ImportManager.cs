using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HastaneRandevuSistemi.Models;

namespace HastaneRandevuSistemi.Services.ImportExport
{
    public class ImportManager
    {
        private static ImportManager _instance;
        HastaneRandevuSistemiContext db = new HastaneRandevuSistemiContext();
        private ImportManager()
        {

        }

        public static ImportManager GetInstance()
        {
            if (_instance == null)
                _instance = new ImportManager();

            return _instance;
        }

        public bool ImportFile(MyFile file)
        {
            bool state = true;

            if (file == null)
                state = false;

            try
            {
                db.MyFiles.Add(file);
                state = true;
            }
            catch (Exception)
            {
                state = false;
            }

            return state;

        }
    }
}
