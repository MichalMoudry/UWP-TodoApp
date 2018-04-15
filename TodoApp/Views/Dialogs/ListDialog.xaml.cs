using TodoApp.Helpers;
using TodoApp.Models.Database;
using TodoApp.ViewModels.Database;
using Windows.UI.Xaml.Controls;

namespace TodoApp.Views.Dialogs
{
    public sealed partial class ListDialog : ContentDialog
    {
        private List _list;

        public ListDialog()
        {
            InitializeComponent();
            LocalizeText(false);
        }

        public ListDialog(List list)
        {
            InitializeComponent();
            _list = list;
            listNameBox.Text = _list.Name;
            LocalizeText(true);
        }

        public ContentDialogResult Result { get; set; }

        /// <summary>
        /// PrimaryButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (!ValidationHelper.Instance().ValidateString(listNameBox.Text) && ValidationHelper.Instance().IsObjectNull(_list))
            {
                Result = ContentDialogResult.Primary;
                await ListViewModel.Instance().AddList(new List() { Name = listNameBox.Text });
            }
            else if (!ValidationHelper.Instance().ValidateString(listNameBox.Text) && ValidationHelper.Instance().IsObjectNull(_list).Equals(false))
            {
                Result = ContentDialogResult.Secondary;
                _list.Name = listNameBox.Text;
                await ListViewModel.Instance().UpdateList(_list);
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
            Result = ContentDialogResult.Secondary;
        }

        /// <summary>
        /// Method for setting localized values to XAML elements.
        /// </summary>
        /// <param name="isUpdate">Is update.</param>
        private void LocalizeText(bool isUpdate)
        {
            listNameBoxLabel.Text = ResourceLoaderHelper.GetResourceLoader().GetString("ListName");
            SecondaryButtonText = ResourceLoaderHelper.GetResourceLoader().GetString("Cancel");
            if (isUpdate)
            {
                Title = ResourceLoaderHelper.GetResourceLoader().GetString("EditList");
                PrimaryButtonText = ResourceLoaderHelper.GetResourceLoader().GetString("Update");
            }
            else
            {
                Title = ResourceLoaderHelper.GetResourceLoader().GetString("AddList");
                PrimaryButtonText = ResourceLoaderHelper.GetResourceLoader().GetString("Add");
            }
        }
    }
}