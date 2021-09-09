using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Shared.Enums;
using TodoApp.Shared.Models.Entity;
using TodoApp.Shared.Services;

namespace TodoApp.ViewModels
{
    /// <summary>
    /// View model class for TodoList page.
    /// </summary>
    public class TodoListPageViewModel : ObservableObject
    {
        /// <summary>
        /// Instance of a class that realizes <see cref="IDataAccess"/> interface.
        /// </summary>
        private readonly IDataAccess _dataAccess;

        private bool _isNotificationDisplayed;

        private Todo _todo;

        private string _todoName;

        /// <summary>
        /// Constructor of <see cref="TodoListPageViewModel"/> class.
        /// </summary>
        /// <param name="dataAccess">Instance of a class that is under <see cref="IDataAccess"/> interface.</param>
        public TodoListPageViewModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        /// <summary>
        /// Property indicating if notification is displayed.
        /// </summary>
        public bool IsNotificationDisplayed
        {
            get => _isNotificationDisplayed;
            set => SetProperty(ref _isNotificationDisplayed, value);
        }

        /// <summary>
        /// Property with content of the notification that is on the page.
        /// </summary>
        public string NotificationContent { get; set; }

        /// <summary>
        /// Name of the to-do that will be added to db.
        /// </summary>
        public string TodoName
        {
            get => _todoName;
            set => SetProperty(ref _todoName, value);
        }

        /// <summary>
        /// Observable collection of <see cref="Todo"/> instances.
        /// </summary>
        public ObservableCollection<Todo> Todos { get; set; } = new ObservableCollection<Todo>();

        /// <summary>
        /// Event handler for adding todos to database and to local observable collection.
        /// </summary>
        public async Task<bool> AddTodo()
        {
            if (!string.IsNullOrEmpty(TodoName))
            {
                _todo = new Todo() { Id = Guid.NewGuid().ToString(), Name = TodoName, IsCompleted = false, Added = DateTime.Now, Updated = DateTime.Now };
                Todos.Add(_todo);
                bool res = await _dataAccess.AddDataAsync(_todo, TableEnums.Todos.ToString());
                TodoName = "";
                if (!res)
                {
                    //TODO: Display error
                }
                return true;
            }
            else
            {
                NotificationContent = "To-do name field must be filled...";
                IsNotificationDisplayed = true;
                IsNotificationDisplayed = false;
                return false;
            }
        }

        /// <summary>
        /// Method for sorting to-dos by a specified key selector.
        /// </summary>
        /// <param name="keySelector">Key selector.</param>
        public void OrderTodoList(Func<Todo, string> keySelector)
        {
            List<Todo> sortedCollection = new List<Todo>(Todos.OrderBy(keySelector));
            Todos.Clear();
            foreach (Todo todo in sortedCollection)
            {
                Todos.Add(todo);
            }
        }

        /// <summary>
        /// Event handler for page Loaded event.
        /// </summary>
        public async void PageLoaded()
        {
            if (Todos.Count > 0)
            {
                Todos.Clear();
            }
            foreach (var todo in await _dataAccess.GetDataAsync(TableEnums.Todos.ToString()))
            {
                Todos.Add((Todo)todo);
            }
        }
    }
}