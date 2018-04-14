using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.ViewModels.UserInterface
{
    class LicenseViewModel
    {
        private static LicenseViewModel _instance;
        public static LicenseViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new LicenseViewModel();
            }
            return _instance;
        }

        protected LicenseViewModel()
        {

        }
    }
}
