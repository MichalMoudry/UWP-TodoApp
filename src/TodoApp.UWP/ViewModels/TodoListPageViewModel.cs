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

        /// <summary>
        /// Private field with a value indicating if notification is displayed.
        /// </summary>
        private bool _isNotificationDisplayed;

        /// <summary>
        /// Key selector used for sorting of to-do list.
        /// </summary>
        private Func<Todo, object> _keySelector;

        /// <summary>
        /// Private field with <see cref="Todo"/> instance.
        /// </summary>
        private Todo _todo;

        /// <summary>
        /// Private field with a value of a new to-do's name.
        /// </summary>
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
                bool res = await _dataAccess.AddDataAsync(_todo, TableEnums.Todos.ToString());
                TodoName = "";
                if (!res)
                {
                    DisplayNotification("Error occured during while saving");
                    return false;
                }
                Todos.Add(_todo);
                //If key selector was set -> sort list
                if (_keySelector != null)
                {
                    OrderTodoList(_keySelector);
                }
                return true;
            }
            else
            {
                DisplayNotification("To-do name field must be filled");
                return false;
            }
        }

        /// <summary>
        /// Method for deleting a to-do.
        /// </summary>
        /// <param name="todo">To-do that will be deleted.</param>
        public async Task DeleteTodo(Todo todo)
        {
            bool res = await _dataAccess.DeleteDataAsync(todo.Id, TableEnums.Todos.ToString());
            if (!res)
            {
                DisplayNotification("Error occured during deletion");
            }
            else
            {
                _ = Todos.Remove(todo);
            }
        }

        /// <summary>
        /// Method for sorting to-dos by a specified key selector.
        /// </summary>
        /// <param name="keySelector">Key selector.</param>
        public void OrderTodoList(Func<Todo, object> keySelector)
        {
            _keySelector = keySelector;
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
            //If key selector was set -> sort list
            if (_keySelector != null)
            {
                OrderTodoList(_keySelector);
            }
        }

        /// <summary>
        /// Event handler for updating todo in a database.
        /// </summary>
        /// <param name="todo">Todo class instance with new data.</param>
        public async Task UpdateTodo(Todo todo)
        {
            bool res = await _dataAccess.UpdateDataAsync(todo, TableEnums.Todos.ToString());
            if (!res)
            {
                DisplayNotification("Error occured during update");
            }
        }

        /// <summary>
        /// Method for displaying a notification on TodoList page.
        /// </summary>
        /// <param name="message">Message that will be displayed.</param>
        private void DisplayNotification(string message)
        {
            NotificationContent = message;
            IsNotificationDisplayed = true;
            IsNotificationDisplayed = false;
        }
    }
}