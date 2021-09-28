﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TodoApp.Services;
using TodoApp.Shared.Enums;
using TodoApp.Shared.Models.Entity;
using TodoApp.Shared.Services;

namespace TodoApp.ViewModels
{
    /// <summary>
    /// View model class for TodoDetails page.
    /// </summary>
    public class TodoDetailsPageViewModel : ObservableObject
    {
        /// <summary>
        /// Instance of a class that realizes <see cref="IDataAccess"/> interface.
        /// </summary>
        private readonly IDataAccess _dataAccess;

        /// <summary>
        /// Instance of a <see cref="NavigationService"/> class.
        /// </summary>
        private readonly NavigationService _navigationService;

        /// <summary>
        /// Private field with a value of a new sub to-do's name.
        /// </summary>
        private string _subTodoName;

        /// <summary>
        /// To-do that is being edited.
        /// </summary>
        private Todo _todo;

        public TodoDetailsPageViewModel(IDataAccess dataAccess, NavigationService navigationService)
        {
            _dataAccess = dataAccess;
            _navigationService = navigationService;
        }

        /// <summary>
        /// Name of the sub to-do that will be added to db.
        /// </summary>
        public string SubTodoName
        {
            get => _subTodoName;
            set => SetProperty(ref _subTodoName, value);
        }

        /// <summary>
        /// Observable collection with to-do's sub to-dos.
        /// </summary>
        public ObservableCollection<SubTodo> SubTodos { get; set; } = new ObservableCollection<SubTodo>();

        /// <summary>
        /// To-do that is being edited.
        /// </summary>
        public Todo Todo
        {
            get => _todo;
            set => SetProperty(ref _todo, value);
        }

        /// <summary>
        /// Method for adding a sub to-do.
        /// </summary>
        /// <returns>True if operations was completed, false if not.</returns>
        public async Task<bool> AddSubTodo()
        {
            if (!string.IsNullOrEmpty(SubTodoName))
            {
                await Task.Delay(1);
                SubTodo subTodo = new() { Id = Guid.NewGuid().ToString(), Name = SubTodoName, Added = DateTime.Now, Updated = DateTime.Now, IsCompleted = false, TodoId = Todo.Id };
                SubTodos.Add(subTodo);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method for inverting completion status of a to-do.
        /// </summary>
        public async Task CheckTodo()
        {
            Todo.IsCompleted = !Todo.IsCompleted;
            _ = await _dataAccess.UpdateDataAsync(Todo, TableEnums.Todos.ToString());
        }

        /// <summary>
        /// Method for calling GoBack() method in NavigationService.
        /// </summary>
        public void NavigateBack() => _navigationService.GoBack();

        /// <summary>
        /// Method for updating a to-do note.
        /// </summary>
        public async Task UpdateTodoNote()
        {
            _ = await _dataAccess.UpdateDataAsync(Todo, TableEnums.Todos.ToString());
        }
    }
}