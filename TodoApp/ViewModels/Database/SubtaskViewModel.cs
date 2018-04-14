using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models.Database;

namespace TodoApp.ViewModels.Database
{
    internal class SubtaskViewModel
    {
        private static SubtaskViewModel _instance;
        private DatabaseContext _databaseContext;
        private List<Subtask> _listSubtasks;
        private ObservableCollection<Subtask> _subtasks;

        protected SubtaskViewModel()
        {
            _databaseContext = new DatabaseContext();
        }

        public static SubtaskViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new SubtaskViewModel();
            }
            return _instance;
        }

        /// <summary>
        /// Method for adding Subtask entity to database.
        /// </summary>
        /// <param name="subtask">Subtask entity.</param>
        /// <returns>Task result.</returns>
        public async Task AddSubtask(Subtask subtask)
        {
            subtask.Added = DateTime.Now;
            subtask.IsCompleted = false;
            _databaseContext.SubTasks.Add(subtask);
            await _databaseContext.SaveChangesAsync();
            _subtasks.Add(subtask);
        }

        /// <summary>
        /// Method for deleting subtask from database.
        /// </summary>
        /// <param name="subtask">Subtask to be deleted.</param>
        /// <returns>Task result.</returns>
        public async Task DeleteSubtask(Subtask subtask)
        {
            _databaseContext.SubTasks.Remove(subtask);
            await _databaseContext.SaveChangesAsync();
            _subtasks.Remove(subtask);
        }

        /// <summary>
        /// Metod for deleting multiple Subtask entites in database.
        /// </summary>
        /// <param name="subtasks">Subtask entites.</param>
        /// <returns>Task result.</returns>
        public async Task DeleteSubtasks(IEnumerable<Subtask> subtasks)
        {
            foreach (Subtask subtask in subtasks)
            {
                await DeleteSubtask(subtask);
            }
        }

        /// <summary>
        /// Method for obtaining Subtask entites from database as observable collection.
        /// </summary>
        /// <param name="todoID">ID of parent to-do.</param>
        /// <returns>Subtask entites from database as observable collection.</returns>
        public ObservableCollection<Subtask> GetSubtasks(int todoID)
        {
            _subtasks = new ObservableCollection<Subtask>(_databaseContext.SubTasks.ToList().Where(i => i.TodoID == todoID));
            return _subtasks;
        }

        /// <summary>
        /// Method for obtaining Subtask entites from database as list.
        /// </summary>
        /// <param name="todoID">ID of parent to-do.</param>
        /// <returns>Subtask entites from database as list.</returns>
        public List<Subtask> GetSubtasksAsList(int todoID)
        {
            return _databaseContext.SubTasks.Where(i => i.TodoID == todoID).ToList();
        }

        /// <summary>
        /// Method for sorting entities in collection.
        /// </summary>
        /// <param name="propertyName">Name of property to sort by.</param>
        public void SortSubtasks(string propertyName)
        {
            if (propertyName.Equals("Name"))
            {
                _listSubtasks = _subtasks.OrderBy(i => i.Name).ToList();
            }
            else if (propertyName.Equals("Date"))
            {
                _listSubtasks = _subtasks.OrderBy(i => i.Added).ToList();
            }
            _subtasks.Clear();
            foreach (Subtask subtask in _listSubtasks)
            {
                _subtasks.Add(subtask);
            }
        }

        /// <summary>
        /// Method for updating subtask in database.
        /// </summary>
        /// <param name="subtask">Subtask entity to be updated.</param>
        /// <param name="update">Is completed.</param>
        /// <returns>Task result.</returns>
        public async Task UpdateSubtask(Subtask subtask, bool update = true)
        {
            _databaseContext.SubTasks.Update(subtask);
            await _databaseContext.SaveChangesAsync();
            if (update)
            {
                int index = _subtasks.IndexOf(subtask);
                _subtasks[index] = subtask;
            }
        }
    }
}