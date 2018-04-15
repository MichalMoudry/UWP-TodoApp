using System;
using System.Threading.Tasks;
using TodoApp.Models.Database;
using TodoApp.Views.Dialogs;
using Windows.UI.Xaml.Controls;

namespace TodoApp.Services
{
    internal class DialogService
    {
        private static DialogService _instance;
        private DeleteDialog _deleteDialog;
        private ListDialog _listDialog;
        private SubtaskDialog _subtaskDialog;
        private TodoDialog _todoDialog;

        public static DialogService Instance()
        {
            if (_instance == null)
            {
                _instance = new DialogService();
            }
            return _instance;
        }

        /// <summary>
        /// Method for displaying delete dialog.
        /// </summary>
        /// <param name="content">Content of the dialog.</param>
        /// <returns>Result of the ContentDialog.</returns>
        public async Task<ContentDialogResult> ShowDeleteDialogAsync(string content)
        {
            _deleteDialog = new DeleteDialog(content);
            return await _deleteDialog.ShowAsync();
        }

        /// <summary>
        /// Method for showing ContentDialog for List entity.
        /// </summary>
        /// <returns>Result of the ContentDialog.</returns>
        public async Task<ContentDialogResult> ShowListDialogAsync()
        {
            _listDialog = new ListDialog();
            await _listDialog.ShowAsync();
            return _listDialog.Result;
        }

        /// <summary>
        /// Method for showing ContentDialog for List entity.
        /// </summary>
        /// <param name="list">List entity.</param>
        /// <returns>Result of the ContentDialog.</returns>
        public async Task<ContentDialogResult> ShowListDialogAsync(List list)
        {
            _listDialog = new ListDialog(list);
            await _listDialog.ShowAsync();
            return _listDialog.Result;
        }

        /// <summary>
        /// Metod for displaying ContentDialog for Subtask entity.
        /// </summary>
        /// <param name="todoID">ID of parent to-do.</param>
        /// <returns>Result of the ContentDialog.</returns>
        public async Task<ContentDialogResult> ShowSubtaskDialogAsync(int todoID)
        {
            _subtaskDialog = new SubtaskDialog(todoID);
            await _subtaskDialog.ShowAsync();
            return _subtaskDialog.Result;
        }

        /// <summary>
        /// Metod for displaying ContentDialog for Subtask entity.
        /// </summary>
        /// <param name="subtask">Subtask entity.</param>
        /// <returns>Result of the ContentDialog.</returns>
        public async Task<ContentDialogResult> ShowSubtaskDialogAsync(Subtask subtask)
        {
            _subtaskDialog = new SubtaskDialog(subtask);
            await _subtaskDialog.ShowAsync();
            return _subtaskDialog.Result;
        }

        /// <summary>
        /// Method for showing ContentDialog for Todo entity.
        /// </summary>
        /// <param name="listID">ID of a list.</param>
        /// <returns>Result of the ContentDialog.</returns>
        public async Task<ContentDialogResult> ShowTodoDialogAsync(int listID)
        {
            _todoDialog = new TodoDialog(listID);
            await _todoDialog.ShowAsync();
            return _todoDialog.Result;
        }

        /// <summary>
        /// Method for showing ContentDialog for Todo entity.
        /// </summary>
        /// <param name="todo">Todo entity.</param>
        /// <returns>Result of the ContentDialog.</returns>
        public async Task<ContentDialogResult> ShowTodoDialogAsync(Todo todo)
        {
            _todoDialog = new TodoDialog(todo);
            await _todoDialog.ShowAsync();
            return _todoDialog.Result;
        }
    }
}