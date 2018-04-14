using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models.Database;

namespace TodoApp.ViewModels.Database
{
    internal class ListViewModel
    {
        private static ListViewModel _instance;
        private DatabaseContext _databaseContext;
        private List<List> _lists;
        private ObservableCollection<List> _observableLists;

        protected ListViewModel()
        {
            _databaseContext = new DatabaseContext();
        }

        public static ListViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new ListViewModel();
            }
            return _instance;
        }

        /// <summary>
        /// Method for adding list to database.
        /// </summary>
        /// <param name="list">List to be added.</param>
        /// <returns>Task result.</returns>
        public async Task AddList(List list)
        {
            list.Added = DateTime.Now;
            _databaseContext.Lists.Add(list);
            await _databaseContext.SaveChangesAsync();
            _observableLists.Add(list);
        }

        /// <summary>
        /// Method for deleting list from database.
        /// </summary>
        /// <param name="list">List to be deleted.</param>
        public async Task DeleteList(List list)
        {
            _databaseContext.Lists.Remove(list);
            _observableLists.Remove(list);
            await _databaseContext.SaveChangesAsync();
            await TodoViewModel.Instance().DeleteTodos(TodoViewModel.Instance().GetTodosAsList(list.ID));
        }

        /// <summary>
        /// Method for deleting List entities from database.
        /// </summary>
        /// <param name="lists">List entites.</param>
        /// <returns>Task result.</returns>
        public async Task DeleteLists(IEnumerable<object> lists)
        {
            foreach (object list in lists)
            {
                await DeleteList((List)list);
            }
        }

        /// <summary>
        /// Method for obtaining List entity by id.
        /// </summary>
        /// <param name="id">ID of an entity.</param>
        /// <returns>List entity.</returns>
        public List GetListByID(int id)
        {
            return _databaseContext.Lists.SingleOrDefault(i => i.ID == id);
        }

        /// <summary>
        /// Method for obtaining observable collection containing lists.
        /// </summary>
        /// <returns>Observable collection containing lists.</returns>
        public ObservableCollection<List> GetListsAsObservable()
        {
            _observableLists = new ObservableCollection<List>(_databaseContext.Lists.ToList());
            return _observableLists;
        }

        /// <summary>
        /// Method for sorting entities in collection.
        /// </summary>
        /// <param name="propertyName">Name of property to sort by.</param>
        public void SortLists(string propertyName)
        {
            if (propertyName.Equals("Name"))
            {
                _lists = _observableLists.OrderBy(i => i.Name).ToList();
            }
            else if (propertyName.Equals("Date"))
            {
                _lists = _observableLists.OrderBy(i => i.Added).ToList();
            }
            _observableLists.Clear();
            foreach (List list in _lists)
            {
                _observableLists.Add(list);
            }
        }

        /// <summary>
        /// Method for updating list.
        /// </summary>
        /// <param name="list">List's new data.</param>
        public async Task UpdateList(List list)
        {
            _databaseContext.Lists.Update(list);
            await _databaseContext.SaveChangesAsync();
        }
    }
}