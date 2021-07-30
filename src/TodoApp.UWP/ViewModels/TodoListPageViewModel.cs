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

        public TodoListPageViewModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        private string todoName;

        public string TodoName
        {
            get => todoName;
            set => SetProperty(ref todoName, value);
        }

        public ObservableCollection<Todo> Todos { get; set; } = new ObservableCollection<Todo>();

        /// <summary>
        /// Event handler for adding todos to database and to local observable collection.
        /// </summary>
        public void AddTodo()
        {
            if (!string.IsNullOrEmpty(TodoName))
            {
                todo = new Todo() { Id = Guid.NewGuid().ToString(), Name = TodoName, IsCompleted = false, Added = DateTime.Now, Updated = DateTime.Now };
                Todos.Add(todo);
                var res = _dataAccess.AddData(todo, $"{TableEnums.Todos}");
                TodoName = "";
                if (!res)
                {
                    //TODO: Display error
                }
            }
        }
    }
}