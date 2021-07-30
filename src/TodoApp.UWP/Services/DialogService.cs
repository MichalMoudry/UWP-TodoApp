using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace TodoApp.Services
{
    public class DialogService
    {
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

        public async Task OpenDialog(ContentDialog contentDialog)
        {
            if (_isDialogOpen)
            {
                return;
            }
            _isDialogOpen = true;
            _ = await contentDialog.ShowAsync();
            _isDialogOpen = false;
        }
    }
}