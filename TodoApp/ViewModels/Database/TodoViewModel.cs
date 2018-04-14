using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models.Database;

namespace TodoApp.ViewModels.Database
{
    internal class TodoViewModel
    {
        private static TodoViewModel _instance;
        private DatabaseContext _databaseContext;
        private ObservableCollection<Todo> _todos;

        protected TodoViewModel()
        {
            _databaseContext = new DatabaseContext();
        }

        public static TodoViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new TodoViewModel();
            }
            return _instance;
        }

        /// <summary>
        /// Method for adding Todo entity to database.
        /// </summary>
        /// <param name="todo">Todo entity.</param>
        /// <returns>Task result.</returns>
        public async Task AddTodo(Todo todo)
        {
            todo.IsCompleted = false;
            todo.Added = DateTime.Now;
            _databaseContext.Add(todo);
            await _databaseContext.SaveChangesAsync();
            _todos.Add(todo);
        }

        /// <summary>
        /// Method for deleting Todo entity from database.
        /// </summary>
        /// <param name="todo">Todo entity.</param>
        /// <returns>Task result.</returns>
        public async Task DeleteTodo(Todo todo)
        {
            _databaseContext.Todos.Remove(todo);
            await _databaseContext.SaveChangesAsync();
            await SubtaskViewModel.Instance().DeleteSubtasks(SubtaskViewModel.Instance().GetSubtasksAsList(todo.ID));
            _todos.Remove(todo);
        }

        /// <summary>
        /// Method for deleting Todo entites in database.
        /// </summary>
        /// <param name="todos">Todo entites.</param>
        /// <returns>Task result.</returns>
        public async Task DeleteTodos(IEnumerable<Todo> todos)
        {
            foreach (Todo todo in todos)
            {
                await DeleteTodo(todo);
            }
        }

        /// <summary>
        /// Method for obtaining Todo entites from database as ObservableCollection.
        /// </summary>
        /// <param name="listID">ID of parent list.</param>
        /// <returns>Todo entites from database as ObservableCollection.</returns>
        public ObservableCollection<Todo> GetTodos(int listID)
        {
            _todos = new ObservableCollection<Todo>(_databaseContext.Todos.ToList().Where(i => i.ListID == listID));
            return _todos;
        }

        /// <summary>
        /// Method for obtaining Todo entites from database as List.
        /// </summary>
        /// <param name="listID">ID of parent list.</param>
        /// <returns>Todo entites from database as List.</returns>
        public List<Todo> GetTodosAsList(int listID)
        {
            return _databaseContext.Todos.ToList();
        }

        /// <summary>
        /// Method for updating Todo entity in database.
        /// </summary>
        /// <param name="todo">Todo entity.</param>
        /// <returns>Task result.</returns>
        public async Task UpdateTodo(Todo todo)
        {
            _databaseContext.Todos.Update(todo);
            await _databaseContext.SaveChangesAsync();
            int index = _todos.IndexOf(todo);
            _todos[index] = todo;
        }
    }
}