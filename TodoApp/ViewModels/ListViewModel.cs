using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.ViewModels
{
    class ListViewModel
    {
        private static ListViewModel _instance;
        public static ListViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new ListViewModel();
            }
            return _instance;
        }
    }
}
