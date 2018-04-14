using TodoApp.Helpers;
using TodoApp.Models.Database;
using TodoApp.ViewModels.Database;
using Windows.UI.Xaml.Controls;

namespace TodoApp.Views.Dialogs
{
    public sealed partial class SubtaskDialog : ContentDialog
    {
        private int _todoID;
        private Subtask _subtask;

        public SubtaskDialog(int todoID)
        {
            InitializeComponent();
            LocalizeText(false);
            _todoID = todoID;
        }

        public SubtaskDialog(Subtask subtask)
        {
            InitializeComponent();
            LocalizeText(true);
            _subtask = subtask;
            todoNameBox.Text = _subtask.Name;
        }

        /// <summary>
        /// PrimaryButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (!ValidationHelper.Instance().ValidateString(todoNameBox.Text) && ValidationHelper.Instance().IsObjectNull(_subtask))
            {
                await SubtaskViewModel.Instance().AddSubtask(new Subtask() { Name = todoNameBox.Text, TodoID = _todoID });
            }
            else if (!ValidationHelper.Instance().ValidateString(todoNameBox.Text) && !ValidationHelper.Instance().IsObjectNull(_subtask))
            {
                _subtask.Name = todoNameBox.Text;
                await SubtaskViewModel.Instance().UpdateSubtask(_subtask);
            }
        }

        /// <summary>
        /// SecondaryButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        /// <summary>
        /// Method for setting localized values to XAML elements.
        /// </summary>
        /// <param name="isUpdate">Is update.</param>
        private void LocalizeText(bool isUpdate)
        {
            SecondaryButtonText = ResourceLoaderHelper.GetResourceLoader().GetString("Cancel");
            todoNameBoxLabel.Text = ResourceLoaderHelper.GetResourceLoader().GetString("SubtaskName");
            if (isUpdate)
            {
                Title = ResourceLoaderHelper.GetResourceLoader().GetString("UpdateSubtask");
                PrimaryButtonText = ResourceLoaderHelper.GetResourceLoader().GetString("Update");
            }
            else
            {
                Title = ResourceLoaderHelper.GetResourceLoader().GetString("AddSubtask");
                PrimaryButtonText = ResourceLoaderHelper.GetResourceLoader().GetString("Add");
            }
        }
    }
}
