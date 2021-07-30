using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using TodoApp.Services;
using TodoApp.Controls;

namespace TodoApp.ViewModels
{
    public class ShellPageViewModel
    {
        private NavigationService _navigationService;

        private DialogService _dialogService;

        public ShellPageViewModel(NavigationService navigationService, DialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        public void NavigateToTodoListPage()
        {
            _navigationService.NavigateToTodoListPage();
        }

        public async Task DisplaySettingsDialog()
        {
            await _dialogService.OpenDialog("Settings", new SettingsControl(), "Cancel", "Ok");
        }
    }
}