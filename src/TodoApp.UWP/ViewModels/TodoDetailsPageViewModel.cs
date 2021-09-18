using Microsoft.Toolkit.Mvvm.ComponentModel;
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
        /// To-do that is being edited.
        /// </summary>
        private Todo _todo;

        public TodoDetailsPageViewModel(IDataAccess dataAccess, NavigationService navigationService)
        {
            _dataAccess = dataAccess;
            _navigationService = navigationService;
        }

        /// <summary>
        /// To-do that is being edited.
        /// </summary>
        public Todo Todo
        {
            get => _todo;
            set => SetProperty(ref _todo, value);
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
    }
}