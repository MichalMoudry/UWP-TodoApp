using System;
using System.Threading.Tasks;
using TodoApp.Views.Dialogs;
using Windows.UI.Xaml.Controls;

namespace TodoApp.Services
{
    internal class DialogService
    {
        private static DialogService _instance;
        private ListDialog _listDialog;
        private TodoDialog _todoDialog;

        public static DialogService Instance()
        {
            if (_instance == null)
            {
                _instance = new DialogService();
            }
            return _instance;
        }

        public async Task<ContentDialogResult> ShowListDialogAsync()
        {
            _listDialog = new ListDialog();
            return await _listDialog.ShowAsync();
        }

        public async Task<ContentDialogResult> ShowTodoDialogAsync()
        {
            _todoDialog = new TodoDialog();
            return await _todoDialog.ShowAsync();
        }
    }
}