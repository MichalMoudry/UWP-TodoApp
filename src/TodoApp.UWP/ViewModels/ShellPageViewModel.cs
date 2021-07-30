using System.Threading.Tasks;
using TodoApp.Controls;
using TodoApp.Services;

namespace TodoApp.ViewModels
{
    public class ShellPageViewModel
    {
        private readonly DialogService _dialogService;

        private readonly NavigationService _navigationService;

        public ShellPageViewModel(NavigationService navigationService, DialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        public async Task DisplaySettingsDialog()
        {
            await _dialogService.OpenDialog("Settings", new SettingsControl(), "Cancel", "Ok");
        }

        public void NavigateToTodoListPage()
        {
            _navigationService.NavigateToTodoListPage();
        }
    }
}