using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoApp.Controls;
using Windows.UI.Xaml.Controls;

namespace TodoApp.Services
{
    /// <summary>
    /// Service for working with ContentDialog class.
    /// </summary>
    public class DialogService
    {
        /// <summary>
        /// Private field with a value indicating if a dialog window is open.
        /// </summary>
        private bool _isDialogOpen;

        public async Task OpenDialog(string dialogTitle, UserControl content, string secondaryButtonText, string primaryButtonText = null, ICommand primaryButtonCommand = null, ICommand secondaryButtonCommand = null)
        {
            if (_isDialogOpen)
            {
                return;
            }
            var dialog = new ContentDialog()
            {
                Title = dialogTitle,
                Content = content,
                SecondaryButtonText = secondaryButtonText
            };
            if (primaryButtonText != null)
            {
                dialog.PrimaryButtonText = primaryButtonText;
            }
            if (primaryButtonCommand != null)
            {
                dialog.PrimaryButtonCommand = primaryButtonCommand;
            }
            if (secondaryButtonCommand != null)
            {
                dialog.SecondaryButtonCommand = secondaryButtonCommand;
            }
            _isDialogOpen = true;
            _ = await dialog.ShowAsync();
            _isDialogOpen = false;
        }

        public async Task<string> OpenSortDialog(string dialogTitle, SortListControl content, string secondaryButtonText, string primaryButtonText = null, ICommand primaryButtonCommand = null)
        {
            if (_isDialogOpen)
            {
                return null;
            }
            var dialog = new ContentDialog()
            {
                Title = dialogTitle,
                Content = content,
                SecondaryButtonText = secondaryButtonText
            };
            if (primaryButtonText != null)
            {
                dialog.PrimaryButtonText = primaryButtonText;
            }
            if (primaryButtonCommand != null)
            {
                dialog.PrimaryButtonCommand = primaryButtonCommand;
            }
            _isDialogOpen = true;
            _ = await dialog.ShowAsync();
            _isDialogOpen = false;
            return content.SortProperty;
        }
    }
}