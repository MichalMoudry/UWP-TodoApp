using TodoApp.Helpers;
using Windows.UI.Xaml.Controls;

namespace TodoApp.Views.Dialogs
{
    public sealed partial class DeleteDialog : ContentDialog
    {
        public DeleteDialog(string content)
        {
            InitializeComponent();
            dialogContent.Text = content;
            PrimaryButtonText = ResourceLoaderHelper.GetResourceLoader().GetString("Confirm");
            SecondaryButtonText = ResourceLoaderHelper.GetResourceLoader().GetString("Cancel");
            Title = ResourceLoaderHelper.GetResourceLoader().GetString("DeleteDialogTitle");
        }

        /// <summary>
        /// PrimaryButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        /// <summary>
        /// SecondaryButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}