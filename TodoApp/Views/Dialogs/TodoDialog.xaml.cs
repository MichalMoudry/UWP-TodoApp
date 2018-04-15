using TodoApp.Helpers;
using TodoApp.Models.Database;
using TodoApp.ViewModels.Database;
using Windows.UI.Xaml.Controls;

namespace TodoApp.Views.Dialogs
{
    public sealed partial class TodoDialog : ContentDialog
    {
        private int _listID;
        private Todo _todo;

        public ContentDialogResult Result { get; set; }


        public TodoDialog(int listID)
        {
            InitializeComponent();
            _listID = listID;
            LocalizeText(false);
        }

        public TodoDialog(Todo todo)
        {
            InitializeComponent();
            _listID = todo.ListID;
            _todo = todo;
            todoNameBox.Text = _todo.Name;
            LocalizeText(true);
        }

        /// <summary>
        /// PrimaryButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (!ValidationHelper.Instance().ValidateString(todoNameBox.Text) && ValidationHelper.Instance().IsObjectNull(_todo))
            {
                Result = ContentDialogResult.Primary;
                await TodoViewModel.Instance().AddTodo(new Todo() { Name = todoNameBox.Text, ListID = _listID });
            }
            else if (!ValidationHelper.Instance().ValidateString(todoNameBox.Text) && !ValidationHelper.Instance().IsObjectNull(_todo))
            {
                Result = ContentDialogResult.Secondary;
                _todo.Name = todoNameBox.Text;
                await TodoViewModel.Instance().UpdateTodo(_todo);
            }
            else
            {
                Result = ContentDialogResult.None;
            }
        }

        /// <summary>
        /// SecondaryButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Result = ContentDialogResult.None;
        }

        /// <summary>
        /// Method for setting localized values to XAML elements.
        /// </summary>
        /// <param name="isUpdate">Is update.</param>
        private void LocalizeText(bool isUpdate)
        {
            todoNameBoxLabel.Text = ResourceLoaderHelper.GetResourceLoader().GetString("TodoName");
            if (isUpdate)
            {
                Title = ResourceLoaderHelper.GetResourceLoader().GetString("EditTodo");
                PrimaryButtonText = ResourceLoaderHelper.GetResourceLoader().GetString("Update");
            }
            else
            {
                Title = ResourceLoaderHelper.GetResourceLoader().GetString("AddTodo");
                PrimaryButtonText = ResourceLoaderHelper.GetResourceLoader().GetString("Add");
            }
            SecondaryButtonText = ResourceLoaderHelper.GetResourceLoader().GetString("Cancel");
        }
    }
}