using Microsoft.Extensions.DependencyInjection;
using TodoApp.Controls;
using TodoApp.Services;

namespace TodoApp.ViewModels
{
    /// <summary>
    /// View model class for ShellPage page.
    /// </summary>
    public class ShellPageViewModel
    {
        private readonly DialogService _dialogService;

        /// <summary>
        /// Constructor of <see cref="ShellPageViewModel"/> class.
        /// </summary>
        /// <param name="dialogService">Instance of a <see cref="DialogService"/> service class.</param>
        public ShellPageViewModel(DialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async void DisplayAboutDialog()
        {
            await _dialogService.OpenDialog("About", null, "Close");
        }
    }
}