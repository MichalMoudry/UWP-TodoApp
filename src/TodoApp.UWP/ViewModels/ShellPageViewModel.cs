using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoApp.Controls;
using TodoApp.Services;
using System.Collections.ObjectModel;
using TodoApp.Shared.Models.Entity;

namespace TodoApp.ViewModels
{
    /// <summary>
    /// View model class for ShellPage page.
    /// </summary>
    public class ShellPageViewModel
    {
        /// <summary>
        /// Private read-only field with <see cref="DialogService"/> instance.
        /// </summary>
        private readonly DialogService _dialogService;

        /// <summary>
        /// Constructor of <see cref="ShellPageViewModel"/> class.
        /// </summary>
        /// <param name="dialogService">Instance of a <see cref="DialogService"/> service class.</param>
        public ShellPageViewModel(DialogService dialogService)
        {
            _dialogService = dialogService;
        }

        /// <summary>
        /// Event handler for a Click event. Sender: button for displaying about dialog window.
        /// </summary>
        public async void DisplayAboutDialog()
        {
            await _dialogService.OpenDialog("About", null, "Close");
        }

        /// <summary>
        /// Event handler for a Click event. Sender: button for displaying sorting dialog window.
        /// </summary>
        public async void SortListButtonClick()
        {
            TodoListPageViewModel todoListPageViewModel = App.Services.GetRequiredService<TodoListPageViewModel>();
            string sortOption = await _dialogService.OpenSortDialog("Sort to-do list", new SortListControl(), "Close", "Sort");
            if (sortOption.Equals("alphabet"))
            {
                todoListPageViewModel.OrderTodoList(i => i.Name);
            }
            else if (sortOption.Equals("completion"))
            {
                todoListPageViewModel.OrderTodoList(i => i.IsCompleted);
            }
            else if (sortOption.Equals("dateAdded"))
            {
                todoListPageViewModel.OrderTodoList(i => i.Added);
            }
            else if (sortOption.Equals("dateUpdated"))
            {
                todoListPageViewModel.OrderTodoList(i => i.Updated);
            }
        }
    }
}