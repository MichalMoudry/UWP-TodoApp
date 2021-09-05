using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Shared.Services;
using TodoApp.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using TodoApp.Shared.Models.Entity;
using TodoApp.Views;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;

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

        private Todo _todo;

        public TodoDetailsPageViewModel(IDataAccess dataAccess, NavigationService navigationService)
        {
            _dataAccess = dataAccess;
            _navigationService = navigationService;
        }

        public Todo Todo
        {
            get => _todo;
            set => SetProperty(ref _todo, value);
        }

        /// <summary>
        /// Method for calling GoBack() method in NavigationService.
        /// </summary>
        public void NavigateBack() => _navigationService.GoBack();
    }
}
