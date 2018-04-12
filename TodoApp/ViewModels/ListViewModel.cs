using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TodoApp.Models.Database;

namespace TodoApp.ViewModels
{
    class ListViewModel
    {
        private ObservableCollection<List> _observableLists;
        private DatabaseContext _databaseContext;
        private static ListViewModel _instance;
        public static ListViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new ListViewModel();
            }
            return _instance;
        }

        protected ListViewModel()
        {
            _databaseContext = new DatabaseContext();
        }

        public ObservableCollection<List> GetLists()
        {
            _observableLists = new ObservableCollection<List>(_databaseContext.Lists.ToList());
            return _observableLists;
        }

        public void AddList(List list)
        {
            _databaseContext.Lists.Add(list);
            _databaseContext.SaveChanges();
            _observableLists.Add(list);
        }
    }
}
