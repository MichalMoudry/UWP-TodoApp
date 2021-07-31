using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using TodoApp.Shared.Enums;
using TodoApp.Shared.Models.Entity;
using TodoApp.Shared.Services;

namespace TodoApp.ViewModels
{
    public class TodoListPageViewModel : ObservableObject
    {
        private readonly IDataAccess _dataAccess;

        private Todo todo;

        private string todoName;

        /// <summary>
        /// Constructor of <see cref="TodoListPageViewModel"/> class.
        /// </summary>
        /// <param name="dataAccess">Instance of a class that is under <see cref="IDataAccess"/> interface.</param>
        public TodoListPageViewModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public string TodoName
        {
            get => todoName;
            set => SetProperty(ref todoName, value);
        }

        /// <summary>
        /// Observable collection of <see cref="Todo"/> instances.
        /// </summary>
        public ObservableCollection<Todo> Todos { get; set; } = new ObservableCollection<Todo>();

        /// <summary>
        /// Event handler for adding todos to database and to local observable collection.
        /// </summary>
        public async void AddTodo()
        {
            if (!string.IsNullOrEmpty(TodoName))
            {
                todo = new Todo() { Id = Guid.NewGuid().ToString(), Name = TodoName, IsCompleted = false, Added = DateTime.Now, Updated = DateTime.Now };
                Todos.Add(todo);
                var res = await _dataAccess.AddDataAsync(todo, $"{TableEnums.Todos}");
                TodoName = "";
                if (!res)
                {
                    //TODO: Display error
                }
            }
        }

        public async void PageLoaded()
        {
            foreach (var todo in await _dataAccess.GetDataAsync(TableEnums.Todos.ToString()))
            {
                Todos.Add((Todo)todo);
            }
        }
    }
}