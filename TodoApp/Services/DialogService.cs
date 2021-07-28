using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using TodoApp.Controls;

namespace TodoApp.Services
{
    class DialogService
    {
        public DialogService()
        {

        }

        public static bool IsDialogOpen;

        public async Task OpenDialog(string dialogTitle, UserControl userControl)
        {
            if (IsDialogOpen)
            {
                return;
            }
            var dialog = new ContentDialog() { Title = "Test dialog", Content = userControl };
            IsDialogOpen = true;
            await dialog.ShowAsync();
        }
    }
}
